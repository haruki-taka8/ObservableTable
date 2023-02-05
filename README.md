# ObservableTable

ObservableTable is a 2D data structure designed to bind to a WPF DataGrid, allowing DataGrids to add/remove both rows and columns by changing the underlying ObservableTable.

## Features
* Allows multi-column collections
* Implements INotifyCollectionChanged and INotiftyPropertyChanged for both rows and columns
* Includes built-in undo and redo capabilities
* Provides a helper class to read from/write to CSV files

## Installation
Class library (`.dll`) available at [releases](https://github.com/haruki-taka8/ObservableTable/releases).

In Visual Studio:
* Right-click your project
* Press Add > Project Reference
* Press Browse
* Import the `.dll` file

## Usage
To initialize an ObservableTable:
```c#
ObservableTable<string> table = new(headers, records);
```

To import data from a CSV file:
```c#
ObservableTable<string> table = Importer.FromFilePath("path.csv");
```

## Requirements
* [CsvHelper](https://github.com/JoshClose/CsvHelper)

## Contributing
Contributions are welcomed. Please open pull requests for changes.
