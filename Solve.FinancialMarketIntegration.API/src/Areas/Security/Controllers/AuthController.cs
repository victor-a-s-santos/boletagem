using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using System.Collections.Generic;
using System.Linq;
using src.Areas.Security.Models;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/security/[controller]")]
    public class AuthController : ControllerBase
    {
        private DataContext Context { get; set; }
        private IAuthService AuthService { get; set; }

        public AuthController(DataContext context, IAuthService authService)
        {
            this.Context = context;
            this.AuthService = authService;
        }

        //Token
        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Token([FromBody]UserAuthentication userParam)
        {
            try
            {
                var user = this.AuthService.Authenticate(userParam.Username, userParam.Password);

                return Ok(user);
            }
            catch(System.Exception ex)
            {
                if (ex.Message == "password-expired")
                {
                    return new UnauthorizedObjectResult(new { Message = ex.Message });
                }
                else if (ex.Message == "wrong-password")
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                } 
                else if(ex.Message == "user-not-valid")
                {
                    return Forbid();
                }

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody]UserPasswordRecovery userParam)
        {
            try
            {
                var user = this.AuthService.PasswordReset(userParam.Username, userParam.NewPassword, userParam.OldPassword, userParam.PasswordResetHash);

                return Ok(user);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "password-history")
                {
                    return new UnauthorizedObjectResult(new { Message = ex.Message });
                }
                else if (ex.Message == "wrong-password")
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                else if (ex.Message == "invalid-password")
                {
                    return BadRequest(new { message = "Password does not comply with minimal security rules" });
                }
                else if (ex.Message == "user-not-valid")
                {
                    return Forbid();
                }

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("password-forgot")]
        public async Task<IActionResult> PasswordForgot([FromBody]ForgotPasswordSchema schema)
        {
            try
            {
                await this.AuthService.PasswordForgot(schema.Username);

                return Ok(new { message = "sent" });
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "user-not-valid")
                {
                    return Forbid();
                }

                throw;
            }
        }

        [HttpGet]
        [Route("user/roles")]
        public ActionResult<IEnumerable<int>> GetRoles()
        {
            var roles = this.AuthService.GetRoles(User).Select(x => x.Id);

            return Ok(roles);
        }
    }
}
