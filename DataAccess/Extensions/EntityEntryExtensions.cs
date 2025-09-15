using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    internal static class EntityEntryExtensions
    {
        internal static HashSet<string> GetPrimaryKeysNames(this EntityEntry entry)
            => entry.Metadata.FindPrimaryKey()?.Properties
                    .Select(p => p.Name)
                    .ToHashSet(StringComparer.InvariantCultureIgnoreCase)
            ?? new();

        internal static object?[] GetPrimaryKeys(this EntityEntry entry)
            => entry.Metadata.FindPrimaryKey()?
                    .Properties
                    .Select(p => entry.Property(p.Name).CurrentValue)
                    .ToArray() ?? Array.Empty<object>();
    }
}
