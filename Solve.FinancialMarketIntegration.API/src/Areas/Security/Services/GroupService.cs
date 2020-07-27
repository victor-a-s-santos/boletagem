using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public class GroupService : IGroupService
    {
        private DataContext Context { get; set; }

        public GroupService(DataContext context)
        {
            this.Context = context;
        }

        public Group Add(Group group, string username)
        {
            group.CreationDate = System.DateTimeOffset.Now;
            group.CreationUser = username;
            this.Context.Groups.Add(group);
            this.Context.SaveChanges();
            return group;
        }
    }

}