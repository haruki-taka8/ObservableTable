using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class FindCell
{
    [TestMethod]
    public void FindCell_AllCells_Findable_FoundList()
    {
        var table = Helper.GetSampleIntTable();
        var expected = new Cell<int>[] { new(0, 0, table.Records[0][0]) };

        var actual = table.FindCell(4);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void FindCell_AllCells_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleIntTable();
        var actual = table.FindCell(-1);

        Assert.IsFalse(actual.Any());
    }

    [TestMethod]
    public void FindCell_FirstRow_Findable_FoundList()
    {
        var table = Helper.GetSampleIntTable();
        var expected = new Cell<int>[] { new(0, 1, table.Records[0][1]) };

        var actual = table.FindCell(table.Records[0][1]);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void FindCell_FirstRow_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleIntTable();
        var actual = table.FindCell(-1);

        Assert.IsFalse(actual.Any());
    }
}
