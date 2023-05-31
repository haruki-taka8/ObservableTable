using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class Redo
{
    [TestMethod]
    public void Redo_Inline_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.Records[0][0] = "Changed";
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_InsertRow_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>());
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_RemoveRow_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveRow(actual.Records[0]);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_InsertColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertColumn(0, new Column<string>("Test"));
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_RemoveColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveColumn(actual.Headers[0]);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_ReorderColumn_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderColumn(0, 1);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_ReorderRow_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderRow(0, 1);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_InsertRows_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>(), Array.Empty<string?>(), Array.Empty<string?>());
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_RemoveRows_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveRow(actual.Records[0], actual.Records[1]);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_InsertRows_Repeated_Reverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string>());
        actual.InsertRow(1, Array.Empty<string>());

        actual.Undo();
        actual.Undo();
        actual.Redo();
        actual.Redo();
        actual.Undo();
        actual.Undo();

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_InsertColumns_Repeated_Reverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertColumn(0, new Column<string>("A", Array.Empty<string>()));
        actual.InsertColumn(1, new Column<string>("A", Array.Empty<string>()));

        actual.Undo();
        actual.Undo();
        actual.Redo();
        actual.Redo();
        actual.Undo();
        actual.Undo();

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_RenameColumn_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RenameColumn(0, "D0");
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_InsertColumns_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertColumn(0, new("F0"), new("G0"));
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_RemoveColumns_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.RemoveColumn(actual.Headers.ToArray());
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.Redo();
        actual.Redo();
        actual.Redo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_SetCell_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.SetCell(new Cell<string>(0, 0, "Test"));
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_SetCells_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.SetCell(new(0, 0, "Test1"), new(1, 1, "Test2"));
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_Mix_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>());
        actual.Records[0][0] = "Changed";
        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_Mix_SomeOperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertRow(0, Array.Empty<string?>());
        actual.Records[0][0] = "Changed";
        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsTrue(actual.UndoCount == 1);
        Assert.IsTrue(actual.RedoCount == 1);
    }

    [TestMethod]
    public void Redo_ReorderRow_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderRow(0, 1);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_ReorderColumn_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderColumn(0, 1);
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void Redo_ReorderColumns_OperationReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReorderColumn(0, 1);
        actual.ReorderColumn(0, 2);
        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));

        actual.Redo();
        actual.Redo();
        Assert.IsFalse(expected.ContentEquals(actual));

        actual.Undo();
        actual.Undo();
        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
