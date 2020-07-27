using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class PortfolioModel
    {
        public string Name { get; set; }

        /// <summary>
        /// Código Carteira
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// CNPJ
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Banco Clearing
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// Agência Clearing
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Conta Clearing
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// Conta Corrente
        /// </summary>
        public string ClearingAccount { get; set; }
    }
}
