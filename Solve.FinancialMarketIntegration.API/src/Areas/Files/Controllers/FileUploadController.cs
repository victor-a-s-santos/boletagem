using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Files.Models;
using Solve.FinancialMarketIntegration.API.Areas.Files.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Controllers
{
    [Route("api/v1/files/file-upload")]
    [ApiController]
    [Authorize]
    public class FileUploadController : Controller
    {
        private FileDataContext Context { get; set; }
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IFileService FileService;
        private readonly IAccountManagerService AccountManagerService;

        public FileUploadController(FileDataContext context, IHostingEnvironment hostingEnvironment, IFileService fileService, IAccountManagerService accountManagerService)
        {
            this.Context = context;
            this.HostingEnvironment = hostingEnvironment;
            this.FileService = fileService;
            this.AccountManagerService = accountManagerService;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("variable-income")]
        public async Task<IActionResult> Index(UploadRvModel model)
        {
            try
            {
                int offset = 0;

                var tz = this.HttpContext.Request.Headers["X-Timezone-Offset"];

                if (tz.Count > 0) {
                    offset = Convert.ToInt32(tz.First().Split(',')[0]);
                }

                bool ok = await this.FileService.ProcessRVAsync(model.FileBase64, offset, User.Identity.Name);
                return new OkObjectResult(ok);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("account-managers")]
        public IActionResult AccountManagersInfo()
        {
            return new OkObjectResult(this.AccountManagerService.GetAccountManagers()
                                                                .Select(am => new { Name = am.Name, Id = am.Id, Document = am.Document })
                                                                .OrderBy(am => am.Name)
                                                                .ToList());
        }
    }
}
