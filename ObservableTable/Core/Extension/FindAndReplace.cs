using System.Text.RegularExpressions;

namespace ObservableTable.Core;

public static class FindAndReplace
{
    /// <summary>
    /// Extension methods for ObservableTable{string}
    /// These methods work similarly to the generic version, 
    /// but these accept partial matches and regular expression matches.
    /// </summary>

    public static IEnumerable<Cell<string>> FindCellWithSubstring(this ObservableTable<string> table, string value, bool matchRegex = false, IEnumerable<Cell<string>>? cells = null)
    {
        bool MatchRegex(Cell<string> cell)
        {
            return Regex.IsMatch(cell.Value!, value);
        }

        bool MatchLiteral(Cell<string> cell)
        {
            return cell.Value!.Contains(value);
        }

        cells ??= table.ToCells();

        return cells
            .Where(x => x.Value is not null)
            .Where(matchRegex ? MatchRegex : MatchLiteral);
    }

    private static IEnumerable<Cell<string>> Replace(this IEnumerable<Cell<string>> cells, string from, string to, bool matchRegex)
    {
        Cell<string> ReplaceRegex(Cell<string> x)
        {
            string value = Regex.Replace(x.Value!, from, to);
            return new(x.Row, x.Column, value);
        }

        Cell<string> ReplaceLiteral(Cell<string> x)
        {
            string value = x.Value!.Replace(from, to);
            return new(x.Row, x.Column, value);
        }

        return cells
            .Where(x => x.Value is not null)
            .Select(x => matchRegex ? ReplaceRegex(x) : ReplaceLiteral(x));
    }

    public static void ReplaceCellWithSubstring(this ObservableTable<string> table, string from, string to, bool matchRegex = false, IEnumerable<Cell<string>>? cells = null)
    {
        cells = table.FindCellWithSubstring(from, matchRegex, cells);

        var toChange = cells.Replace(from, to, matchRegex);
        table.SetCell(toChange);
    }

    public static void ReplaceCellWithSubstring(this ObservableTable<string> table, string from, string to, bool matchRegex = false, params Cell<string>[] cells)
    {
        table.ReplaceCellWithSubstring(from, to, matchRegex, cells.AsEnumerable());
    }
}