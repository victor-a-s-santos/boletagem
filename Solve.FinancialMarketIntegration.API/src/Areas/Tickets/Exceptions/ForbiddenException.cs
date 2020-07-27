
namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions {
    public class ForbiddenException : ServiceException
    {
        public override string HttpEquivalentCode => "403";

        public ForbiddenException(string message): base(message) {
            
        }
    }

}
