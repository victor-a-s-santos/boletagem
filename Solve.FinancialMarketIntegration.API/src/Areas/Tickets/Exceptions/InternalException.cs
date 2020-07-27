using System;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions {
    public class InternalException : ServiceException
    {
        public override string HttpEquivalentCode => "500";

        public InternalException(string message): base(message) {
            
        }
    }

}
