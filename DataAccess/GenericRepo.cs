using Common.Exceptions;
using DataAccess.Entities;
using DataAccess.Extensions;
using Gridify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils.Extensions;
using Utils.Helpers;

namespace DataAccess
{
    public interface IGenericRepo<T> where T : class
    {
        public DbSet<T> EntityDbSet { get; init; }
        public T? Get(params object?[] keys);
        public List<T> GetAll();
        public bool Exists(Expression<Func<T, bool>> predicate);
        public T? Find(Expression<Func<T, bool>> predicate);
        public T? Find(GridifyQuery query);
        public List<T> FindAll(Expression<Func<T, bool>> predicate);
        public List<T> FindAll(GridifyQuery query);
        public Paging<T> GetPaginated(GridifyQuery gridifyQuery);
        public T Add(T entity);
        public T AddSaving(T entity);
        public Task<T[]> AddSavingCatching(params T[] entities);
        public T AddOrUpdate(T entity, bool @override = false);
        public T AddOrUpdate(Expression<Func<T, bool>> predicate, T entity);
        public T AddOrUpdateSaving(Expression<Func<T, bool>> predicate, T entity);
        public T AddOrUpdateSaving(T entity);
        public IEnumerable<T> AddOrUpdateRange(IEnumerable<T> entities, bool @override = false);
        public IEnumerable<T> AddOrUpdateRangeSaving(IEnumerable<T> entities, bool @override = false);
        public IEnumerable<T> AddRange(IEnumerable<T> entities);
        public IEnumerable<T> AddRangeSaving(IEnumerable<T> entities);
        public T Update(T entity);
        public T UpdateSaving(T entity);
        public IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        //public T Patch(T entity, PartialUpdater<T> patcher);
        //public T Patch(Expression<Func<T, bool>> predicate, PartialUpdater<T> patcher);
        //public T PatchSaving(Expression<Func<T, bool>> predicate, PartialUpdater<T> patcher);
        //public T PatchSaving(T entity, PartialUpdater<T> patcher);
        public void RemoveSaving(T entity);
        public int Save();
    }

    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly PGIContext context;

        public DbSet<T> EntityDbSet { get; init; }

        private static readonly ConcurrentDictionary<Type, IEnumerable<string>> TypeNavigations = new();
        private protected readonly Type entityType;
        IGlobalFilters globalFilter;
        public GenericRepo(PGIContext context)
        {
            this.context = context;
            entityType = typeof(T);

            EntityDbSet = context.Set<T>();

            this.globalFilter = context.GetService<IGlobalFilters>();
        }

        public virtual T Add(T entity)
        {
            var keys = context.Entry(entity).GetPrimaryKeys()
                ?? throw new BadRequestException();

            T? source = FindEntityOrNull(entity);

            if (source != null)
                throw new BadRequestException($"Duplicate entry '{string.Join(", ", keys)}'");

            TrackEntity(entity);

            context.Add(entity);

            return entity;
        }

        public virtual T AddSaving(T entity)
        {
            ValidateOrThow(entity);

            OnCreate(entity);

            Add(entity);

            //if (!IsValid(entity, out List<ValidationResult> results))
            //    throw new BadRequestException(string.Join(" | ", results.Select(x => x.ErrorMessage)));

            Save();

            OnCreated(entity);

            return entity;
        }

        public virtual Task<T[]> AddSavingCatching(params T[] entities)
        {
            try
            {
                AddRangeSaving(entities);

                return Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                return Task.FromException<T[]>(ex);
            }
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            EntityDbSet.AddRange(entities);

            return entities;
        }

        public virtual IEnumerable<T> AddRangeSaving(IEnumerable<T> entities)
        {
            AddRange(entities);

            Save();

            return entities;
        }

        public virtual T AddOrUpdate(Expression<Func<T, bool>> predicate, T entity)
        {
            var existing = EntityDbSet.SingleOrDefault(predicate);

            if (existing is not null)
            {
                var pkNames = context.Entry(entity).GetPrimaryKeysNames();
                entity.MapTo(existing, p => pkNames.Contains(p.Name));

                return Update(existing);
            }

            return Add(entity);
        }

        public virtual T AddOrUpdateSaving(Expression<Func<T, bool>> predicate, T entity)
        {
            AddOrUpdate(predicate, entity);
            context.SaveChanges();

            return entity;
        }

        public virtual T AddOrUpdate(T entity, bool @override = false)
        {
            var existing = FindEntityOrNull(entity);

            if (existing != null)
                entity = Update(entity);
            else
                Add(entity);

            return entity;
        }

        public virtual T AddOrUpdateSaving(T entity)
        {
            AddOrUpdate(entity);
            context.SaveChanges();

            return entity;
        }

        public virtual IEnumerable<T> AddOrUpdateRange(IEnumerable<T> entities, bool @override = false)
        {
            foreach (var entity in entities)
                AddOrUpdate(entity, @override);
            return entities;
        }

        public virtual IEnumerable<T> AddOrUpdateRangeSaving(IEnumerable<T> entities, bool @override = false)
        {
            AddOrUpdateRange(entities, @override);
            Save();

            return entities;
        }

        private TEntity? FindEntityOrNull<TEntity>(TEntity entity) where TEntity : class
        {
            return FindEntityOrNull(entity, entity.GetType());
        }

        private TEntity? FindEntityOrNull<TEntity>(TEntity entity, Type entityType) where TEntity : class
        {
            var keys = context.Entry(entity).GetPrimaryKeys();

            if (keys == null || !keys.Any()) return null;

            var source = context.Find(entityType, keys);

            if (source == null) return null;

            var entry = context.Entry(source);

            if (entry == null) return null;

            source = entry.GetDatabaseValues()?.ToObject();

            if (source == null) return null;

            entry.State = EntityState.Detached;

            /*if (parseValues)
                ObjectParser.Parse(entity, dbValues);

            if (parseNavs)
                ObjectParser.ParseNonPrimitives(entity, dbValues);*/

            return source as TEntity;
        }

        public virtual T? Get(params object?[] keys)
        {
            return EntityDbSet.Find(keys);
        }

        public virtual List<T> GetAll()
        {
            return EntityDbSet.AsNoTrackingWithIdentityResolution().ToList();
        }


        public virtual bool Exists(Expression<Func<T, bool>> predicate)
            => Find(predicate) is not null;

        public virtual T? Find(Expression<Func<T, bool>> predicate)
        {
            return EntityDbSet.AsNoTrackingWithIdentityResolution().FirstOrDefault(predicate);
        }

        public virtual T? Find(GridifyQuery query)
        {
            return EntityDbSet.AsNoTrackingWithIdentityResolution().ApplyFiltering(query).FirstOrDefault();
        }

        public virtual List<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return EntityDbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToList();
        }
        public virtual Paging<T> GetPaginated(GridifyQuery gridifyQuery)
        {
            // return context.Set<T>().ApplyPaging(gridifyQuery).Paginate() ;
            //return EntityDbSet.AsNoTrackingWithIdentityResolution().Gridify(gridifyQuery);
            return IncludeRelatedNavigations(EntityDbSet.AsNoTrackingWithIdentityResolution(), GetNavigationProps(), globalFilter.Expand)
                .Gridify(gridifyQuery);

        }
        private IEnumerable<string> GetNavigationProps()
        {
            return TypeNavigations.GetOrAdd(entityType, (type) =>
            {
                var model = context.Model.FindEntityType(type);
                var navs = model.GetNavigations()
                    .Select(x => x.Name)
                    .ToList();

                navs.AddRange(model.GetSkipNavigations().Select(x => x.Name));
                return navs;
            });
        }
        public  IQueryable<T> IncludeRelatedNavigations<T>(IQueryable<T> queryable,
            IEnumerable<string> navigations, bool load = true) where T : class
        {
            if (load)
                foreach (var navigation in navigations)
                    queryable = queryable.Include(navigation);

            return queryable;
        }
        public virtual List<T> FindAll(GridifyQuery query)
        {
            //return EntityDbSet.AsNoTrackingWithIdentityResolution().ApplyFiltering(query)
            //    .ToList();
            return EntityDbSet.AsNoTrackingWithIdentityResolution().Gridify(query).Data.ToList();
        }

        public virtual T Update(T entity)
        {
            TrackEntity(entity);

            return entity;
        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            context.UpdateRange(entities);

            return entities;
        }


        private void TrackEntity(object entity)
        {
            if (entity is null)
                return;

            context.ChangeTracker.TrackGraph(entity, e =>
            {
                if (!e.Entry.State.In(EntityState.Added, EntityState.Modified, EntityState.Detached))
                    return;

                var dbValues = FindEntityOrNull(e.Entry.Entity);

                if (dbValues == null)
                {
                    e.Entry.State = EntityState.Added;
                    return;
                }

                if (dbValues.EntityEquals(e.Entry.Entity))
                {
                    TrackEntitySkipNavigations(e.Entry, e.Entry.Metadata.GetDeclaredSkipNavigations());

                    e.Entry.State = EntityState.Unchanged;
                    return;
                }

                TrackEntitySkipNavigations(e.Entry, e.Entry.Metadata.GetDeclaredSkipNavigations());

                e.Entry.State = EntityState.Modified;
            });
        }

        private void TrackEntitySkipNavigations(EntityEntry? entry, IEnumerable<ISkipNavigation>? navigations)
        {
            if (entry == null)
                return;

            if (navigations == null)
                return;

            var entity = entry.Entity;

            if (entity == null)
                return;

            var entityType = entry.Metadata.ClrType;

            foreach (var navigation in navigations)
            {
                var navEntry = entry.Navigation(navigation.Name);

                if (navigation.IsCollection)
                {
                    var navValues = (IEnumerable?)navEntry.CurrentValue;

                    if (navValues is null) continue;

                    foreach (var navValue in navValues)
                        TrackEntitySkipNavigation(entityType, entity, navValue, navigation);
                }
                else
                    TrackEntitySkipNavigation(entityType, entity, navEntry.CurrentValue, navigation);
            }
        }

        private void TrackEntitySkipNavigation(Type entityType, object entity, object navValue, ISkipNavigation navigation)
        {
            var joiningEntity = Activator.CreateInstance(navigation.JoinEntityType.ClrType);

            foreach (var prop in navigation.ForeignKey.PrincipalKey.Properties)
            {
                var fkvalue = entityType.GetProperty(prop.Name)?.GetValue(entity);

                foreach (var joiningProp in navigation.ForeignKey.Properties)
                    joiningProp.PropertyInfo.SetValue(joiningEntity, fkvalue);
            }

            foreach (var prop in navigation.Inverse.ForeignKey.PrincipalKey.Properties)
            {
                var navType = navigation.Inverse.ForeignKey.PrincipalEntityType.ClrType;

                var fkvalue = navType.GetProperty(prop.Name)?.GetValue(navValue);

                foreach (var joiningProp in navigation.Inverse.ForeignKey.Properties)
                    joiningProp.PropertyInfo.SetValue(joiningEntity, fkvalue);
            }

            TrackEntity(joiningEntity);
        }

        public virtual T UpdateSaving(T entity)
        {
            ValidateOrThow(entity, true);

            OnUpdate(entity);

            TrackEntity(entity);

            context.Update(entity);

            if (!IsValid(entity, out List<ValidationResult> results))
                throw new BadRequestException(string.Join(" | ", results.Select(x => x.ErrorMessage)));

            Save();

            OnUpdated(entity);

            return entity;
        }

        //public virtual T Patch(T entity, PartialUpdater<T> patcher)
        //{
        //    patcher.Patch(entity, context.Entry(entity));

        //    return entity;
        //}

        //public virtual T Patch(Expression<Func<T, bool>> predicate, PartialUpdater<T> patcher)
        //{
        //    var entity = Find(predicate)
        //        ?? throw new NotFoundException($"{nameof(T)} not found");

        //    return Patch(entity, patcher);
        //}

        //public virtual T PatchSaving(T entity, PartialUpdater<T> patcher)
        //{
        //    var entityPatched = Patch(entity, patcher);

        //    return UpdateSaving(entityPatched);
        //}

        //public virtual T PatchSaving(Expression<Func<T, bool>> predicate, PartialUpdater<T> patcher)
        //{
        //    var entityPatched = Patch(predicate, patcher);

        //    return UpdateSaving(entityPatched);
        //}

        public virtual void RemoveSaving(T entity)
        {
            OnDelete(entity);

            EntityDbSet.Remove(entity);

            Save();

            OnDeleted(entity);

        }

        protected static bool IsValid(T entity, out List<ValidationResult> results)
        {
            results = new();

            return Validator.TryValidateObject(
                 entity
                 , new ValidationContext(entity)
                 , results
                 , true);
        }

        public virtual void ValidateOrThow(T entity, bool isUpdating = false)
        {
            if (!IsValid(entity, out List<ValidationResult> results))
                throw new BadRequestException(string.Join(" | ", results.Select(x => x.ErrorMessage)));
        }

        /// <summary>
        /// Action called after ValidateOrThow and before Save
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        public virtual void OnCreate(T entity) { }

        /// <summary>
        /// Action called after ValidateOrThow and before Save
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        public virtual void OnUpdate(T entity) { }

        /// <summary>
        /// Action called after ValidateOrThow and before Save
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        public virtual void OnDelete(T entity) { }

        /// <summary>
        /// Action called after Save
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        public virtual void OnCreated(T entity) { }

        /// <summary>
        /// Action called after Save
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        public virtual void OnUpdated(T entity) { }

        /// <summary>
        /// Action called after Save
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        public virtual void OnDeleted(T entity) { }

        public virtual int Save()
        {
            return context.SaveChanges();
        }
    }

}

public static class PaginatorExtension
{
    public const int DEFAULT_ROWS_NUMBER = 500;

    public const int MAX_ROWS_NUMBER = 5000;

    public const int DEFAULT_PAGE_NUMBER = 0;

    public static List<T> Paginate<T>(this IQueryable<T> source, int? limit, int? offset, out int rowsCount, out int pageNumber, out int totalCount)
    {
        totalCount = source.Count();

        if (!offset.HasValue || offset < 0) offset = DEFAULT_PAGE_NUMBER;

        if (!limit.HasValue || limit <= 0) limit = DEFAULT_ROWS_NUMBER;
        else if (limit > MAX_ROWS_NUMBER) limit = MAX_ROWS_NUMBER;

        var skip = offset.Value > 0
            ? (offset.Value - 1) * limit.Value
            : offset.Value * limit.Value;

        var result = source.Skip(skip).Take(limit.Value).ToList();

        rowsCount = result.Count;
        pageNumber = offset.Value == DEFAULT_PAGE_NUMBER ? 1 : offset.Value;

        return result;

    }
}
