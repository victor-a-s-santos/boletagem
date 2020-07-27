using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class Entity : Entity<int>
    {

    }

    public class Entity<T> : IAuditable
    {
        public T Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTimeOffset? ChangeDate { get; set; }
        public string ChangeUser { get; set; }
    }

    public interface IAuditable
    {
        DateTimeOffset CreationDate { get; set; }
        string CreationUser { get; set; }
        DateTimeOffset? ChangeDate { get; set; }
        string ChangeUser { get; set; }
    }
}
