using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities.Enums
{
    /// <summary>
    /// Mapeamento de moedas.
    /// </summary>
    public enum Currency : short
    {
        /// <summary>
        /// Brazil
        /// </summary>
        Real = 1,

        /// <summary>
        /// USD - United States
        /// </summary>
        USD = 2,

        /// <summary>
        /// Europe
        /// </summary>
        Euro = 3,

        /// <summary>
        /// Japan
        /// </summary>
        Yene = 4
    }
}
