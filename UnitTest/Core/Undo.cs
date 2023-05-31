using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class Undo
{
    [TestMethod]
    public void Undo_Inline_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.Records[0][0] = "Changed";
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_InsertRow_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>());
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_RemoveRow_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveRow(actual.Records[0]);
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_InsertColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertColumn(0, new Column<string>("Test"));
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_RemoveColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveColumn(actual.Headers[0]);
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_InsertRows_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>(), Array.Empty<string?>(), Array.Empty<string?>());
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_RenameColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RenameColumn(0, "D0");
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_RemoveRows_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveRow(actual.Records[0], actual.Records[1]);
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_InsertColumns_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertColumn(0, new("F0"), new("G0"));
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_RemoveColumns_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveColumn(actual.Headers.ToArray());
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.Undo();
        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_SetCell_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.SetCell(new Cell<string>(0, 0, "Test"));
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_SetCells_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.SetCell(new(0, 0, "Test1"), new(1, 1, "Test2"));
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_Mix_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>());
        actual.Records[0][0] = "Temp";
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_Mix_SomeOperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>());
        actual.Records[0][0] = "Temp";
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsFalse(expected.ContentEquals(actual));
        Assert.IsTrue(actual.UndoCount == 1);
        Assert.IsTrue(actual.RedoCount == 1);
    }

    [TestMethod]
    public void Undo_ReorderRow_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderRow(0, 1);
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_ReorderColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderColumn(0, 1);
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Undo_ReorderColumns_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderColumn(0, 1);
        actual.ReorderColumn(1, 2);
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
