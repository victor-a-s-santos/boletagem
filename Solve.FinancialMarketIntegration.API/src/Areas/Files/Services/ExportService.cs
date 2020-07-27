using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Services
{
    public class ExportService : IExportService
    {
        public ExcelWorksheet GenerateWorksheet<T>(
            ExcelPackage package,
            string title,
            IList<ColumnMap<T>> columnMap,
            IEnumerable<T> dataSource
        )
        {
            var workSheet = package.Workbook.Worksheets.Add(title);
            workSheet.View.ShowGridLines = false;

            var fields = columnMap.Count;

            for (int c = 0; c < columnMap.Count; c++)
            {
                workSheet.Cells[1, c + 1].Value = columnMap[c].ColumnName;
            }

            workSheet.Cells[1, 1, 1, columnMap.Count - 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells[1, 1, 1, columnMap.Count - 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#00067A"));
            workSheet.Cells[1, columnMap.Count, 1, columnMap.Count].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells[1, columnMap.Count, 1, columnMap.Count].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#00B050"));
            workSheet.Cells[1, 1, 1, columnMap.Count].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

            var data = dataSource.ToList();

            for (var rowNumber = 2; rowNumber < data.Count + 2; rowNumber++)
            {
                if (rowNumber % 2 == 1)
                {
                    workSheet.Cells[rowNumber, 1, rowNumber, columnMap.Count].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    workSheet.Cells[rowNumber, 1, rowNumber, columnMap.Count].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#F0F0F0"));
                }

                var rowData = data[rowNumber - 2];

                for (int i = 0; i < columnMap.Count; i++)
                {
                    workSheet.Cells[rowNumber, i + 1].Value = columnMap[i].Mapper(rowData);
                }
            }

            workSheet.Cells[1, 1, 1, columnMap.Count].Style.Font.Bold = true;
            workSheet.Cells[1, 1, data.Count + 1, columnMap.Count].AutoFitColumns();

            return workSheet;
        }
    }

    public class ColumnMap<T>
    {
        public ColumnMap(
            string columnName,
            Func<T, object> mapper
        )
        {
            this.ColumnName = columnName;
            this.Mapper = mapper;
        }

        public string ColumnName { get; }

        public Func<T, object> Mapper { get; }
    }
}