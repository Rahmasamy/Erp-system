using ClosedXML.Excel;

namespace Aspire.Migration.Seed;

public class ExcelReader
{
    private readonly string _filePath;

    public ExcelReader()
    {
        _filePath = Path.Combine(
            AppContext.BaseDirectory,
            "SeedData",
            "DMS-Migration.xlsx");
    }

    public List<ExcelRow> ReadRows(string sheetName)
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException(
                $"Excel file was not found: {_filePath}");
        }

        using var workbook = new XLWorkbook(_filePath);

        if (!workbook.Worksheets.Contains(sheetName))
        {
            throw new InvalidOperationException(
                $"Sheet '{sheetName}' was not found.");
        }

        var worksheet = workbook.Worksheet(sheetName);

        var usedRows = worksheet.RowsUsed().ToList();

        if (usedRows.Count == 0)
        {
            return new List<ExcelRow>();
        }

        var headerRow = usedRows.First();

        var headers = headerRow
            .CellsUsed()
            .Select(cell => cell.GetString().Trim())
            .ToList();

        var result = new List<ExcelRow>();

        foreach (var row in usedRows.Skip(1))
        {
            var excelRow = new ExcelRow();

            for (int i = 0; i < headers.Count; i++)
            {
                var value = row.Cell(i + 1).GetString();

                excelRow.Values[headers[i]] = value;
            }

            result.Add(excelRow);
        }

        return result;
    }

    public void ReadSheetNames()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException(
                $"Excel file was not found: {_filePath}");
        }

        using var workbook = new XLWorkbook(_filePath);

        Console.WriteLine("Available sheets:");

        foreach (var worksheet in workbook.Worksheets)
        {
            Console.WriteLine($"- {worksheet.Name}");
        }
    }
}