using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class RemoveColumn
{
    [TestMethod]
    public void RemoveColumn_ExistentColumn_RemovedColumn()
    {
        ObservableTable<string> expected = new(
            new string[] { "B0", "C0" },
            new string?[] { "B1", "C1" },
            new string?[] { "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.RemoveColumn(actual.Headers[0]);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RemoveColumn_ExistentColumns_RemovedColumns()
    {
        ObservableTable<string> expected = new(
            new string[] { "C0" },
            new string?[] { "C1" },
            new string?[] { "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.RemoveColumn(actual.Headers[0], actual.Headers[1]);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RemoveColumn_AllColumns_EmptyTable()
    {
        ObservableTable<string> expected = new(
            new string[] { },
            new string?[] { },
            new string?[] { }
        );

        var actual = Helper.GetSampleTable();
        actual.RemoveColumn(actual.Headers.ToArray());

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RemoveColumn_NonexistentColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.RemoveColumn("Non-existent header")
        );
    }

    [TestMethod]
    public void RemoveColumn_NonexistentColumnWithNormalColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.RemoveColumn("A0", "Non-existent header", "C0")
        );
    }

    [TestMethod]
    public void RemoveColumn_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();
        actual.RemoveColumn();

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
