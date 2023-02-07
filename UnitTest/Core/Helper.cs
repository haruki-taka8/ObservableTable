using ObservableTable.Core;

namespace UnitTest.Core;

internal static class Helper
{
    internal static ObservableTable<string> GetSampleTable()
    {
        return new ObservableTable<string>(
            headers: new string[] { "A0", "B0", "C0" },
            records: new List<string[]>()
            {
                new string[] { "A1", "B1", "C1" },
                new string[] { "A2", "B2", "C2" }
            }
        );
    }
}
