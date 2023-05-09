using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class EventHandler
{
    [TestMethod]
    public void EventHandler_Subscribed_Notified()
    {
        var table = Helper.GetSampleTable();
        var modified = false;

        table.TableModified += (object? sender, EventArgs e) => modified = true;

        table.SetCell(new Cell<string>(0, 0, "A1"));
        Assert.IsTrue(modified);
    }
}
