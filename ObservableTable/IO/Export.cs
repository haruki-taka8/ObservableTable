using ObservableTable.Core;
using System.Text;

namespace ObservableTable.IO;

public static class Exporter
{
    private static string ConcatenateList(IList<string?> list)
    {
        if (list.Count == 0) { return ""; }
        return '"' + string.Join("\",\"", list) + '"';
    }

    public static string ToCsvString(ObservableTable<string> table, bool hasHeader = true)
    {
        StringBuilder builder = new();

        if (hasHeader)
        { 
            builder.AppendLine(ConcatenateList((IList<string?>)table.Headers));
        }

        foreach (var record in table.Records)
        {
            builder.AppendLine(ConcatenateList(record));
        }

        return builder.ToString().Trim();
    }

    public static IEnumerable<string> ToCsvStringEnumerable(ObservableTable<string> table, bool hasHeader)
    {
        List<string> output = new();

        if (hasHeader)
        {
            output.Add(ConcatenateList((IList<string?>)table.Headers));
        }

        foreach (var record in table.Records)
        {
            output.Add(ConcatenateList(record));
        }

        return output;
    }

    public static void ToFile(string path, ObservableTable<string> table, bool hasHeader = true)
    {
        var content = ToCsvString(table, hasHeader);
        File.WriteAllText(path, content);
    }
}
