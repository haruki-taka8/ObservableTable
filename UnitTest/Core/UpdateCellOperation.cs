using ObservableTable.Core;
using System.Net.Http.Headers;

namespace UnitTest.Core;

[TestClass]
public class UpdateCellOperation
{
    /*
     * UpdateCellOperation() must return a deep-copied CellOperation<T>
     * (hence the reference check must satisfy !expected.Equals(actual))
     */

    [TestMethod]
    public void UpdateCellOperation_ModifiedCell_ModifiedCellAndDeepCopied()
    {
        var table = Helper.GetSampleTable();
        CellEdit<string> expected = new(0, new(0, 0, "A1"));

        CellEdit<string> actual = new(0, new(0, 0, "old"));
        table.UpdateCellOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual)); // Value
        Assert.IsFalse(expected.Equals(actual));       // Reference
    }

    [TestMethod]
    public void UpdateCellOperation_NoChange_DeepCopiedOnly()
    {
        var table = Helper.GetSampleTable();
        CellEdit<string> expected = new(0, new(0, 0, "A1"));

        CellEdit<string> actual = new(0, new(0, 0, "A1"));
        table.UpdateCellOperation(actual);

        Assert.IsTrue(expected.ContentEquals(actual)); // Value
        Assert.IsFalse(expected.Equals(actual));       // Reference
    }
}
