using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class EventHandler
{
    [TestMethod]
    public void EventHandler_SetCell_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.SetCell(new Cell<string>(0, 0, "A1"));
        Assert.IsTrue(modified);
    }


    [TestMethod]
    public void EventHandler_RenameColumn_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.RenameColumn(0, "H0");
        Assert.IsTrue(modified);
    }

    [TestMethod]
    public void EventHandler_InsertRow_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.InsertRow(0, new List<string?>());
        Assert.IsTrue(modified);
    }

    [TestMethod]
    public void EventHandler_RemoveRow_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.RemoveRow(table.Records[0]);
        Assert.IsTrue(modified);
    }

    [TestMethod]
    public void EventHandler_InsertColumn_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.InsertColumn(0, new Column<string>("A0"));
        Assert.IsTrue(modified);
    }

    [TestMethod]
    public void EventHandler_RemoveColumn_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.RemoveColumn("A0");
        Assert.IsTrue(modified);
    }

    [TestMethod]
    public void EventHandler_Undo_Notified()
    {
        var table = Helper.GetSampleTable();
        int modified = 0;

        table.TableModified += (object? sender, EventArgs e) => modified++;

        table.RenameColumn(0, "H0");
        table.Undo();
        Assert.IsTrue(modified == 2);
    }

    [TestMethod]
    public void EventHandler_Redo_Notified()
    {
        var table = Helper.GetSampleTable();
        int modified = 0;

        table.TableModified += (object? sender, EventArgs e) => modified++;

        table.RenameColumn(0, "H0");
        table.Undo();
        table.Redo();
        Assert.IsTrue(modified == 3);
    }
}
