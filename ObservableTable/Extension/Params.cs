﻿namespace ObservableTable.Core;

public static class Params
{
    /// <summary>
    /// These extension methods accept params instead of IEnumerable<>,
    /// providing flexibility for consumers.
    /// They are wrappers to, and functionally identical to, the built-in methods.
    /// </summary>
    
    public static void InsertRow<T>(this ObservableTable<T> table, int index, params IList<T?>[] rows) where T : notnull
    {
        table.InsertRow(index, rows.AsEnumerable());
    }

    public static void RemoveRow<T>(this ObservableTable<T> table, params IList<T?>[] rows) where T : notnull
    {
        table.RemoveRow(rows.AsEnumerable());
    }

    public static void InsertColumn<T>(this ObservableTable<T> table, int index, params Column<T>[] columns) where T : notnull
    {
        table.InsertColumn(index, columns.AsEnumerable());
    }

    public static void RemoveColumn<T>(this ObservableTable<T> table, params T[] headers) where T : notnull
    {
        table.RemoveColumn(headers.AsEnumerable());
    }

    public static void SetCell<T>(this ObservableTable<T> table, params Cell<T>[] cells) where T : notnull
    {
        table.SetCell(cells.AsEnumerable());
    }

    public static IEnumerable<Cell<T>> FindCell<T>(this ObservableTable<T> table, T value, params Cell<T>[] cells) where T : notnull
    {
        return table.FindCell(value, cells.AsEnumerable());
    }

    public static void ReplaceCell<T>(this ObservableTable<T> table, T from, T to, params Cell<T>[] cells) where T : notnull
    {
        table.ReplaceCell(from, to, cells.AsEnumerable());
    }
}
