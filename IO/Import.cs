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

    private static List<string?[]> GetRecords(CsvReader csvReader)
    {
        List<string?[]> records = new();
        while (csvReader.Read())
        {
            var thisRow = csvReader.Parser.Record;
            records.Add(thisRow);
        }
        return records;
    }

    private static IEnumerable<string> GetHeader(string?[] firstRecord, bool hasHeader = true)
    {
        return hasHeader
            ? firstRecord.Select(x => x ?? "")
            : Enumerable.Range(0, firstRecord.Length).Select(x => x.ToString());
    }

    public static ObservableTable<string> FromFilePath(string filePath, bool hasHeader = true)
    {
        using StreamReader streamReader = new(filePath);
        using CsvReader csvReader = new(streamReader, configuration);

        var records = GetRecords(csvReader);
        var headers = GetHeader(records[0], hasHeader);
        if (hasHeader) { records.RemoveAt(0); }

        return new ObservableTable<string>(headers, records);
    }
}
