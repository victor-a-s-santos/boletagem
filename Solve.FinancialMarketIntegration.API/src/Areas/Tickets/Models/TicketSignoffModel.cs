using System;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
  public class TicketSignoffModel
  {
    public short Id { get; set; }

    public TicketTypes TicketTypeId { get; set; }

    public TimeSpan TimeLimit { get; set; }

    public string Type { get; set; }
  }
}