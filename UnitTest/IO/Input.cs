using ObservableTable.Core;
using ObservableTable.IO;
using System.Collections.ObjectModel;

namespace UnitTest.IO;

[TestClass]
public class Input
{
    private readonly static string CsvFolder = Path.GetFullPath("../../../Assets/");
    private static readonly string[] ColumnPrefixes = { "A", "B", "C", "D", "E", "F" };
    private static readonly string[] FirstRowInCsvFile = { "A0", "B0", "C0", "D0", "E0", "F0" };
    private static readonly string[] RowSuffixes = { "0", "1", "2", "3", "4", "5" };

    private static void TestHeader(IList<string> expected, IList<string> actual)
    {
        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    private static void TestRecords(ObservableCollection<ObservableCollection<string?>> records, int startingRowIndex, int expectedRecordsCount)
    {
        Assert.AreEqual(expectedRecordsCount, records.Count);

        for (int i = 0; i < records.Count; i++)
        {
            var expectedRow = ColumnPrefixes.Select(x => x + (i + startingRowIndex).ToString());
            Assert.IsTrue(Enumerable.SequenceEqual(expectedRow, records[i]));
        }
    }

    [TestMethod]
    public void FromFilePath_Quoted_HasHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv");

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 1, 5);
    }

    [TestMethod]
    public void FromFilePath_Quoted_HasNoHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 6);
    }

    [TestMethod]
    public void FromFilePath_Unquoted_HasHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Unquoted.csv");

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 1, 5);
    }

    [TestMethod]
    public void FromFilePath_Unquoted_HasNoHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Unquoted.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 6);
    }

    [TestMethod]
    public void FromFilePath_Single_HasHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", true);

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 0, 0);
    }

    [TestMethod]
    public void FromFilePath_Single_HasNoHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 1);
    }

    [TestMethod]
    public void FromFilePath_Empty_HasHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", true);

        Assert.AreEqual(0, table.Headers.Count);
        Assert.AreEqual(0, table.Records.Count);
    }

    [TestMethod]
    public void FromFilePath_Empty_HasNoHeaders()
    {
        ObservableTable<string> table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", false);

        Assert.AreEqual(0, table.Headers.Count);
        Assert.AreEqual(0, table.Records.Count);
    }
    
    [TestMethod]
    public async Task FromFilePathAsync_Quoted_HasHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Quoted.csv");

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 1, 5);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Quoted_HasNoHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Quoted.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 6);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Unquoted_HasHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Unquoted.csv");

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 1, 5);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Unquoted_HasNoHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Unquoted.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 6);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Single_HasHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Single.csv", true);

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 0, 0);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Single_HasNoHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Single.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 1);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Empty_HasHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Empty.csv", true);

        Assert.AreEqual(0, table.Headers.Count);
        Assert.AreEqual(0, table.Records.Count);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Empty_HasNoHeaders()
    {
        ObservableTable<string> table = await ObservableTableImporter.FromFilePathAsync(CsvFolder + "Empty.csv", false);

        Assert.AreEqual(0, table.Headers.Count);
        Assert.AreEqual(0, table.Records.Count);
    }
    
    [TestMethod]
    public async Task FromFilePathAsync_Quoted_HasHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Quoted.csv");

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 1, 5);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Quoted_HasNoHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Quoted.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 6);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Unquoted_HasHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Unquoted.csv");

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 1, 5);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Unquoted_HasNoHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Unquoted.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 6);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Single_HasHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Single.csv", true);

        TestHeader(table.Headers, FirstRowInCsvFile);
        TestRecords(table.Records, 0, 0);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Single_HasNoHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Single.csv", false);

        TestHeader(table.Headers, RowSuffixes);
        TestRecords(table.Records, 0, 1);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Empty_HasHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Empty.csv", true);

        Assert.AreEqual(0, table.Headers.Count);
        Assert.AreEqual(0, table.Records.Count);
    }

    [TestMethod]
    public async Task FromFilePathAsync_Empty_HasNoHeaders()
    {
        ObservableTable<string> table = await Importer.FromFilePathAsync(CsvFolder + "Empty.csv", false);

        Assert.AreEqual(0, table.Headers.Count);
        Assert.AreEqual(0, table.Records.Count);
    }
}
