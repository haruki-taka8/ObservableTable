using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class RenameColumn
{
    [TestMethod]
    public void RenameColumn_NegativeIndex_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.RenameColumn(-1, "New Header")
        );
    }

    [TestMethod]
    public void RenameColumn_ZeroIndex_RenamedHeader()
    {
        ObservableTable<string> expected = new(
            new string[] { "D0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.RenameColumn(0, "D0");

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RenameColumn_LastIndex_RenamedHeader()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "D0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.RenameColumn(actual.Headers.Count - 1, "D0");

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RenameColumn_OverflowIndex_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.RenameColumn(actual.Headers.Count, "D0")
        );
    }
}
