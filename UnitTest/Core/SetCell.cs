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
            actual.SetCell(new CellDefinition<string>(-1, 0, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_NegativeColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell(new CellDefinition<string>(0, -1, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_OverflowRow_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell(new CellDefinition<string>(100, 0, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_OverflowColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.SetCell(new CellDefinition<string>(0, 100, "Test"))
        );
    }

    [TestMethod]
    public void SetCell_Single_ModifiedCell()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "Test", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.SetCell(new CellDefinition<string>(0, 0, "Test"));

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void SetCell_Multiple_ModifiedCells()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "Test1", "B1", "C1" },
            new string?[] { "A2", "Test2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.SetCell(
            new(0, 0, "Test1"),
            new(1, 1, "Test2")
        );

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
