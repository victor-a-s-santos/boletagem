using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService UserService { get; set; }

        public UserController(IUserService userService)
        {
            this.UserService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSchema>> GetByIdAsync([FromRoute]int id)
        {
            try
            {
                var result = await this.UserService.GetByIdAsync(id);
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

        [HttpGet]
        public async Task<ActionResult<PagedResult<UserSchema>>> ListAsync()
        {
            try
            {
                var result = await this.UserService.ListAsync();
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

        [HttpGet("account-managers/{accountManagerId?}")]
        public async Task<ActionResult<PagedResult<UserSchema>>> ListByAccountManagerAsync(int? accountManagerId = null, bool isExternalUser = true, int page = 0, int pageSize = 0)
        {
            try
            {
                var result = await this.UserService.ListAsync(accountManagerId, isExternalUser, page, pageSize);
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

        [HttpGet("export/{accountManagerId?}")]
        public async Task<ActionResult<IEnumerable<UserSchema>>> ExportAsync(int? accountManagerId = null, bool isExternalUser = true)
        {
            try
            {
                var result = await this.UserService.ExportAsync(accountManagerId, isExternalUser);
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

        [HttpPost("register")]
        public async Task<ActionResult<UserSchema>> RegisterNewAsync([FromBody]UserSchema body)
        {
            try
            {
                var result = await this.UserService.RegisterNewAsync(body, User.Identity.Name);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "not-found")
                {
                    return NotFound();
                } else if (ex.Message == "already-exists")
                {
                    return Conflict();
                }

                throw;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<UserSchema>> EditAsync([FromRoute]int id, [FromBody]UserSchema body)
        {
            try
            {
                var result = await this.UserService.EditAsync(id, body, User.Identity.Name);
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
