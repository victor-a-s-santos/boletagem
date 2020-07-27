using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserSchema>();

            CreateMap<UserSchema, User>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}