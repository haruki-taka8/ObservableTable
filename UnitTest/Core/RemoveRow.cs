using ObservableTable.Core;
using System.Collections.ObjectModel;

namespace UnitTest.Core;

[TestClass]
public class RemoveRow
{
    [TestMethod]
    public void RemoveRow_ExistentRow_RowRemoved()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "A0", "B0", "C0" },
            records: new string?[] { "A2", "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();
        actual.RemoveRow(actual.Records[0]);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RemoveRow_ExistentRows_RowsRemoved()
    {
        ObservableTable<string> expected = new(
            headers: new string[] { "A0", "B0", "C0" }
        );

        var actual = Helper.GetSampleTable();
        actual.RemoveRow(actual.Records[0], actual.Records[1]);

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RemoveRow_NonexistentRow_Exception()
    {
        var expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => actual.RemoveRow(new ObservableCollection<string?>())
        );
    }

    [TestMethod]
    public void RemoveRow_NonAndExistentRow_Exception()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A2", "B2", "C2" }
        );

        var actual = Helper.GetSampleTable();

        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => actual.RemoveRow(actual.Records[0], new ObservableCollection<string?>())
        );
    }

    [TestMethod]
    public void RemoveRow_AllRows_EmptyTable()
    {
        ObservableTable<string> expected = new(
            new string[] { "A0", "B0", "C0" }
        );

        var actual = Helper.GetSampleTable();
        actual.RemoveRow(actual.Records.ToArray());

        Assert.IsTrue(expected.ContentEquals(actual));
    }

    [TestMethod]
    public void RemoveRow_Nothing_Nothing()
    {
        var expected = Helper.GetSampleTable();

        var actual = Helper.GetSampleTable();
        actual.RemoveRow();

        Assert.IsTrue(expected.ContentEquals(actual));
    }
}
