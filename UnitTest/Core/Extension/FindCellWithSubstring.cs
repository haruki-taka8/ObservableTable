using ObservableTable.Core;

namespace UnitTest.Core.Extension;

[TestClass]
public class FindCellWithSubstring
{
    [TestMethod]
    public void FindCellWithSubstring_NoRegex_AllCells_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[] { new(1, 1, table.Records[1][1]) };

        var actual = table.FindCellWithSubstring(table.Records[1][1]!);

        Assert.IsTrue(expected.SequenceEqual(actual));
    }

    [TestMethod]
    public void FindCellWithSubstring_NoRegex_AllCells_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var actual = table.FindCellWithSubstring("foobar");

        Assert.IsFalse(actual.Any());
    }

    [TestMethod]
    public void FindCellWithSubstring_NoRegex_SecondRow_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[] { new(1, 1, table.Records[1][1]) };

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellWithSubstring(table.Records[1][1]!, false, secondRow);

        Assert.IsTrue(expected.SequenceEqual(actual));
    }

    [TestMethod]
    public void FindCellWithSubstring_NoRegex_SecondRow_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellWithSubstring("foobar", false, secondRow);

        Assert.IsFalse(actual.Any());
    }


    [TestMethod]
    public void FindCellWithSubstring_Regex_AllCells_Findable_Found()
    {
        var table = Helper.GetSampleTable();
        var expected = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1])
        };

        var actual = table.FindCellWithSubstring(@"^[AB]2$", true);

        Assert.IsTrue(expected.SequenceEqual(actual));
    }

    [TestMethod]
    public void FindCellWithSubstring_Regex_AllCells_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var actual = table.FindCellWithSubstring(@"^$", true);

        Assert.IsFalse(actual.Any());
    }

    [TestMethod]
    public void FindCellWithSubstring_Regex_SecondRow_Findable_Found()
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
        var actual = table.FindCellWithSubstring(@"^[AB]2$", true, secondRow);

        Assert.IsTrue(expected.SequenceEqual(actual));
    }

    [TestMethod]
    public void FindCellWithSubstring_Regex_SecondRow_Unfindable_EmptyEnumerable()
    {
        var table = Helper.GetSampleTable();

        var secondRow = new Cell<string>[]
        {
            new(1, 0, table.Records[1][0]),
            new(1, 1, table.Records[1][1]),
            new(1, 2, table.Records[1][2])
        };
        var actual = table.FindCellWithSubstring(@"^$", true, secondRow);

        Assert.IsFalse(actual.Any());
    }
}
