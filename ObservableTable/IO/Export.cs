using ObservableTable.Core;

namespace ObservableTable.IO;

public static class Exporter
{
    private static string ConcatenateList(IEnumerable<string?> list)
    {
        if (!list.Any()) { return ""; }
        return '"' + string.Join("\",\"", list) + '"';
    }

    public static string ToCsvString(ObservableTable<string> table, bool hasHeader = true)
    {
        var output = ToIEnumerable(table, hasHeader);
        return string.Join(Environment.NewLine, output);
    }

    public static IEnumerable<string> ToIEnumerable(ObservableTable<string> table, bool hasHeader)
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

    public static void ToFile(string path, ObservableTable<string> table, bool hasHeader = true)
    {
        var content = ToCsvString(table, hasHeader);
        File.WriteAllText(path, content);
    }
}
