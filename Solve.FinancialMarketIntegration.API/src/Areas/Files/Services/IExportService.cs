using System.Collections.Generic;
using OfficeOpenXml;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Services
{
    public interface IExportService
    {
         ExcelWorksheet GenerateWorksheet<T>(
            ExcelPackage package,
            string title,
            IList<ColumnMap<T>> columnMap,
            IEnumerable<T> dataSource
        );
    }
}