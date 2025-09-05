using DataAccess.Entities;
using Gridify;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils.Helpers;

namespace DataAccess
{
    public interface IGenericRepo<T> : IDisposable where T : class
    {
        public T Get(params object[] keys);
        public List<T> GetAll();
        public T Add(T entity);
        public T AddSaving(User user, T entity);
        public T UpdateSaving(User user, T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> items);
        public T Find(Func<T, bool> predicate);
        public List<T> FindAll(Func<T, bool> predicate);
        public int Save();
        //public List<T> GetPaginated(Expression<Func<T, bool>> predicate, int? limit, int? offset, out int rowsCount, out int pageNumber, out int totalCount);
        //public List<T> GetPaginated(int? limit, int? offset, out int rowsCount, out int pageNumber, out int totalCount);
        //public List<T> GetAll(int? page, int? limit, out int rows, out int pageNumber, out int totalCount);
        //public List<T> GetAll(Expression<Func<T, bool>> predicate, int? page, int? limit, out int rows, out int pageNumber, out int totalCount);
        //public List<T> FindAll(GridifyQuery gridifyQuery);
        //public List<T> FindAll(IQueryBuilder<T> builder);

        public Paging<T> GetPaginated(GridifyQuery gridifyQuery);
        //public Paging<T> GetPaginated(IQueryBuilder<T> builder);
    }
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private const int MYSQL_CUSTOM_ERROR_CODE = 99999;

        protected PGIContext context;

        public GenericRepo(PGIContext context)
        {
            this.context = context;

            this.context.SaveChangesFailed += Context_SaveChangesFailed;
        }

        public virtual T Add(T entity)
        {

            var keys = GetPrimaryKeys(entity);

            if (keys == null)
            {
                throw new Exception("Error en los datos suministrados");
            }

            T source = Get(keys);

            if (source != null)
            {
                throw new Exception($"Campo duplicado '{string.Join(", ", keys)}'");
            }

            context.Set<T>().Add(entity);
            Save();
            return entity;
        }

        public virtual T AddSaving(User user, T entity)
        {
            Add(entity);
            Save();
            return entity;
        }

        /*public virtual T Get(int id)
        {
            return context.Set<T>().Find(id);
        }*/

        public virtual T Get(params object[] keys)
        {
            return context.Set<T>().Find(keys);
        }

        /* public virtual T Get(string? code)
         {
             return context.Set<T>().Find(code);
         }*/

        public virtual List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public virtual List<T> GetAll(int? page, int? limit, out int rows, out int pageNumber, out int totalCount)
        {
            return context.Set<T>().AsQueryable().Paginate(limit, page, out rows, out pageNumber, out totalCount).ToList();
        }
        //public virtual List<T> GetAll(Expression<Func<T>, int? page, int? limit, out int rows, out int pageNumber, out int totalCount)
        //{
        //    return context.Set<T>().AsQueryable().Paginate(limit, page, out rows, out pageNumber, out totalCount).ToList();
        //}
        public virtual List<T> GetAll(Expression<Func<T, bool>> predicate, int? page, int? limit, out int rows, out int pageNumber, out int totalCount)
        {
            return GetPaginated(predicate, limit, page, out rows, out pageNumber, out totalCount);
        }
        public virtual T Find(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public virtual List<T> FindAll(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate).AsQueryable().ToList();
        }

        public virtual T Update(T source, T entity)
        {
            context.Entry(source).CurrentValues.SetValues(entity);

            //context.Set<T>().Update(entity);

            return entity;

        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> items)
        {
            context.Set<T>().UpdateRange(items);

            return items;
        }

        public virtual T Update(T entity)
        {
            var keys = GetPrimaryKeys(entity);

            if (keys == null)
            {
                throw new Exception("El valor a actualizar no existe");
            }

            T source = Get(keys);

            if (source == null)
            {
                throw new Exception("El valor a actualizar no existe");
            }

            // context.Entry<T>(source).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            //context.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //var entry = context.Entry<T>(source);

            //entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //entry.CurrentValues.SetValues(entity);

            //entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //context.Attach<T>(source);

            //context.Set<T>().Update(entity);
            Update(source, entity);
            return entity;

        }

        public virtual T UpdateSaving(User user, T entity)
        {
            Update(entity);
            var result = Save();

            return entity;
        } 
        public virtual T RemoveSaving( T entity)
        {
            var keys = GetPrimaryKeys(entity);

            if (keys == null)
            {
                throw new Exception("El valor a eliminar no existe");
            }

            T source = Get(keys);

            if (source == null)
            {
                throw new Exception("El valor a eliminar no existe");
            }


            context.Remove(source);
            var result = Save();

            return entity;
        }
        public virtual T UpdateSaving(T source, T entity)
        {
            Update(source, entity);
            Save();

            return entity;
        }

        public virtual int Save()
        {
            return context.SaveChanges();
        }

        private object[] GetPrimaryKeys(T entity)
        {
            var entry = context.Entry(entity);

            return entry.Metadata.FindPrimaryKey()
                         .Properties
                         .Select(p => entry.Property(p.Name).CurrentValue)
                         .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="limit">cantidad maxima de registro</param>
        /// <param name="offset">los que se van a omitir</param>
        /// <param name="rowsCount">cantidad de registro que se esta devolviendo</param>
        /// <param name="pageNumber">pagina actual</param>
        /// <param name="totalCount">cantidad de registro en total</param>
        /// <returns></returns>

        public virtual List<T> GetPaginated(Expression<Func<T, bool>> predicate, int? limit, int? offset, out int rowsCount, out int pageNumber, out int totalCount)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .Paginate(limit, offset, out rowsCount, out pageNumber, out totalCount);
        }

        public virtual List<T> GetPaginated(int? limit, int? offset, out int rowsCount, out int pageNumber, out int totalCount)
        {
            return context.Set<T>()
                .AsQueryable()
                .Paginate(limit, offset, out rowsCount, out pageNumber, out totalCount);
        }
        private void Context_SaveChangesFailed(object sender, Microsoft.EntityFrameworkCore.SaveChangesFailedEventArgs e)
        {
            if (e.Exception.InnerException != null)
            {
                if (e.Exception.InnerException is MySqlConnector.MySqlException)
                {
                    var ex = e.Exception.InnerException as MySqlConnector.MySqlException;

                    // CUSTOM ERROR FROM TRIGGERS
                    if (MYSQL_CUSTOM_ERROR_CODE.ToString() == ex.SqlState)
                    {
                        throw new Exception($"{ex.Message}");
                    }

                    switch (ex.ErrorCode)
                    {
                        // CAMPO DUPLICADO
                        case MySqlConnector.MySqlErrorCode.DuplicateKeyEntry:

                            var value = Regex.Match(ex.Message, "'.*?'").Value;

                            throw new Exception($"El valor {value} ya existe.");

                        // ERROR DE REFERENCIA
                        case MySqlConnector.MySqlErrorCode.NoReferencedRow2:

                            new LogData().Error(ex);
                            var foreignKeyField = Regex.Match(ex.Message, "FOREIGN KEY \\(`(.*?)`\\)").Groups[1].Value;

                            // = Regex.Match(ex.Message, "'.*?'").Value;

                            throw new Exception($"El valor vinculado no existe. {foreignKeyField}");

                        default:

                            new LogData().Error(ex);

                            throw new Exception($"Ocurrió un error procesando su solicitud ({ex.ErrorCode}), {ex.Message}");
                    }
                }
            }

            new LogData().Error(e.Exception);

            throw new Exception($"Ocurrió un error procesando su solicitud ({e.Exception.HResult})");
        }

        public virtual List<T> FindAll(GridifyQuery gridifyQuery)
        {
            return context.Set<T>().ApplyFiltering(gridifyQuery).ToList();

        }

        public virtual List<T> FindAll(IQueryBuilder<T> builder)
        {
            if (!builder.IsValid())
                throw new();

            var exp = builder.BuildFilteringExpression();

            return context.Set<T>().Where(exp).ToList();
        }

        //public virtual Paging<T> GetPaginated(GridifyQuery gridifyQuery)
        //{
        //    return context.Set<T>().Gridify(gridifyQuery);

        //}
        public virtual Paging<T> GetPaginated(GridifyQuery gridifyQuery)
        {
            // return context.Set<T>().ApplyPaging(gridifyQuery).Paginate() ;
            return context.Set<T>().Gridify(gridifyQuery);

        }

        public virtual Paging<T> GetPaginated(IQueryBuilder<T> builder)
        {
            if (!builder.IsValid())
                throw new();

            var exp = builder.BuildWithPaging(context.Set<T>());

            return exp;
        }

        public void Dispose()
        {
            context.Dispose();
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
