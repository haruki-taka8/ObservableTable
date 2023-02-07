namespace ObservableTable.Core;

internal static class Extension
{
    internal static IList<T?> SetWidth<T>(this IList<T?> list, int resultantLength)
    {
        if (list.Count > resultantLength)
        {
            return list.SkipLast(list.Count - resultantLength).ToList();
        }

        var padding = Enumerable.Repeat<T?>(default, resultantLength - list.Count);
        return list.Concat(padding).ToList();
    }
}
