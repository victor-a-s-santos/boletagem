using System.Runtime.Serialization;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class BankModel
    {
       [DataMember(Name = "id", IsRequired = false)]
        public int Id { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

         [DataMember(Name = "name")]
        public string Name { get; set; }

         [DataMember(Name = "shortName")]
        public string ShortName { get; set; }
    }
}