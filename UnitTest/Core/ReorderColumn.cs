using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class ReorderColumn
{
[TestMethod]
    public void ReorderColumn_Downward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[]  { "A0", "C0", "B0" },
            new string?[] { "A1", "C1", "B1" },
            new string?[] { "A2", "C2", "B2" }
        );

        var actual = Helper.GetSampleTable();
        actual.ReorderColumn(1, 2);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderColumn_Forward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[]  { "A0", "C0", "B0" },
            new string?[] { "A1", "C1", "B1" },
            new string?[] { "A2", "C2", "B2" }
        );

        var actual = Helper.GetSampleTable();
        actual.ReorderColumn(2, 1);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderColumn_ZeroIndex_Downward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[]  { "B0", "C0", "A0" },
            new string?[] { "B1", "C1", "A1" },
            new string?[] { "B2", "C2", "A2" }
        );

        var actual = Helper.GetSampleTable();
        actual.ReorderColumn(0, 2);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderColumn_ZeroIndex_Forward_Reordered()
    {
        ObservableTable<string> expected = new(
            new string[]  { "C0", "A0", "B0" },
            new string?[] { "C1", "A1", "B1" },
            new string?[] { "C2", "A2", "B2" }
        );

        var actual = Helper.GetSampleTable();
        actual.ReorderColumn(2, 0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void ReorderColumn_Negative_Downward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            actual.ReorderColumn(-1, 1);
        });
    }

    [TestMethod]
    public void ReorderColumn_Negative_Forward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            actual.ReorderColumn(1, -1);
        });
    }

    [TestMethod]
    public void ReorderColumn_Overflow_Backward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            actual.ReorderColumn(actual.Headers.Count, 0);
        });
    }

    [TestMethod]
    public void ReorderColumn_Overflow_Forward_Exception()
    {
        var actual = Helper.GetLongSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            actual.ReorderColumn(0, actual.Headers.Count);
        });
    }
}