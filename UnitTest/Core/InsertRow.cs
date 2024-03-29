using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class InsertRow
{
    [TestMethod]
    public void InsertRow_NegativeIndex_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.InsertRow(-1, new string?[] { "A3" })
        );
    }

    [TestMethod]
    public void InsertRow_ZeroIndex_NewRow()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A0", null, null },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.InsertRow(0, new string?[] { "A0" });

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertRow_LastIndex_NewRow()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" },
            new string?[] { "A3", null, null }
        );

        var actual = Helper.GetSampleTable();
        actual.InsertRow(actual.Records.Count, new string?[] { "A3" });

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertRow_OverflowIndex_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.InsertRow(actual.Records.Count + 1, new string?[] { "A3" })
        );
    }

    [TestMethod]
    public void InsertRow_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();
        actual.InsertRow(0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertRow_EmptyRow_NewRow()
    {
        ObservableTable<string> expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();
        actual.InsertRow(0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertRow_RowTooLong_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentException>(() =>
            actual.InsertRow(0, new string?[] { "A0", "B0", "C0", "Extra1", "Extra2" })
        );
    }

    [TestMethod]
    public void InsertRow_RowTooLongWithNormalRow_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentException>(() =>
            actual.InsertRow(0,
                new string?[] { "A0", "B0", "C0", "Extra1", "Extra2" },
                new string?[] { "A1", "B1", "C1" }
            )
        );
    }
}
