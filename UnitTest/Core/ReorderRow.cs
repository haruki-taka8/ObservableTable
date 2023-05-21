using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class ReorderRow
{
    [TestMethod]
    public void ReorderRow_Downward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A3", "B3", "C3" },
            new string?[] { "A4", "B4", "C4" },
            new string?[] { "A2", "B2", "C2" },
            new string?[] { "A5", "B5", "C5" }
        );

        var actual = Helper.GetLongSampleTable();
        actual.ReorderRow(1, 3);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderRow_Forward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A4", "B4", "C4" },
            new string?[] { "A2", "B2", "C2" },
            new string?[] { "A3", "B3", "C3" },
            new string?[] { "A5", "B5", "C5" }
        );

        var actual = Helper.GetLongSampleTable();
        actual.ReorderRow(3, 1);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderRow_ZeroIndex_Downward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A2", "B2", "C2" },
            new string?[] { "A3", "B3", "C3" },
            new string?[] { "A4", "B4", "C4" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A5", "B5", "C5" }
        );

        var actual = Helper.GetLongSampleTable();
        actual.ReorderRow(0, 3);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderRow_ZeroIndex_Forward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A4", "B4", "C4" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" },
            new string?[] { "A3", "B3", "C3" },
            new string?[] { "A5", "B5", "C5" }
        );

        var actual = Helper.GetLongSampleTable();
        actual.ReorderRow(3, 0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderRow_Negative_Downward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            actual.ReorderRow(-1, 3);
        });
    }

    [TestMethod]
    public void ReorderRow_Negative_Forward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            actual.ReorderRow(3, -1);
        });
    }

    [TestMethod]
    public void ReorderRow_Overflow_Backward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            actual.ReorderRow(actual.Records.Count, 0);
        });
    }

    [TestMethod]
    public void ReorderRow_Overflow_Forward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            actual.ReorderRow(0, actual.Records.Count);
        });
    }
}