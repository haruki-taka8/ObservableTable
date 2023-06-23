using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class FindCellWithSubstring
{
    [TestMethod]
    public void FindCellsWithSubstring_NoRegex_AllCells_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[] { new(1, 1, table.Records[1][1]) };

        var actual = table.FindCellsWithSubstring(table.Records[1][1]!);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void FindCellsWithSubstring_NoRegex_AllCells_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var actual = table.FindCellsWithSubstring("foobar");

        Assert.IsFalse(actual.Any());
    }

    [TestMethod]
    public void FindCellsWithSubstring_NoRegex_SecondRow_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[] { new(1, 1, table.Records[1][1]) };

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellsWithSubstring(table.Records[1][1]!, false, secondRow);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void FindCellsWithSubstring_NoRegex_SecondRow_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellsWithSubstring("foobar", false, secondRow);

        Assert.IsFalse(actual.Any());
    }


    [TestMethod]
    public void FindCellsWithSubstring_Regex_AllCells_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[]
        { 
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1])
        };

        var actual = table.FindCellsWithSubstring(@"^[AB]2$", true);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void FindCellsWithSubstring_Regex_AllCells_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var actual = table.FindCellsWithSubstring(@"^$", true);

        Assert.IsFalse(actual.Any());
    }

    [TestMethod]
    public void FindCellsWithSubstring_Regex_SecondRow_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1])
        };

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellsWithSubstring(@"^[AB]2$", true, secondRow);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void FindCellsWithSubstring_Regex_SecondRow_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellsWithSubstring(@"^$", true, secondRow);

        Assert.IsFalse(actual.Any());
    }
}
