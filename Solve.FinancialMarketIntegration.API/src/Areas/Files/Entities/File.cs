using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Entities
{

    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short IdFileType { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public short Status { get; set; }
    }
}
