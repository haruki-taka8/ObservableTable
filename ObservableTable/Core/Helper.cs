namespace ObservableTable.Core;

internal static class Helper
{
    internal static IList<T?> PadRight<T>(this IList<T?> list, int resultantLength)
    {
        var output = new T?[resultantLength];
        list.CopyTo(output, 0);
        return output;
    }

    internal static IEnumerable<Cell<T>> ToCells<T>(this ObservableTable<T> table)
    {
        for (int row = 0; row < table.Records.Count; row++)
        {
            for (int column = 0; column < table.Headers.Count; column++)
            {
                yield return new(row, column, table.Records[row][column]);
            }
        }
    }

    internal static IEnumerable<Cell<T>> Replace<T>(this IEnumerable<Cell<T>> cells, T from, T to)
    {
        foreach (var cell in cells)
        {
            yield return Replace(cell, from, to);
        }
    }

    private static Cell<T> Replace<T>(this Cell<T> cell, T from, T to)
    {
        return Equals(from, cell.Value)
            ? new(cell.Row, cell.Column, to)
            : cell;
    }
}
