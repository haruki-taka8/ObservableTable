namespace ObservableTable.Core;

internal static class Extension
{
    internal static IList<T?> PadRight<T>(this IList<T?> list, int resultantLength)
    {
        var padding = Enumerable.Repeat<T?>(default, resultantLength - list.Count);
        return list.Concat(padding).ToList();
    }
}
