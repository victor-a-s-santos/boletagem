using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public IList<GroupRole> GroupRoles { get; set; }
    }
}
