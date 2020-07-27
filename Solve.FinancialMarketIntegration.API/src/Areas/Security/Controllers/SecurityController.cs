using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public ISecurityService SecurityService { get; set; }
        public IAccountManagerService AccountManagerService { get; set; }

        public SecurityController(ISecurityService securityService, IAccountManagerService accountManagerService)
        {
            this.SecurityService = securityService;
            this.AccountManagerService = accountManagerService;
        }

        [HttpGet("groups/{id}")]
        public async Task<ActionResult<Group>> GetGroupByIdAsync([FromRoute]int id)
        {
            try
            {
                var result = await this.SecurityService.GetGroupByIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<GroupSchema>>> GetGroupsAsync()
        {
            try
            {
                var result = await this.SecurityService.ListGroupAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpGet("groups/from-manager/{username}")]
        public async Task<ActionResult<IEnumerable<GroupSchema>>> GetMasterGroupsByUsernameAsync(string username)
        {
            try
            {
                var result = await this.SecurityService.GetMasterGroupsByUsernameAsync(username);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpPut("groups/update/{id}")]
        public async Task<ActionResult<GroupSchema>> UpdateGroupAsync([FromRoute]int id, [FromBody]GroupSchema body)
        {
            try
            {
                var result = await this.SecurityService.EditGroupAsync(id, body, User.Identity.Name);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesAsync()
        {
            try
            {
                var result = await this.SecurityService.GetRolesAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpGet("account-managers")]
        public ActionResult<IEnumerable<AccountManager>> GetAccountManagers()
        {
            try
            {
                var result = this.AccountManagerService.GetAccountManagers();

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpGet("user-account-manager/{username}")]
        public ActionResult<AccountManagerSchema> GetAccountManagerFromUsernameAsync(string username)
        {
            try
            {
                var result = this.AccountManagerService.GetAccountManagerSchema(username);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                }

                throw;
            }
        }
    }
}