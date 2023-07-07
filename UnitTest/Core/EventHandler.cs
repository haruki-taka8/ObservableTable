using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class EventHandler
{
    private readonly static ObservableTable<string> original = Helper.GetSampleTable();

    private void SingleHandler(object? sender, EditEventArgs e)
    {
        var table = (ObservableTable<string>)sender!;
        table.TableModified -= SingleHandler;

        Assert.AreEqual(e.Parity, 0);

        e.Undo();
        Assert.IsTrue(original.ContentEquals(table));

        e.Redo();
        Assert.IsFalse(original.ContentEquals(table));

        e.Undo();
        Assert.IsTrue(original.ContentEquals(table));
    }

    [TestMethod]
    public void EventHandler_SetCell_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.SetCell(new Cell<string>(0, 0, "A1!"));
    }


    [TestMethod]
    public void EventHandler_RenameColumn_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.RenameColumn(0, "H0");
    }

    [TestMethod]
    public void EventHandler_InsertRow_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.InsertRow(0, new List<string?>());
    }

    [TestMethod]
    public void EventHandler_RemoveRow_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.RemoveRow(actual.Records[0]);
    }

    [TestMethod]
    public void EventHandler_InsertColumn_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.InsertColumn(0, new Column<string>("A0"));
    }

    [TestMethod]
    public void EventHandler_RemoveColumn_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.RemoveColumn("A0");
    }

    [TestMethod]
    public void EventHandler_ReorderRow_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.ReorderRow(0,1);
    }

    [TestMethod]
    public void EventHandler_ReorderColumn_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.ReorderColumn(0, 1);
    }

    [TestMethod]
    public void EventHandler_ReplaceCell_Notified()
    {
        var actual = Helper.GetSampleTable();

        actual.TableModified += SingleHandler;
        actual.ReplaceCell(actual.Records[0][0]!, "");
    }

    [TestMethod]
    public void EventHandler_Undo_Notified()
    {
        static void Undo_SingleHandler(object? sender, EditEventArgs e)
        {
            var table = (ObservableTable<string>)sender!;
            table.TableModified -= Undo_SingleHandler;

            Assert.AreEqual(e.Parity, 0);

            e.Invoke(true);
            Assert.IsFalse(original.ContentEquals(table));

            e.Invoke(false);
            Assert.IsTrue(original.ContentEquals(table));

            e.Invoke(true);
            Assert.IsFalse(original.ContentEquals(table));
        }

        var actual = Helper.GetSampleTable();
        actual.RenameColumn(0, "H0");

        actual.TableModified += Undo_SingleHandler;
        actual.Undo();
    }

    [TestMethod]
    public void EventHandler_Redo_Notified()
    {
        var actual = Helper.GetSampleTable();
        actual.RenameColumn(0, "H0");
        actual.Undo();

        actual.TableModified += SingleHandler;
        actual.Redo();
    }


    [TestMethod]
    public void EventHandler_Parity_Notified()
    {
        int[] expectedParity = new[] { 0, 1 };

        var actual = Helper.GetSampleTable();
        List<int> parity = new();

        actual.TableModified += (object? sender, EditEventArgs e) => parity.Add(e.Parity);
        List<List<string?>> rows = new() { new(), new() };
        actual.InsertRow(0, rows);

        Assert.IsTrue(Enumerable.SequenceEqual(parity, expectedParity));
    }
}
