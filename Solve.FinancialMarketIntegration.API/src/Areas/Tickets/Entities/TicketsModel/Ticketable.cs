using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class Ticketable : ITicketable, IAuditable
    {
        public Ticket Ticket { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTimeOffset? ChangeDate { get; set; }
        public string ChangeUser { get; set; }
        
    }
}
