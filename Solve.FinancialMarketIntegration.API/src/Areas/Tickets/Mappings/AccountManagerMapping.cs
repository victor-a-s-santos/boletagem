using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class AccountManagerMapping : Profile
    {
        public AccountManagerMapping()
        {
            CreateMap<AccountManager, AccountManagerSchema>();
            CreateMap<AccountManagerSchema, AccountManager>();
        }
    }
}