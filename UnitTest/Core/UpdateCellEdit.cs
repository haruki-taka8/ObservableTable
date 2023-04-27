using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class UpdateCellEdit
{
    [TestMethod]
    public void UpdateCellEdit_ModifiedCell_ModifiedCellEdit()
    {
        var table = Helper.GetSampleTable();
        CellEdit<string> expected = new(0, 0, 0, "A1");

        CellEdit<string> actual = new(0, 0, 0, "old");
        var output = (CellEdit<string>)table.UpdateCellEdit(actual);

        Assert.IsTrue(expected.ContentEquals(output)); // Value
        Assert.IsFalse(actual.Equals(output));         // Reference
    }

    [TestMethod]
    public void UpdateCellEdit_NoChange_Original()
    {
        var table = Helper.GetSampleTable();
        CellEdit<string> expected = new(0, 0, 0, "A1");

        CellEdit<string> actual = new(0, 0, 0, "A1");
        var output = (CellEdit<string>)table.UpdateCellEdit(actual);

        Assert.IsTrue(expected.ContentEquals(output)); // Value
        Assert.IsFalse(actual.Equals(output));         // Reference
    }

    [TestMethod]
    public void UpdateCellEdit_RowEdit_Original()
    {
        var table = Helper.GetSampleTable();
        RowEdit<string> actual = new(0, true, 0, new Row<string>());
        var output = table.UpdateCellEdit(actual);

        Assert.IsTrue(output is RowEdit<string>); // Type
        Assert.IsFalse(actual.Equals(output));    // Reference
    }

    [TestMethod]
    public void UpdateCellEdit_ColumnEdit_Original()
    {
        var table = Helper.GetSampleTable();
        ColumnEdit<string> actual = new(0, true, 0, "", new List<string?>());
        var output = table.UpdateCellEdit(actual);

        Assert.IsTrue(output is ColumnEdit<string>); // Type
        Assert.IsFalse(actual.Equals(output));       // Reference
    }
}
