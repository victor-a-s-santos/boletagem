using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class Ticket : Entity
    {
        public Ticket ()
        {
            History = new List<TicketHistory>();
        }

        public TicketType Type { get; set; }
        public Portfolio Portfolio { get; set; }
        public AccountManager AccountManager { get; set; }
        public string AccountManagerName { get; set; }
        public string AccountManagerDocument { get; set; } 
        public DateTimeOffset OperationDate { get; set; }
        public string Annotations { get; set; }
        public int? WorkflowId { get; set; }
        public ICollection<TicketHistory> History { get; set; }

        public static Ticket New(TicketType ticketType, Portfolio portfolio, string annotations, AccountManager accountManager)
        {
            return new Ticket
            {
                Type = ticketType,
                Portfolio = portfolio,
                OperationDate = DateTimeOffset.Now,
                Annotations = annotations,
                AccountManager = accountManager
            };
        }
    }
}
