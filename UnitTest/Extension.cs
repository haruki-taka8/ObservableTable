using ObservableTable.Core;

namespace UnitTest;

internal static class Extension
{
    internal static bool ContentEquals(this ObservableTable<string> a, ObservableTable<string> b)
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

    internal static bool ContentEquals<T>(this CellEdit<T> a, CellEdit<T> b)
    {
        foreach (var property in a.GetType().GetProperties())
        {
            if (property.GetValue(a)?.GetHashCode() != property.GetValue(b)?.GetHashCode())
            { return false; }
        }
        return true;
    }
}
