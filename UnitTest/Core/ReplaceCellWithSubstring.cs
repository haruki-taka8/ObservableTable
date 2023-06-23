﻿using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class ReplaceCellWithSubstring
{
    [TestMethod]
    public void ReplaceCellSubstring_NoRegex_AllCells_Replaceable_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "!1", "B1", "C1" },
            new string?[] { "!2", "B2", "C2" }
        );
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellsWithSubstring("A", "!");

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_NoRegex_AllCells_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellsWithSubstring("foobar", "!");

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_NoRegex_FirstRow_Replaceable_Replaced()
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

        actual.ReplaceCellsWithSubstring("1", "!", false, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_NoRegex_FirstRow_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();
        List<Cell<string>> firstRow = new()
        {
            new(0, 0, "A1"),
            new(0, 1, "B1"),
            new(0, 2, "C1")
        };

        actual.ReplaceCellsWithSubstring("foobar", "!", false, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_Regex_AllCells_Replaceable_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A!", "B!", "C!" },
            new string?[] { "A!", "B!", "C!" }
        );
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellsWithSubstring(@"\d", "!", true);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_Regex_AllCells_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellsWithSubstring(@"\s", "!", true);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_Regex_FirstRow_Replaceable_Replaced()
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

        actual.ReplaceCellsWithSubstring(@"\d", "!", true, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_Regex_FirstRow_Irreplaceable_Nothing()
    {
        var expected = Helper.GetSampleTable();
        var actual = Helper.GetSampleTable();
        List<Cell<string>> firstRow = new()
        {
            new(0, 0, "A1"),
            new(0, 1, "B1"),
            new(0, 2, "C1")
        };

        actual.ReplaceCellsWithSubstring(@"\s", "!", true, firstRow);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReplaceCellSubstring_Regex_AllCells_Replaceable_Grouped_Replaced()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "11", "11", "11" },
            new string?[] { "22", "22", "22" }
        );
        var actual = Helper.GetSampleTable();

        actual.ReplaceCellsWithSubstring(@"^\w(\d)$", "$1$1", true);

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}