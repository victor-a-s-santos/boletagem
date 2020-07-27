
namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions {
    public class BadRequestException : ServiceException
    {
        public override string HttpEquivalentCode => "400";

        public BadRequestException(string message): base(message) {
            
        }
    }

}
