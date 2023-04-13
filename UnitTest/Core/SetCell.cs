using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class SetCell
{
    [TestMethod]
    public void SetCell_NegativeRow_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell((-1, 0, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_NegativeColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell((0, -1, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_OverflowRow_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell((100, 0, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_OverflowColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell((0, 100, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();
        actual.SetCell();

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void SetCell_Single_ModifiedCell()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "A0", "B0", "C0" },
            records: new List<string?[]>()
            {
                new string?[] { "Test", "B1", "C1" },
                new string?[] { "A2", "B2", "C2" },
            }
        );

        var actual = Helper.GetSampleTable();
        actual.SetCell((0, 0, "Test"));

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void SetCell_Multiple_ModifiedCells()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "A0", "B0", "C0" },
            records: new List<string?[]>()
            {
                new string?[] { "A1", "Test1", "C1" },
                new string?[] { "A2", "B2", "Test2" },
            }
        );

        var actual = Helper.GetSampleTable();
        actual.SetCell(
            (0, 1, "Test1"),
            (1, 2, "Test2")
        );

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
