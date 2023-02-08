using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class UpdateCellInOperation
{
    /*
     * UpdateCellInOperation() must return a deep-copied Operation<T>
     * (hence the reference check must satisfy !expected.Equals(actual))
     */

    [TestMethod]
    public void UpdateCellInOperation_Inline_ModifiedCellAndDeepCopied()
    {
        var table = Helper.GetSampleTable();
        Operation<string> expected = new(Change.Inline, 0, true, "A1", 0);

        Operation<string> actual = new(Change.Inline, 0, true, "old", 0);
        table.UpdateCellInOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual)); // Value
        Assert.IsFalse(expected.Equals(actual));       // Reference
    }

    [TestMethod]
    public void UpdateCellInOperation_InsertRow_DeepCopied()
    {
        var table = Helper.GetSampleTable();
        Operation<string> expected = new(Change.InsertRow, 0, true, table.Records[0]);

        Operation<string> actual = new(Change.InsertRow, 0, true, table.Records[0]);
        table.UpdateCellInOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual));
        Assert.IsFalse(expected.Equals(actual));
    }

    [TestMethod]
    public void UpdateCellInOperation_RemoveRow_DeepCopied()
    {
        var table = Helper.GetSampleTable();
        Operation<string> expected = new(Change.RemoveRow, 0, true, table.Records[0]);

        Operation<string> actual = new(Change.RemoveRow, 0, true, table.Records[0]);
        table.UpdateCellInOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual));
        Assert.IsFalse(expected.Equals(actual));
    }

    [TestMethod]
    public void UpdateCellInOperation_InsertColumn_DeepCopied()
    {
        var table = Helper.GetSampleTable();
        Operation<string> expected = new(Change.InsertColumn, 0, true, "A0", new string[] { "A1", "A2" });

        Operation<string> actual = new(Change.InsertColumn, 0, true, "A0", new string[] { "A1", "A2" });
        table.UpdateCellInOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual));
        Assert.IsFalse(expected.Equals(actual));
    }

    [TestMethod]
    public void UpdateCellInOperation_RemoveColumn_DeepCopied()
    {
        var table = Helper.GetSampleTable();
        Operation<string> expected = new(Change.RemoveColumn, 0, true, "A0", new string[] { "A1", "A2" });

        Operation<string> actual = new(Change.RemoveColumn, 0, true, "A0", new string[] { "A1", "A2" });
        table.UpdateCellInOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual));
        Assert.IsFalse(expected.Equals(actual));
    }
}
