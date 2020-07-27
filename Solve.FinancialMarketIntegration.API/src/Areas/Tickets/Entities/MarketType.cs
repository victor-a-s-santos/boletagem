using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class MarketType
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }

    public enum MarketTypes
    {
        RFTituloPrivado = 1,
        RFTituloPublico = 2,
        RendaVariavel = 3,
        Dinheiro = 4
    }
}
