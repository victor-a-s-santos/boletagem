namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class Counterpart
    {
        public Counterpart()
        {

        }

        public string Name { get; set; }
        public string Document { get; set; }
        public string ClearingAccount { get; set; }
        public string Command { get; set; }
    }
}