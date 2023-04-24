using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTest")]
namespace ObservableTable.Core;

public class ObservableTable<T>
{
    // Properties & Fields
    public ObservableCollection<ObservableCollection<T?>> Records { get; } = new();
    public ReadOnlyObservableCollection<T> Headers => new(headers);

    private ObservableCollection<T> headers { get; } = new();

    // Constructors
    public ObservableTable() { }

    public ObservableTable(IEnumerable<T> headers, params IList<T?>[] records)
    {
        this.headers = new(headers);
        InsertRow(0, records);
    }

    // Methods: Record modifications
    public void InsertRow(int index, params IList<T?>[] rows)
    {
        foreach (var row in rows)
        {
            InsertRow(index++, row);
        }
    }

    private void InsertRow(int index, IList<T?> row)
    {
        IList<T?> baseToAdd = row.PadRight(headers.Count);
        ObservableCollection<T?> toAdd = new(baseToAdd);
        Records.Insert(index, toAdd);
    }

    public void RemoveRow(params ObservableCollection<T?>[] rows)
    {
        foreach (var row in rows)
        {
            RemoveRow(row);
        }
    }

    private void RemoveRow(ObservableCollection<T?> row)
    {
        Records.Remove(row);
    }

    public void InsertColumn(int index, params ColumnDefinition<T>[] columns)
    {
        foreach (var column in columns)
        {
            InsertColumn(index++, column);
        }
    }

    private void InsertColumn(int index, ColumnDefinition<T> column)
    {
        var values = column.Values.PadRight(Records.Count);

        for (int i = 0; i < Records.Count; i++)
        {
            Records[i].Insert(index, values[i]);
        }

        headers.Insert(index, column.Header);
    }

    public void RemoveColumn(params T[] headers)
    {
        foreach (var header in headers)
        {
            RemoveColumn(header);
        }
    }

    private void RemoveColumn(T header)
    {
        // Remove header first to prevent binding failures
        int index = headers.IndexOf(header);
        headers.Remove(header);

        List<T?> removedColumn = new();
        foreach (var record in Records)
        {
            removedColumn.Add(record[index]);
            record.RemoveAt(index);
        }
    }

    public void SetCell(params CellDefinition<T>[] cells)
    {
        foreach (var cell in cells)
        {
            SetCell(cell);
        }
    }

    private void SetCell(CellDefinition<T> cell)
    {
        Records[cell.Row][cell.Column] = cell.Value;
    }
}
