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

        actual.InsertColumn(0, ("Test", Array.Empty<string>()));
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
    public void Redo_InsertColumns_OperationsReverted()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.InsertColumn(0, ("F0", Array.Empty<string>()), ("G0", Array.Empty<string>()));
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
}
