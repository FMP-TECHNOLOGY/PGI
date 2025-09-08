namespace Utils.Extensions
{
    public static class TypeExtension
    {
        private static readonly HashSet<Type> PrimitiveTypes = new()
        {
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(char),
            typeof(string),
            typeof(DateTime),
            typeof(DateOnly),
            typeof(TimeOnly),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid),
        };

        public static bool IsPrimitive(this Type type)
        {
            var propType = Nullable.GetUnderlyingType(type) ?? type;

            return propType.IsPrimitive || propType.IsEnum || PrimitiveTypes.Contains(propType);
        }
    }

}
