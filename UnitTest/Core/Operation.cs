using ObservableTable.Core;

namespace UnitTest.Core;

[TestClass]
public class Operation
{
    /*
     * DeepCopy() must return a deep-copied Operation<T>
     * (hence the reference check must satisfy !expected.Equals(actual))
     */

    /*
    [TestMethod]
    public void RowColumnCtor_IncorrectParams_ArgException()
    {
        Assert.ThrowsException<ArgumentException>(
             () => new Operation<string>(Change.Inline, 0, true, "Header", new List<string>())
        );
    }

    [TestMethod]
    public void InlineCtor_IncorrectParams_ArgException()
    {
        Assert.ThrowsException<ArgumentException>(
             () => new Operation<string>(Change.InsertColumn, 0, true, "Cell", 5)
        );
    }

    [TestMethod]
    public void DeepCopy_DeepCopied()
    {
        var expected = new Operation<string>(Change.Inline, 0, true, "Cell", 0);
        var actual = expected.DeepCopy();

        Assert.IsTrue(expected.ContentEquals(actual)); // Value
        Assert.IsFalse(expected.Equals(actual));       // Reference
    }
    */
}
