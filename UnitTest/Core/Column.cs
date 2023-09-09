using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class Column
{
    [TestMethod]
    public void Column_IdenticalColumns_EqualOperator_True()
    {
        Column<string> expected = new("header");
        Column<string> actual = new("header");

        Assert.IsTrue(expected == actual);
    }

    [TestMethod]
    public void Column_EqualColumns_Equals_True()
    {
        // Testing the default comparator
        Column<string> expected = new("header");
        Column<string> actual = new("header");

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Column_DifferentColumnNames_EqualOperator_False()
    {
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("differentHeader", Array.Empty<string>());

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Column_DifferentColumnNames_Equals_NotEqual()
    {
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("differentHeader", Array.Empty<string>());

        Assert.AreNotEqual(expected, actual);
    }


    [TestMethod]
    public void Column_DifferentColumnValues_EqualOperator_False()
    {
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("header", new string[] { "foo", "bar" });

        Assert.IsTrue(expected != actual);
    }

    [TestMethod]
    public void Column_DifferentColumnValues_Equals_NotEqual()
    {
        Column<string> expected = new("header", Array.Empty<string>());
        Column<string> actual = new("header", new string[] { "foo", "bar" });

        Assert.AreNotEqual(expected, actual);
    }
}
