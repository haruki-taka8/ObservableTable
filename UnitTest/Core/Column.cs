<<<<<<< HEAD
﻿using ObservableTable.Core;

namespace UnitTest.Core;
=======
﻿namespace UnitTest.Core;
>>>>>>> main

[TestClass]
public class Column
{
    [TestMethod]
    public void Column_IdenticalColumns_EqualOperator_True()
    {
<<<<<<< HEAD
        Column<string> expected = new("header");
        Column<string> actual = new("header");
=======
        ObservableTable.Core.Column<string> expected = new("header");
        ObservableTable.Core.Column<string> actual = new("header");
>>>>>>> main

        Assert.IsTrue(expected == actual);
    }

    [TestMethod]
    public void Column_EqualColumns_Equals_True()
    {
        // Testing the default comparator
<<<<<<< HEAD
        Column<string> expected = new("header");
        Column<string> actual = new("header");
=======
        ObservableTable.Core.Column<string> expected = new("header");
        ObservableTable.Core.Column<string> actual = new("header");
>>>>>>> main

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Column_DifferentColumnNames_EqualOperator_False()
    {
<<<<<<< HEAD
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("differentHeader", Array.Empty<string>());
=======
        ObservableTable.Core.Column<string> expected = new("header", Array.Empty<string>());
        ObservableTable.Core.Column<string> actual = new("differentHeader", Array.Empty<string>());
>>>>>>> main

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Column_DifferentColumnNames_Equals_NotEqual()
    {
<<<<<<< HEAD
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("differentHeader", Array.Empty<string>());
=======
        ObservableTable.Core.Column<string> expected = new("header", Array.Empty<string>());
        ObservableTable.Core.Column<string> actual = new("differentHeader", Array.Empty<string>());
>>>>>>> main

        Assert.AreNotEqual(expected, actual);
    }


    [TestMethod]
    public void Column_DifferentColumnValues_EqualOperator_False()
    {
<<<<<<< HEAD
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("header", new string[] { "foo", "bar" });
=======
        ObservableTable.Core.Column<string> expected = new("header", Array.Empty<string>());
        ObservableTable.Core.Column<string> actual = new("header", new string[] { "foo", "bar" });
>>>>>>> main

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Column_DifferentColumnValues_Equals_NotEqual()
    {
<<<<<<< HEAD
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("header", new string[] { "foo", "bar" });
=======
        ObservableTable.Core.Column<string> expected = new("header", Array.Empty<string>());
        ObservableTable.Core.Column<string> actual = new("header", new string[] { "foo", "bar" });
>>>>>>> main

        Assert.AreNotEqual(expected, actual);
    }
}
