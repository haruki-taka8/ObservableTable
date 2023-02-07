using ObservableTable.Core;

namespace UnitTest;

internal static class Extension
{
    internal static bool ContentEquals(this ObservableTable<string> operand1, ObservableTable<string> operand2)
    {
        // Headers
        if (!Enumerable.SequenceEqual(operand1.Headers, operand2.Headers))
        { return false; }

        // Records
        if (operand1.Records.Count != operand2.Records.Count)
        { return false; }

        for (int i = 0; i < operand1.Records.Count; i++)
        {
            if (!Enumerable.SequenceEqual(operand1.Records[i], operand2.Records[i]))
            { return false; }
        }

        return true;
    }

    internal static bool ContentEquals<T>(this Operation<T> operand1, Operation<T> operand2)
    {
        // Headers
        foreach (var property in operand1.GetType().GetProperties())
        {
            if (property.GetValue(operand1) != property.GetValue(operand2))
            { return false; }
        }

        return true;
    }
}
