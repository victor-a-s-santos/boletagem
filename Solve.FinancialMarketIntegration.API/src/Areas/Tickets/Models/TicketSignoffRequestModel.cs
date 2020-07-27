using System;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
  public class TicketSignoffRequestModel
  {
    public int TicketSignoffId { get; set; }

    public TimeSpan TimeLimit { get; set; }

    public string Justificative { get; set; }
  }
}