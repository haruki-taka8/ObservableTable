using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using ObservableTable.Core;

namespace ObservableTable.IO;

public static class Importer
{
    private static readonly CsvConfiguration configuration = new(CultureInfo.InvariantCulture)
    {
        MissingFieldFound = null,
        BadDataFound = null
    };

    private static IEnumerable<IList<string?>> GetRecords(string path)
    {
        using StreamReader streamReader = new(path);
        using CsvReader csvReader = new(streamReader, configuration);
        
        while (csvReader.Read())
        {
            var row = csvReader.Parser.Record ?? Array.Empty<string>();
            yield return row;
        }
    }

    private static IEnumerable<string> FillNull(IEnumerable<string?> row)
    {
        foreach (var item in row)
        {
            yield return item ?? "";
        }
    }

    private static IEnumerable<string> GetNumberedHeaders(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return i.ToString();
        }
    }

    private static ObservableTable<string> NewTableWithHeader(IList<IList<string?>> records)
    {
        var headers = FillNull(records[0]);
        records.RemoveAt(0);

        return new(headers, records);
    }

    private static ObservableTable<string> NewTableWithoutHeader(IList<IList<string?>> records)
    {
        var headers = GetNumberedHeaders(records[0].Count);

        return new(headers, records);
    }

    public static ObservableTable<string> FromFilePath(string path, bool hasHeader = true)
    {
        var records = GetRecords(path).ToList();

        if (records.Count == 0) { return new(); }

        return hasHeader
               ? NewTableWithHeader(records)
               : NewTableWithoutHeader(records);
    }
}
