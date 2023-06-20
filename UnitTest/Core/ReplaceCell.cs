using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class ReplaceCell
{
    [TestMethod]
    public void ReplaceCell_AllCells_Replaceable_Replaced()
    {
        ObservableTable<int> expected = new(
            new int[] { 1, 2, 3 },
            new int[] { 0, 5, 6 },
            new int[] { 7, 8, 9 }
        );
        ObservableTable<int> actual = Helper.GetSampleIntTable();

        actual.ReplaceCell(4, 0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCell_AllCells_Irreplaceable_Nothing()
    {
        ObservableTable<int> expected = Helper.GetSampleIntTable();
        ObservableTable<int> actual = Helper.GetSampleIntTable();

        actual.ReplaceCell(2763, 0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCell_FirstRow_Replaceable_Replaced()
    {
        ObservableTable<int> expected = new(
           new int[] { 1, 2, 3 },
           new int[] { 4, 0, 6 },
           new int[] { 7, 8, 9 }
        );
        ObservableTable<int> actual = Helper.GetSampleIntTable();
        List<Cell<int>> firstRow = new()
        {
            new(0, 0, 4),
            new(0, 1, 5),
            new(0, 2, 6)
        };

        actual.ReplaceCell(5, 0, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCell_FirstRow_Irreplaceable_Nothing()
    {
        ObservableTable<int> expected = Helper.GetSampleIntTable();
        ObservableTable<int> actual = Helper.GetSampleIntTable();
        List<Cell<int>> firstRow = new()
        {
            new(0, 0, 4),
            new(0, 1, 5),
            new(0, 2, 6)
        };

        actual.ReplaceCell(2763, 0, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
