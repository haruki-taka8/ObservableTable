using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Core;

[TestClass]
public class RowIndexOf
{
    [TestMethod]
    public void RowIndexOf_ExistentRow_Index()
    {
        int expected = 1;

        var table = Helper.GetSampleTable();
        int actual = table.RowIndexOf(table.Records[1]);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RowIndexOf_ExistentRowAtZero_Index()
    {
        int expected = 0;

        var table = Helper.GetSampleTable();
        int actual = table.RowIndexOf(table.Records[0]);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RowIndexOf_NonexistentRow_NegativeOne()
    {
        int expected = -1;

        var table = Helper.GetSampleTable();
        int actual = table.RowIndexOf(new List<string?>() { "a", "b", "c" });

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RowIndexOf_RowLengthsMismatch_NegativeOne()
    {
        int expected = -1;

        var table = Helper.GetSampleTable();
        int actual = table.RowIndexOf(new List<string?>());

        Assert.AreEqual(expected, actual);
    }
}
