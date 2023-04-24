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

    private static IList<IList<string?>> GetRecords(CsvReader csvReader)
    {
        IList<IList<string?>> records = new List<IList<string?>>();
        while (csvReader.Read())
        {
            var thisRow = csvReader.Parser.Record?.ToList();
            if (thisRow is null) { continue; }
            records.Add(thisRow);
        }
        return records;
    }

    private static IEnumerable<string> GetHeader(IEnumerable<string?> firstRecord, bool hasHeader = true)
    {
        return hasHeader
            ? firstRecord.Select(x => x ?? "")
            : Enumerable.Range(0, firstRecord.Count()).Select(x => x.ToString());
    }

    public static ObservableTable<string> FromFilePath(string filePath, bool hasHeader = true)
    {
        using StreamReader streamReader = new(filePath);
        using CsvReader csvReader = new(streamReader, configuration);

        var records = GetRecords(csvReader);
        var headers = new List<string>();
        if (records.Count > 0)
        {
            headers = GetHeader(records[0], hasHeader).ToList();
            if (hasHeader) { records.RemoveAt(0); }
        }

        return new ObservableTable<string>(headers, records.ToArray());
    }
}
