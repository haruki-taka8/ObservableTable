namespace ObservableTable.Core;

internal static class Extension
{
    internal static IList<T?> PadRight<T>(this IList<T?> list, int resultantLength)
    {
        if (list.Count > resultantLength)
        {
            throw new ArgumentException("Input list longer than desired resultant length", nameof(list));
        }

        var output = new T?[resultantLength];
        list.CopyTo(output, 0);
        return output;
    }
}
