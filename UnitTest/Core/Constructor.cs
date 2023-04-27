using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class Constructor
{
    [TestMethod]
    public void Ctor_Empty_Nothing()
    {
        ObservableTable<string> actual = new();

        // Check if table is empty
        Assert.IsTrue(actual.Headers.Count == 0);
        Assert.IsTrue(actual.Records.Count == 0);

        // Check if recordTransaction is true
        Assert.IsTrue(actual.UndoCount == 0);
        actual.InsertRow(0, Array.Empty<string>());
        Assert.IsTrue(actual.UndoCount == 1);
    }

    [TestMethod]
    public void Ctor_HeadersAndRecords_Inserted()
    {
        var actual = Helper.GetSampleTable();

        // Check if headers & records exists
        Assert.IsTrue(Enumerable.SequenceEqual(actual.Headers, new string[] { "A0", "B0", "C0" }));
        Assert.IsTrue(actual.Records.Count == 2);
        Assert.IsTrue(Enumerable.SequenceEqual(actual.Records[0], new string[] { "A1", "B1", "C1" }));
        Assert.IsTrue(Enumerable.SequenceEqual(actual.Records[1], new string[] { "A2", "B2", "C2" }));

        // Check if recordTransaction is true
        Assert.IsTrue(actual.UndoCount == 0);
        actual.InsertRow(0, Array.Empty<string>());
        Assert.IsTrue(actual.UndoCount == 1);
    }
}
