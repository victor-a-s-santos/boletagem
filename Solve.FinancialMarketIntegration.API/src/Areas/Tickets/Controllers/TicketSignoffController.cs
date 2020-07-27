using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/v1/tickets/signoff")]
    [ApiController]
    [TicketActionExceptionFilter]
    public class TicketSignoffController : ControllerBase
    {
        private TicketDataContext Context { get; set; }
        private IAccountManagerService AccountManagerService { get; set; }
        private IMapper Mapper { get; set; }

        public TicketSignoffController(TicketDataContext context, IMapper mapper, IAccountManagerService accountManagerService)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.AccountManagerService = accountManagerService;
        }

        /// <summary>
        /// Get a single default signoff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketSignoffModel>> Get([FromRoute]int id)
        {
            var result = await GetDefaultByIdAsync(id);
            return Ok(result);
        }

        [NonAction]
        private async Task<TicketSignoffModel> GetDefaultByIdAsync(int ticketId)
        {
            var query = Context.TicketSignoff
                .Where(t => t.Id == ticketId);

            var data = await query.SingleOrDefaultAsync();
            if (data == null)
            {
                throw new Exception("not-found");
            }

            var result = Mapper.Map<TicketSignoffModel>(data);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Get list of all default signoff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketSignoffModel>>> ListAsync()
        {
            try
            {
                var query = Context.TicketSignoff.AsNoTracking();

                var data = await query.ToListAsync();

                if (data.Count == 0)
                {
                    return await Task.FromResult(new TicketSignoffModel[] { });
                }

                var result = data
                    .Select(ticket =>
                    {
                        var mapped = Mapper.Map<TicketSignoffModel>(ticket);

                        return mapped;
                    });

                return await Task.FromResult(result.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check if is allowed to foward a ticket based on signoff limit time
        /// </summary>
        /// <param name="id">TicketSignoffId</param>
        /// <returns>True if have permission</returns>
        [HttpGet("{id}/permission")]
        public async Task<ActionResult> GetPermission([FromRoute]int id)
        {
            bool isAllowed = false;

            DateTimeOffset timeLimit;

            var ticketSignoffRequest = await GetRequestByIdAsync(id);

            if (ticketSignoffRequest != null)
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;

                DateTimeOffset currentTimeLimit = new DateTime(
                    now.Year, now.Month, now.Day,
                    ticketSignoffRequest.TimeLimit.Hours,
                    ticketSignoffRequest.TimeLimit.Minutes,
                    ticketSignoffRequest.TimeLimit.Seconds,
                    DateTimeKind.Utc
                );

                isAllowed = currentTimeLimit.DateTime > now.DateTime;
                timeLimit = currentTimeLimit.DateTime;
            }
            else
            {
                var ticketSignoffDefault = await GetDefaultByIdAsync(id);

                DateTimeOffset now = DateTimeOffset.UtcNow;

                DateTimeOffset currentTimeLimit = new DateTime(
                    now.Year, now.Month, now.Day,
                    ticketSignoffDefault.TimeLimit.Hours,
                    ticketSignoffDefault.TimeLimit.Minutes,
                    ticketSignoffDefault.TimeLimit.Seconds,
                    DateTimeKind.Utc
                );

                isAllowed = currentTimeLimit > now;
                timeLimit = currentTimeLimit;
            }

            var result = new
            {
                isAllowed = isAllowed,
                timeLimit = timeLimit.TimeOfDay
            };

            return Ok(result);
        }

        [NonAction]
        private async Task<TicketSignoffRequestModel> GetRequestByIdAsync(int TicketSignoffId)
        {
            var query = Context.TicketSignoffRequest
                    .Include(t => t.TicketSignoff)
                    .Where(t => t.TicketSignoff.Id == TicketSignoffId &&
                                t.CreationDate.Date == DateTimeOffset.UtcNow.Date)
                    .Take(1)
                    .OrderByDescending(t => t.Id);

            var data = await query.SingleOrDefaultAsync();
            if (data == null)
            {
                return null; //Task.FromResult<object>(null)
            }

            var result = Mapper.Map<TicketSignoffRequestModel>(data);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Update a default ticket signoff
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<TicketSignoffModel> Put([FromRoute]int id, [FromBody] TicketSignoffModel model)
        {
            var ticket = await Context.TicketSignoff
                .SingleOrDefaultAsync(t => t.Id == model.Id);

            if (ticket == null)
            {
                throw new Exception("not-found");
            }

            Mapper.Map(model, ticket);

            ticket.ChangeDate = DateTimeOffset.UtcNow;
            ticket.ChangeUser = User.Identity.Name;

            await Context.SaveChangesAsync();

            var log = new TicketSignoffLog();
            log.TicketSignoff = ticket;
            log.CreationDate = DateTimeOffset.UtcNow;
            log.CreationUser = User.Identity.Name;
            log.TimeLimit = ticket.TimeLimit;

            Context.TicketSignoffLog.Add(log);

            await Context.SaveChangesAsync();

            var result = Mapper.Map<TicketSignoffModel>(ticket);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Get default history list
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/history-default")]
        public async Task<ActionResult<IEnumerable<TicketSignoffLogModel>>> ListDefaultHistory([FromRoute] int id, [FromQuery] int limit = 10)
        {
            limit = limit < 0 ? 10 : limit;

            var query = Context.TicketSignoffLog
                .Include(t => t.TicketSignoff)
                .Where(t => t.TicketSignoff.Id == id)
                .OrderByDescending(t => t.CreationDate)
                .Take(limit)
                .AsNoTracking();

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new TicketSignoffLogModel[] { });
            }

            var result = data
                .Select(ticket =>
                {
                    var mapped = Mapper.Map<TicketSignoffLogModel>(ticket);

                    return mapped;
                });

            return await Task.FromResult(result.ToList());
        }

        /// <summary>
        /// Insert a new request ticket signoff
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<TicketSignoffRequestModel> Post([FromRoute]int id, [FromBody] TicketSignoffRequestModel model)
        {
            var newRequest = Mapper.Map<TicketSignoffRequest>(model);

            Context.Attach(newRequest.TicketSignoff);
            Context.TicketSignoffRequest.Add(newRequest);
            await Context.SaveChangesAsync();

            var result = Mapper.Map<TicketSignoffRequestModel>(newRequest);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Get request history list
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/history-request")]
        public async Task<ActionResult<IEnumerable<TicketSignoffHistoryModel>>> ListRequestHistory([FromRoute] int id, [FromQuery] int limit = 10)
        {
            limit = limit < 0 ? 10 : limit;

            var query = Context.TicketSignoffRequest
                .Include(t => t.TicketSignoff)
                .Where(t => t.TicketSignoff.Id == id)
                .OrderByDescending(t => t.CreationDate)
                .Take(limit)
                .AsNoTracking();

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new TicketSignoffHistoryModel[] { });
            }

            var result = data
                .Select(ticket =>
                {
                    var mapped = Mapper.Map<TicketSignoffHistoryModel>(ticket);

                    return mapped;
                });

            return await Task.FromResult(result.ToList());
        }

        [HttpGet("resume")]
        public async Task<ActionResult<IEnumerable<TicketSignoffResumeModel>>> ListResume()
        {
            var query = from ts in Context.TicketSignoff
                        let tsr = Context.TicketSignoffRequest
                            .OrderByDescending(r => r.CreationDate)
                            .Where(tr => ts.Id == tr.TicketSignoff.Id &&
                                   tr.CreationDate.Date == DateTimeOffset.UtcNow.Date
                            ).FirstOrDefault()
                        select new TicketSignoffResumeModel
                        {
                            Id = ts.Id,
                            TicketTypeId = ts.TicketTypeId,
                            Type = ts.Type,
                            DefaultLimitTime = ts.TimeLimit,
                            CurrentLimitTime = (TimeSpan?)tsr.TimeLimit
                        };

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new TicketSignoffResumeModel[] { });
            }

            return await Task.FromResult(data.ToList());
        }
    }

}
