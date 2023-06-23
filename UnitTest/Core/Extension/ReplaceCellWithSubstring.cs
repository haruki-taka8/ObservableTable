using ObservableTable.Core;

namespace UnitTest.Core.Extension;

[TestClass]
public class ReplaceCellWithSubstring
{
    [TestMethod]
    public void ReplaceCellWithSubstring_NoRegex_AllCells_Replaceable_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "!1", "B1", "C1" },
            new string?[] { "!2", "B2", "C2" }
        );
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellWithSubstring("A", "!");

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_NoRegex_AllCells_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellWithSubstring("foobar", "!");

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_NoRegex_FirstRow_Replaceable_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A!", "B!", "C!" },
            new string?[] { "A2", "B2", "C2" }
        );
        var actual = Helper.GetSampleTable();
        List<Cell<string>> firstRow = new()
        {
            new(0, 0, "A1"),
            new(0, 1, "B1"),
            new(0, 2, "C1")
        };

        actual.ReplaceCellWithSubstring("1", "!", false, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_NoRegex_FirstRow_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();
        List<Cell<string>> firstRow = new()
        {
            new(0, 0, "A1"),
            new(0, 1, "B1"),
            new(0, 2, "C1")
        };

        actual.ReplaceCellWithSubstring("foobar", "!", false, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_Regex_AllCells_Replaceable_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A!", "B!", "C!" },
            new string?[] { "A!", "B!", "C!" }
        );
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellWithSubstring(@"\d", "!", true);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_Regex_AllCells_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellWithSubstring(@"\s", "!", true);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_Regex_FirstRow_Replaceable_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A!", "B!", "C!" },
            new string?[] { "A2", "B2", "C2" }
        );
        var actual = Helper.GetSampleTable();
        List<Cell<string>> firstRow = new()
        {
            new(0, 0, "A1"),
            new(0, 1, "B1"),
            new(0, 2, "C1")
        };

        actual.ReplaceCellWithSubstring(@"\d", "!", true, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_Regex_FirstRow_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();
        List<Cell<string>> firstRow = new()
        {
            new(0, 0, "A1"),
            new(0, 1, "B1"),
            new(0, 2, "C1")
        };

        actual.ReplaceCellWithSubstring(@"\s", "!", true, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellWithSubstring_Regex_AllCells_Replaceable_Grouped_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "11", "11", "11" },
            new string?[] { "22", "22", "22" }
        );
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellWithSubstring(@"^\w(\d)$", "$1$1", true);

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
