namespace ObservableTable.Core;

internal static class Helper
{
    internal static IList<T?> PadRight<T>(this IList<T?> list, int resultantLength)
    {
        var output = new T?[resultantLength];
        list.CopyTo(output, 0);
        return output;
    }

    internal static IEnumerable<Cell<T>> ListCells<T>(this ObservableTable<T> table)
    {
        for (int row = 0; row < table.Records.Count; row++)
        {
            for (int column = 0; column < table.Headers.Count; column++)
            {
                yield return new(row, column, table.Records[row][column]);
            }
        }
    }
}
