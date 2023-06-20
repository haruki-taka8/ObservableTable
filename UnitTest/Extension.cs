using ObservableTable.Core;

namespace UnitTest;

internal static class Extension
{
    internal static bool ContentEquals<T>(this ObservableTable<T> a, ObservableTable<T> b)
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
}
