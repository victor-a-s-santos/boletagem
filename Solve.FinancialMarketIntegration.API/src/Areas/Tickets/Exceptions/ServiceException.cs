using System;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions {
    public abstract class ServiceException: Exception
    {
        public abstract string HttpEquivalentCode { get; }

        public ServiceException(string message): base(message) {
            
        }

    }

}