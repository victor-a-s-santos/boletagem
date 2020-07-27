using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Entities
{

    public class FileType
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }
}
