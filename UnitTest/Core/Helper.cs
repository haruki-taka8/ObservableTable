using ObservableTable.Core;

namespace UnitTest.Core;

internal static class Helper
{
    internal static ObservableTable<string> GetSampleTable()
    {
        return new ObservableTable<string>(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" }
        );
    }
}
