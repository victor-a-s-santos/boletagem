
namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions {
    public class NotFoundException : ServiceException
    {
        public override string HttpEquivalentCode => "404";

        public NotFoundException(string message): base(message) {
            
        }
    }

}
