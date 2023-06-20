using System.Text.RegularExpressions;

namespace ObservableTable.Core;

public static class Extension
{
    private static IEnumerable<Cell<string>> ReplacedCells(string from, string to, bool matchRegex, IEnumerable<Cell<string>> cells)
    {
        foreach (var cell in cells)
        {
            if (cell.Value is null) { continue; }

            if (!matchRegex && cell.Value.Contains(from))
            {
                Cell<string> newCell = new(cell.Row, cell.Column, cell.Value.Replace(from, to));
                yield return newCell;
            }

            if (matchRegex && Regex.IsMatch(cell.Value, from))
            {
                Cell<string> newCell = new(cell.Row, cell.Column, Regex.Replace(cell.Value, from, to));
                yield return newCell;
            }
        }
    }

    public static void ReplaceCellSubstring(this ObservableTable<string> table, string from, string to, bool matchRegex = false, IEnumerable<Cell<string>>? cells = null)
    {
        cells ??= table.ListCells();

        var toChange = ReplacedCells(from, to, matchRegex, cells).ToList();
        table.SetCell(toChange);
    }

    public static void ReplaceCellSubstring(this ObservableTable<string> table, string from, string to, bool matchRegex = false, params Cell<string>[] cells)
    {
        table.ReplaceCellSubstring(from, to, matchRegex, cells.ToList());
    }
}