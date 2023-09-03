namespace ObservableTable.Core;

internal static class Helper
{
    /// <summary>
    /// Pad an <seealso cref="IList{T?}"/> <paramref name="list"/> with <typeparamref name="T"/>'s default value to length <paramref name="resultantLength"/>.
    /// </summary>
    /// <returns>
    /// The padded list.
    /// </returns>
    /// <exception cref="ArgumentException">If <paramref name="list"/> is longer than <paramref name="resultantLength"/></exception>
    internal static IList<T?> PadRight<T>(this IList<T?> list, int resultantLength)
    {
        var output = new T?[resultantLength];
        list.CopyTo(output, 0);
        return output;
    }

    /// <summary>
    /// Converts an ObservableTable to an enumerable of <seealso cref="Cell{T}"/>.
    /// </summary>
    /// <remarks>
    /// The table is flattened left-to-right, top-to-bottom. 
    /// </remarks>
    internal static IEnumerable<Cell<T>> ToCells<T>(this ObservableTable<T> table) where T : notnull
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
