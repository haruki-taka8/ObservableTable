using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Core;

[TestClass]
public class CellIndexOf
{
    [TestMethod]
    public void CellIndexOf_ExistentCell_Indices()
    {
        (int, int) expected = (1, 1);

        var table = Helper.GetSampleTable();
        (int, int) actual = table.CellIndexOf("B2");

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CellIndexOf_ExistentCellAtTopLeft_Indices()
    {
        (int, int) expected = (0, 0);

        var table = Helper.GetSampleTable();
        (int, int) actual = table.CellIndexOf("A1");

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CellIndexOf_ExistentCellAtBottomRight_Indices()
    {
        (int, int) expected = (1, 2);

        var table = Helper.GetSampleTable();
        (int, int) actual = table.CellIndexOf("C2");

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CellIndexOf_NonexistentRow_NegativeOne()
    {
       (int, int) expected = (-1, -1);

        var table = Helper.GetSampleTable();
        (int, int) actual = table.CellIndexOf("foobar");

        Assert.AreEqual(expected, actual);
    }
}
