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

    /// <summary>
    /// Read from a file and convert its content into a jagged array.
    /// </summary>
    private async static Task<IEnumerable<IList<string?>>> GetRecords(string path)
    {
        using StreamReader streamReader = new(path);
        using CsvReader csvReader = new(streamReader, configuration);

        List<IList<string?>> records = new();

        while (await csvReader.ReadAsync())
        {
            var row = csvReader.Parser.Record ?? Array.Empty<string>();
            records.Add(row);
        }

        return records;
    }

    /// <summary>
    /// Replace all instances of null with an empty string in a row.
    /// </summary>
    private static IEnumerable<string> RemoveNull(IEnumerable<string?> row)
    {
        return row.Where(x => x is not null)!;
    }

    /// <summary>
    /// Yields an enumerable of unique numbers that can be used as placeholder headers.
    /// </summary>
    private static IEnumerable<string> GetNumberedHeaders(int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(x => x.ToString());
    }

    /// <summary>
    /// Public method allowing consumers to convert a CSV file to an ObservableTable{string}. Use <see cref="FromFilePathAsync(string, bool)"/>
    /// when reading a large file.
    /// </summary>
    /// <param name="hasHeader">Whether the file has headers or not. If not, numbered headers are generated.</param>
    /// <returns></returns>
    public static ObservableTable<string> FromFilePath(string path, bool hasHeader = true)
    {
        return Task.Run(() => FromFilePathAsync(path, hasHeader)).Result;
    }


    /// <summary>
    /// Public method allowing consumers to convert a CSV file to an ObservableTable{string} asynchrnously.
    /// </summary>
    /// <param name="hasHeader">Whether the file has headers or not. If not, numbered headers are generated.</param>
    /// <returns></returns>
    public async static Task<ObservableTable<string>> FromFilePathAsync(string path, bool hasHeader = true)
    {
        var records = (await GetRecords(path)).ToList();

        var firstRow = records.FirstOrDefault() ?? new List<string?>();

        var headers = hasHeader
            ? RemoveNull(firstRow)
            : GetNumberedHeaders(firstRow.Count);

        if (hasHeader) { records.Remove(firstRow); }

        return new(headers, records);
    }
}
