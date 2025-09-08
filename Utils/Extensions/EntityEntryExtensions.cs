//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Tools.Extensions
{
    public static class DataExtensions
    {

        //public static HashSet<string> GetPrimaryKeysNames(this EntityEntry entry)
        //    => entry.Metadata.FindPrimaryKey()?.Properties
        //            .Select(p => p.Name)
        //            .ToHashSet(StringComparer.InvariantCultureIgnoreCase)
        //    ?? new();

        //public static object?[] GetPrimaryKeys(this EntityEntry entry)
        //    => entry.Metadata.FindPrimaryKey()?
        //            .Properties
        //            .Select(p => entry.Property(p.Name).CurrentValue)
        //            .ToArray() ?? Array.Empty<object>();

        //public static IQueryable<T> IncludeRelatedNavigations<T>(this IQueryable<T> queryable,
        //    IEnumerable<string> navigations, bool load = true) where T : class
        //{
        //    if (load)
        //        foreach (var navigation in navigations)
        //            queryable = queryable.Include(navigation);

        //    return queryable;
        //}
    }
}
