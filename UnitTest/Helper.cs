using ObservableTable.Core;

namespace UnitTest;

internal static class Helper
{
    internal static bool ContentEquals<T>(this ObservableTable<T> a, ObservableTable<T> b) where T : notnull
    {
        // Headers
        if (!Enumerable.SequenceEqual(a.Headers, b.Headers))
        { return false; }

        // Records
        if (a.Records.Count != b.Records.Count)
        { return false; }

        for (int i = 0; i < a.Records.Count; i++)
        {
            if (!Enumerable.SequenceEqual(a.Records[i], b.Records[i]))
            { return false; }
        }
        return true;
    }

    internal static ObservableTable<string> GetSampleTable()
    {
        return new ObservableTable<string>(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" }
        );
    }

    internal static ObservableTable<string> GetLongSampleTable()
    {
        return new ObservableTable<string>(
            new string[] { "A0", "B0", "C0" },
            new string?[] { "A1", "B1", "C1" },
            new string?[] { "A2", "B2", "C2" },
            new string?[] { "A3", "B3", "C3" },
            new string?[] { "A4", "B4", "C4" },
            new string?[] { "A5", "B5", "C5" }
        );
    }

    internal static ObservableTable<int> GetSampleIntTable()
    {
        return new ObservableTable<int>(
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 }
        );
    }
}
