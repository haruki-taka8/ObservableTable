using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Core;

[TestClass]
public class ColumnIndexOf
{
    [TestMethod]
    public void ColumnIndexOf_ExistentColumn_Index()
    {
        int expected = 1;

        var table = Helper.GetSampleTable();
        int actual = table.ColumnIndexOf(new("B0", new List<string?>() { "B1", "B2" }));

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ColumnIndexOf_ExistentColumnAtZero_Index()
    {
        int expected = 0;

        var table = Helper.GetSampleTable();
        int actual = table.ColumnIndexOf(new("A0", new List<string?>() { "A1", "A2" }));

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ColumnIndexOf_ExistentColumnAtCount_Index()
    {
        int expected = 2;

        var table = Helper.GetSampleTable();
        int actual = table.ColumnIndexOf(new("C0", new List<string?>() { "C1", "C2" }));

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ColumnIndexOf_NonexistentColumn_NegativeOne()
    {
        int expected = -1;

        var table = Helper.GetSampleTable();
        int actual = table.ColumnIndexOf(new("foo", new List<string?>() { "bar", "baz" }));

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ColumnIndexOf_ColumnLengthsMismatch_NegativeOne()
    {
        int expected = -1;

        var table = Helper.GetSampleTable();
        int actual = table.ColumnIndexOf(new("B0"));

        Assert.AreEqual(expected, actual);
    }
}
