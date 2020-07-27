using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset? LastPasswordChangedDate { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTimeOffset? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public short AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string UserDocument { get; set; }
        public DateTimeOffset PasswordExpirationDate { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
        public string Salt { get; set; }
        public ICollection<UserPassword> PasswordHistory { get; set; }
        public string PasswordResetHash { get; set; }
        public string LastAccessToken { get; set; }
        public DateTimeOffset? LastAccessDate { get; set; }

        public void IncriseAccessFailedCount(int limit)
        {
            AccessFailedCount += 1;

            if (AccessFailedCount > limit)
            {
                CreateLockout();
            }
        }

        public void ResetAccessFailedCount()
        {
            AccessFailedCount = 0;
        }

        public void CreateLockout()
        {
            LockoutEnabled = true;
        }

        public void RegisterAccess(string token) {
            LastAccessToken = token;
            LastAccessDate = DateTimeOffset.Now;
            ResetAccessFailedCount();
        }
    }
}
