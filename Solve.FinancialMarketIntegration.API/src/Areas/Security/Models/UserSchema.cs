using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Models
{
    [DataContract]
    public class UserSchema
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        [DataMember(Name = "email", IsRequired = false)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets UserName
        /// </summary>
        [DataMember(Name = "userName", IsRequired = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets UserDocument
        /// </summary>
        [DataMember(Name = "userDocument", IsRequired = false)]
        public string UserDocument { get; set; }

        /// <summary>
        /// Gets or Sets Groups
        /// </summary>
        [DataMember(Name = "groups", IsRequired = false)]
        public IEnumerable<int> Groups { get; set; }

        /// <summary>
        /// Gets or Sets Active
        /// </summary>
        [DataMember(Name = "active", IsRequired = false)]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or Sets IsMaster
        /// </summary>
        [DataMember(Name = "isMaster", IsRequired = false)]
        public bool IsMaster { get; set; }

        /// <summary>
        /// Gets or Sets AccountManagerId
        /// </summary>
        [DataMember(Name = "accountManagerId", IsRequired = false)]
        public int? AccountManagerId { get; set; }

        /// <summary>
        /// Gets or Sets IsExternalUser
        /// </summary>
        [DataMember(Name = "isExternalUser", IsRequired = false)]
        public bool IsExternalUser { get; set; }

        /// <summary>
        /// Gets or Sets LockoutEndDateUtc
        /// </summary>
        [DataMember(Name = "lockoutEndDateUtc", IsRequired = false)]
        public DateTimeOffset? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or Sets LockoutEnabled
        /// </summary>
        [DataMember(Name = "lockoutEnabled", IsRequired = false)]
        public bool LockoutEnabled { get; set; }
    }
}
