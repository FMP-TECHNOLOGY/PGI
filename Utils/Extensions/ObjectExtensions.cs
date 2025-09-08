using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Utils.Extensions
{
    public static class ObjectExtensions
    {
        private readonly static Dictionary<Type, Dictionary<string, PropertyInfo>> TypeProperties = new();

        private readonly static HashSet<string> IgnoredProps = new(StringComparer.InvariantCultureIgnoreCase) {
            "CreatedAt",
            "CreatedBy",
            "UpdatedAt",
            "UpdatedBy",
            "LogInstance"
        };

        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };

        public static string? ToJsonString<T>(this T obj) where T : class
        {
            if (obj == null) return null;

            if (obj is Stream stream)
                return JsonSerializer.Serialize(stream, jsonSerializerOptions);

            return JsonSerializer.Serialize(obj, typeof(T), jsonSerializerOptions);
        }

        public static bool EntityEquals(this object source, object other)
        {
            if (source == null && other == null)
                return true;

            if (source is null && other is not null)
                return false;

            if (other is null)
                return false;

            if (source is null)
                return false;

            var parentType = source.GetType();
            var childType = other.GetType();

            if (!TypeProperties.TryGetValue(parentType, out Dictionary<string, PropertyInfo>? parentProperties))
            {
                parentProperties = parentType.GetProperties().Where(p => p.PropertyType.IsPrimitive() && !IgnoredProps.Contains(p.Name)).ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);
                TypeProperties.Add(parentType, parentProperties);
            }

            if (!TypeProperties.TryGetValue(childType, out Dictionary<string, PropertyInfo>? childProperties))
            {
                childProperties = childType.GetProperties().Where(p => p.PropertyType.IsPrimitive() && !IgnoredProps.Contains(p.Name)).ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);
                TypeProperties.Add(childType, childProperties);
            }

            if (parentProperties.Count != childProperties.Count)
                return false;

            foreach (var prop in parentProperties)
            {
                if (!childProperties.TryGetValue(prop.Key, out PropertyInfo? childProp))
                    return false;

                var parentProp = prop.Value;

                if ((Nullable.GetUnderlyingType(parentProp.PropertyType) ?? parentProp.PropertyType) != (Nullable.GetUnderlyingType(childProp.PropertyType) ?? childProp.PropertyType))
                    return false;

                var parentValue = parentProp.GetValue(source);
                var childValue = childProp.GetValue(other);

                if (parentValue is null && childValue is not null)
                    return false;

                if (!(parentValue?.Equals(childValue)) ?? false)
                    return false;
            }

            return true;
        }

        public static void MapTo(this object source, object other, Func<PropertyInfo, bool>? ignorePropsPredicate = null)
        {
            if (source == null && other == null)
                return;

            if (source is null && other is not null)
                return;

            if (other is null)
                return;

            if (source is null)
                return;

            var parentType = source.GetType();
            var childType = other.GetType();

            if (!TypeProperties.TryGetValue(parentType, out Dictionary<string, PropertyInfo>? parentProperties))
            {
                parentProperties = parentType.GetProperties().Where(p => p.PropertyType.IsPrimitive() && !IgnoredProps.Contains(p.Name)).ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);
                TypeProperties.Add(parentType, parentProperties);
            }

            if (!TypeProperties.TryGetValue(childType, out Dictionary<string, PropertyInfo>? childProperties))
            {
                childProperties = childType.GetProperties().Where(p => p.PropertyType.IsPrimitive() && !IgnoredProps.Contains(p.Name)).ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);
                TypeProperties.Add(childType, childProperties);
            }

            foreach (var prop in parentProperties)
            {
                if (ignorePropsPredicate?.Invoke(prop.Value) ?? false)
                    continue;

                if (!childProperties.TryGetValue(prop.Key, out PropertyInfo? childProp))
                    continue;

                if (!childProp.CanWrite)
                    continue;

                var parentProp = prop.Value;

                if (!parentProp.CanRead)
                    continue;

                if ((Nullable.GetUnderlyingType(parentProp.PropertyType) ?? parentProp.PropertyType) != (Nullable.GetUnderlyingType(childProp.PropertyType) ?? childProp.PropertyType))
                    continue;

                var parentValue = parentProp.GetValue(source);

                var isChildNullable = Nullable.GetUnderlyingType(childProp.PropertyType) != null;

                if (parentValue is null && !isChildNullable)
                    continue;

                childProp.SetValue(other, parentValue is null ? null : Convert.ChangeType(parentValue, childProp.PropertyType));
            }
        }
    }
}
