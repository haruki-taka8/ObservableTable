<<<<<<< HEAD
﻿using ObservableTable.Core;

namespace UnitTest.Core;
=======
﻿namespace UnitTest.Core;
>>>>>>> main

[TestClass]
public class Cell
{
    [TestMethod]
    public void Cell_IdenticalCells_EqualOperator_True()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(0, 0, "");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "");
>>>>>>> main

        Assert.IsTrue(expected == actual);
    }

    [TestMethod]
    public void Cell_EqualCells_Equals_True()
    {
        // Testing the default comparator
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(0, 0, "");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "");
>>>>>>> main

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Cell_DifferentCellRows_EqualOperator_False()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(1, 0, "");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(1, 0, "");
>>>>>>> main

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Cell_DifferentCellRows_Equals_NotEqual()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(1, 0, "");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(1, 0, "");
>>>>>>> main

        Assert.AreNotEqual(expected, actual);
    }

    [TestMethod]
    public void Cell_DifferentCellColumns_EqualOperator_False()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(0, 1, "");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 1, "");
>>>>>>> main

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Cell_DifferentCellColumns_Equals_NotEqual()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(0, 1, "");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 1, "");
>>>>>>> main

        Assert.AreNotEqual(expected, actual);
    }

    [TestMethod]
    public void Cell_DifferentCellValues_EqualOperator_False()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(0, 0, "foo");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "foo");
>>>>>>> main

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Cell_DifferentCellValues_Equals_NotEqual()
    {
<<<<<<< HEAD
        Cell<string> expected = new(0, 0, "");
        Cell<string> actual = new(0, 0, "foo");
=======
        ObservableTable.Core.Cell<string> expected = new(0, 0, "");
        ObservableTable.Core.Cell<string> actual = new(0, 0, "foo");
>>>>>>> main

        Assert.AreNotEqual(expected, actual);
    }
}
