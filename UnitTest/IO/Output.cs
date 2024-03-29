using ObservableTable.IO;

namespace UnitTest.IO;

[TestClass]
public class Output
{
    private readonly static string CsvFolder = Path.GetFullPath("../../../Assets/");
    private static readonly string FirstRowInCsvFile = @"""A0"",""B0"",""C0"",""D0"",""E0"",""F0""";
    private static readonly string expectedTableString = @"""A0"",""B0"",""C0"",""D0"",""E0"",""F0""
""A1"",""B1"",""C1"",""D1"",""E1"",""F1""
""A2"",""B2"",""C2"",""D2"",""E2"",""F2""
""A3"",""B3"",""C3"",""D3"",""E3"",""F3""
""A4"",""B4"",""C4"",""D4"",""E4"",""F4""
""A5"",""B5"",""C5"",""D5"",""E5"",""F5""";

    private static void AssertTable(string actualTableString)
    {
        Assert.AreEqual(expectedTableString, actualTableString, false);
    }

    private static void AssertTableEnumerable(IEnumerable<string> actualTableString)
    {
        var joined = string.Join(Environment.NewLine, actualTableString);
        Assert.AreEqual(expectedTableString, joined, false);
    }

    [TestMethod]
    public void ToCsvString_Quoted_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", true);

        var actualTableString = ObservableTableExporter.ToCsvString(table, true);

        AssertTable(actualTableString);
    }

    [TestMethod]
    public void ToCsvString_Quoted_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", false);

        var actualTableString = ObservableTableExporter.ToCsvString(table, false);

        AssertTable(actualTableString);
    }

    [TestMethod]
    public void ToCsvString_Unquoted_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Unquoted.csv", true);

        var actualTableString = ObservableTableExporter.ToCsvString(table, true);

        AssertTable(actualTableString);
    }

    [TestMethod]
    public void ToCsvString_Unquoted_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Unquoted.csv", false);

        var actualTableString = ObservableTableExporter.ToCsvString(table, false);

        AssertTable(actualTableString);
    }

    [TestMethod]
    public void ToCsvString_Single_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", true);

        var actualTableString = ObservableTableExporter.ToCsvString(table, true);

        Assert.AreEqual(FirstRowInCsvFile, actualTableString, false);
    }

    [TestMethod]
    public void ToCsvString_Single_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", false);

        var actualTableString = ObservableTableExporter.ToCsvString(table, false);

        Assert.AreEqual(FirstRowInCsvFile, actualTableString, false);
    }

    [TestMethod]
    public void ToCsvString_Empty_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", true);

        var actualTableString = ObservableTableExporter.ToCsvString(table, true);

        Assert.IsTrue(
            string.IsNullOrEmpty(actualTableString),
            string.Format("Expected string \"\"; got {0}", actualTableString)
        );
    }

    [TestMethod]
    public void ToCsvString_Empty_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", false);

        var actualTableString = ObservableTableExporter.ToCsvString(table, false);

        Assert.IsTrue(
            string.IsNullOrEmpty(actualTableString),
            string.Format("Expected string \"\"; got {0}", actualTableString)
        );
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Quoted_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", true);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, true);

        AssertTableEnumerable(actualTableString);
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Quoted_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", false);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, false);

        AssertTableEnumerable(actualTableString);
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Unquoted_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Unquoted.csv", true);

        var actualTableString = ObservableTableExporter.ToCsvString(table, true);

        AssertTable(actualTableString);
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Unquoted_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Unquoted.csv", false);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, false);

        AssertTableEnumerable(actualTableString);
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Single_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", true);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, true);

        Assert.IsTrue(actualTableString.Any());
        Assert.AreEqual(FirstRowInCsvFile, actualTableString.First(), false);
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Single_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", false);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, false);

        Assert.IsTrue(actualTableString.Any());
        Assert.AreEqual(FirstRowInCsvFile, actualTableString.First(), false);
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Empty_HasHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", true);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, true);

        Assert.IsTrue(
            string.IsNullOrEmpty(string.Join(Environment.NewLine, actualTableString)),
            string.Format("Expected string \"\"; got {0}", actualTableString)
        );
    }

    [TestMethod]
    public void ToCsvStringEnumerable_Empty_HasNoHeaders()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", false);

        var actualTableString = ObservableTableExporter.ToIEnumerable(table, false);

        Assert.IsTrue(
            string.IsNullOrEmpty(string.Join(Environment.NewLine, actualTableString)),
            string.Format("Expected string \"\"; got {0}", actualTableString)
        );
    }

    [TestMethod]
    public void ToFile_HasHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", true);
        table.ToFile("temp.csv", true);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", true);

        Assert.IsTrue(table.ContentEquals(newTable));
    }

    [TestMethod]
    public void ToFile_HasNoHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", false);
        table.ToFile("temp.csv", false);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", false);

        Assert.IsTrue(table.ContentEquals(newTable));
    }

    [TestMethod]
    public void ToFile_Single_HasHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", true);
        table.ToFile("temp.csv", true);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", true);

        Assert.IsTrue(table.ContentEquals(newTable));
    }

    [TestMethod]
    public void ToFile_Single_HasNoHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", false);
        table.ToFile("temp.csv", false);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", false);

        Assert.IsTrue(table.ContentEquals(newTable));
    }

    [TestMethod]
    public void ToFile_Empty_HasHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", true);
        table.ToFile("temp.csv", true);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", true);

        Assert.IsTrue(table.ContentEquals(newTable));
    }

    [TestMethod]
    public void ToFile_Empty_HasNoHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", false);
        table.ToFile("temp.csv", false);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", false);

        Assert.IsTrue(table.ContentEquals(newTable));
    }

    [TestMethod]
    public async Task ToFileAsync_HasHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", true);
        await table.ToFileAsync("temp.csv", true);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", true);
    
        Assert.IsTrue(table.ContentEquals(newTable));
    }
    
    [TestMethod]
    public async Task ToFile_HasNoHeaderAsync()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Quoted.csv", false);
        await table.ToFileAsync("temp.csv", false);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", false);
    
        Assert.IsTrue(table.ContentEquals(newTable));
    }
    
    [TestMethod]
    public async Task ToFileAsync_Single_HasHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", true);
        await table.ToFileAsync("temp.csv", true);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", true);
    
        Assert.IsTrue(table.ContentEquals(newTable));
    }
    
    [TestMethod]
    public async Task ToFileAsync_Single_HasNoHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Single.csv", false);
        await table.ToFileAsync("temp.csv", false);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", false);
    
        Assert.IsTrue(table.ContentEquals(newTable));
    }
    
    [TestMethod]
    public async Task ToFileAsync_Empty_HasHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", true);
        await table.ToFileAsync("temp.csv", true);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", true);
    
        Assert.IsTrue(table.ContentEquals(newTable));
    }
    
    [TestMethod]
    public async Task ToFileAsync_Empty_HasNoHeader()
    {
        var table = ObservableTableImporter.FromFilePath(CsvFolder + "Empty.csv", false);
        await table.ToFileAsync("temp.csv", false);
        var newTable = ObservableTableImporter.FromFilePath("temp.csv", false);
    
        Assert.IsTrue(table.ContentEquals(newTable));
    }
}
