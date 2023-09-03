namespace UnitTest.Core;

[TestClass]
public class Cell
{
    [TestMethod]
    public void Cell_IdenticalCells_EqualOperator_True()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "");

        Assert.IsTrue(expected == actual);
    }

    [TestMethod]
    public void Cell_EqualCells_Equals_True()
    {
        // Testing the default comparator
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "");

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Cell_DifferentCellRows_EqualOperator_False()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(1, 0, "");

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Cell_DifferentCellRows_Equals_NotEqual()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(1, 0, "");

        Assert.AreNotEqual(expected, actual);
    }

    [TestMethod]
    public void Cell_DifferentCellColumns_EqualOperator_False()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 1, "");

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Cell_DifferentCellColumns_Equals_NotEqual()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 1, "");

        Assert.AreNotEqual(expected, actual);
    }

    [TestMethod]
    public void Cell_DifferentCellValues_EqualOperator_False()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "foo");

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Cell_DifferentCellValues_Equals_NotEqual()
    {
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "foo");

        Assert.AreNotEqual(expected, actual);
    }
}
