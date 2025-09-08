
namespace Utils.Extensions
{
    public static class ArrayExtensions
    {
        public static bool In<T>(this T? value, params T[] values)
            => value is string stringValue
            ? stringValue.In(StringComparison.InvariantCultureIgnoreCase, (values as string[])!)
            : values.Contains(value);

        public static bool In(this string? value, StringComparison comparison, params string[] values)
            => values.Any(v => v.Equals(value, comparison));
    }
}
