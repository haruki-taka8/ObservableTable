using ObservableTable.Core;

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
        string result = hasHeader ? ConcatenateList((IList<string?>)table.Headers) : "";

        foreach (var record in table.Records)
        {
            if (string.IsNullOrEmpty(result))
            {
                result = ConcatenateList(record);
                continue;
            }
            result += Environment.NewLine + ConcatenateList(record);
        }
        return result;
    }

    public static void ToFile(string path, ObservableTable<string> table, bool hasHeader = true)
    {
        string csvString = ToCsvString(table, hasHeader);

        using StreamWriter writer = new(path);
        writer.WriteLine(csvString);
    }
}
