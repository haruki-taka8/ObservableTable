using ObservableTable.Core;

namespace ObservableTable.IO;

public static class Exporter
{
    /// <summary>
    /// Converts an enumerable to a CSV row, fully quoted.
    /// </summary>
    private static string ConcatenateList(IEnumerable<string?> list)
    {
        if (!list.Any()) { return ""; }
        return '"' + string.Join("\",\"", list) + '"';
    }

    /// <summary>
    /// Converts an ObservableTable{string} to a multi-line CSV string.
    /// </summary>
    /// <param name="hasHeader">If true, the headers of the table will be included.</param>
    public static string ToCsvString(this ObservableTable<string> table, bool hasHeader = true)
    {
        var output = ToIEnumerable(table, hasHeader);
        return string.Join(Environment.NewLine, output);
    }

    /// <summary>
    /// Converts an ObservableTable{string} to a a jagged array.
    /// </summary>
    /// <param name="hasHeader">If true, the headers of the table will be included.</param>
    public static IEnumerable<string> ToIEnumerable(this ObservableTable<string> table, bool hasHeader)
    {
        if (hasHeader)
        {
            yield return ConcatenateList(table.Headers);
        }

        foreach (var record in table.Records)
        {
            yield return ConcatenateList(record);
        }
    }

    /// <summary>
    /// Converts an ObservableTable{string} to a multi-line CSV file. Use <see cref="ToFileAsync(ObservableTable{string}, string, bool)"/> if
    /// writing the file will take a long time.
    /// </summary>
    /// <param name="hasHeader">If true, the headers of the table will be written to the file.</param>
    public static void ToFile(this ObservableTable<string> table, string path, bool hasHeader = true)
    {
        var content = ToCsvString(table, hasHeader);
        File.WriteAllText(path, content);
    }

    /// <summary>
    /// Converts an ObservableTable{string} to a multi-line CSV file asynchronously.
    /// </summary>
    /// <param name="hasHeader">If true, the headers of the table will be written to the file.</param>
    public static async Task ToFileAsync(this ObservableTable<string> table, string path, bool hasHeader = true)
    {
        var content = ToCsvString(table, hasHeader);
        await File.WriteAllTextAsync(path, content);
    }

    /// <summary>
    /// Converts an ObservableTable{string} to a multi-line CSV file.
    /// </summary>
    /// <param name="hasHeader">If true, the headers of the table will be written to the file.</param>
    [Obsolete("Use extension method .ToFile() instead. This method will be phased out in/after 2024.")]
    public static void ToFile(string path, ObservableTable<string> table, bool hasHeader = true)
    {
        table.ToFile(path, hasHeader);
    }
}
