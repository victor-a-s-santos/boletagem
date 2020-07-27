using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities
{
    public class GroupRole
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
