using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Enums
{
    public static class EnumBase
    {
        public static string GetEnumName(this Enum item)
        {
            return Enum.GetName(item.GetType(), item);
        }
    }
}
