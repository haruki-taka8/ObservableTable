using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class InsertColumn
{
    [TestMethod]
    public void InsertColumn_NegativeIndex_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.InsertColumn(-1, ("D0", new List<string?>()))
        );
    }

    [TestMethod]
    public void InsertColumn_ZeroIndex_NewColumn()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "D0", "A0", "B0", "C0" },
            records: new List<string?[]>()
            {
                new string?[] { null, "A1", "B1", "C1" },
                new string?[] { null, "A2", "B2", "C2" }
            }
        );

        var actual = Helper.GetSampleTable();
        actual.InsertColumn(0, ("D0", new List<string?>()));

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertColumn_LastIndex_NewColumn()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "A0", "B0", "C0", "D0" },
            records: new List<string?[]>()
            {
                new string?[] { "A1", "B1", "C1", null },
                new string?[] { "A2", "B2", "C2", null }
            }
        );

        var actual = Helper.GetSampleTable();
        actual.InsertColumn(actual.Headers.Count, ("D0", new List<string?>()));

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertColumn_OverflowIndex_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            actual.InsertColumn(actual.Headers.Count + 1, ("D0", new List<string?>()))
        );
    }

    [TestMethod]
    public void InsertColumn_TwoColumns_NewColumns()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "D0", "E0", "A0", "B0", "C0" },
            records: new List<string?[]>()
            {
                new string?[] { null, "E1", "A1", "B1", "C1" },
                new string?[] { null, "E2", "A2", "B2", "C2" }
            }
        );

        var actual = Helper.GetSampleTable();
        actual.InsertColumn(0,
            ("D0", new List<string?>()),
            ("E0", new string[] { "E1", "E2" })
        );

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void InsertColumn_ColumnTooLong_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentException>(() =>
            actual.InsertColumn(0, ("D0", new string[] { "D1", "D2", "D3" }))
        );
    }

    [TestMethod]
    public void InsertColumn_ColumnTooLongWithNormalColumn_Exception()
    {
        var actual = Helper.GetSampleTable();
        Assert.ThrowsException<ArgumentException>(() =>
            actual.InsertColumn(0,
                ("D0", new string[] { "D1", "D2", "D3" }),
                ("E0", new string[] { "E1", "E2" })
            )
        );
    }

    [TestMethod]
    public void InsertColumn_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();
        actual.InsertColumn(0);

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
