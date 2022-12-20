static class IEnumerableExtensions
{
    public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, int count, Action<T> action)
    {
        foreach (var v in enumerable.Take(count))
            action(v);
        return enumerable;
    }
    public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var v in enumerable)
            action(v);
        return enumerable;
    }
    public static string AggregateToString<T>(this IEnumerable<T> enumerable)
        => enumerable.Aggregate("", (a, b) => a + b);
}