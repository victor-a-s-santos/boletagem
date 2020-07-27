using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Enums;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Files.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [ApiController]
    public class TicketExportController : ControllerBase
    {
        private TicketDataContext context;
        private IWorkflowService workflowService;
        private IAuthService authService;

        private ITicketService<TicketFundQuotaModel> ticketFundQuotaService;
        private ITicketService<TicketPrivateFixedIncomeModel> privateFixedService;
        private ITicketService<TicketPublicFixedIncomeModel> publicFixedIncomeService;
        private ITicketService<TicketFuturesModel> futuresService;
        private ITicketService<TicketMarginModel> marginService;
        private ITicketService<TicketContractedModel> contractedService;
        private ITicketService<TicketCurrencyTermModel> currencyTermService;
        private ITicketService<TicketSwapCetipModel> swapCetipService;
        private ITicketService<TicketVariableIncomeModel> variableIncomeService;

        private IExportService exportService;

        public TicketExportController(
            TicketDataContext context,
            IWorkflowService workflowService,
            IAuthService authService,
            TicketFundQuotasController fundQuotaController,
            TicketPrivateFixedIncomeController privateFixedIncomeController,
            TicketPublicFixedIncomeController publicFixedIncomeController,
            TicketFuturesController futuresController,
            TicketMarginController marginController,
            TicketContractedController contractedController,
            TicketCurrencyTermController ticketCurrencyTermController,
            TicketSwapCetipController swapCetipController,
            TicketVariableIncomeController variableIncomeController,
            IExportService exportService
        )
        {
            this.context = context;
            this.workflowService = workflowService;
            this.authService = authService;
            this.ticketFundQuotaService = fundQuotaController;
            this.privateFixedService = privateFixedIncomeController;
            this.publicFixedIncomeService = publicFixedIncomeController;
            this.futuresService = futuresController;
            this.marginService = marginController;
            this.contractedService = contractedController;
            this.currencyTermService = ticketCurrencyTermController;
            this.swapCetipService = swapCetipController;
            this.variableIncomeService = variableIncomeController;
            this.exportService = exportService;
        }

        private const string DefaultStatus = "Em Elaboração";

        [HttpGet]
        [Route("api/v1/tickets/ticket/export")]
        public async Task<ActionResult<MemoryStream>> Export(
            [FromQuery]int? ticketType,
            [FromQuery]DateTimeOffset? startDate,
            [FromQuery]DateTimeOffset? endDate,
            [FromQuery]int? statusId,
            [FromQuery]string portfolioCode,
            [FromQuery]string portfolioName,
            [FromQuery]string portfolioDocument,
            [FromQuery]int? ticketId,
            [FromQuery]int? accountManagerId
        )
        {
            var roles = authService.GetRoles(User.Identity.Name).ToDictionary(kvp => kvp.Id, kvp => kvp.Name);

            IDictionary<short, string> settlements = null;

            var getSettlements = new Func<IDictionary<short, string>>(() =>
            {
                if (settlements == null)
                {
                    settlements = context.SettlementTypes.Select(t => new { t.Id, t.Name }).ToDictionary(t => t.Id, t => t.Name);
                }

                return settlements;
            });

            ExcelPackage excelPackage = null;

            var methods = new List<Action>();

            if (roles.ContainsKey((int)Roles.ViewQuotaMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.FundQuota))
            {
                var resultQuota = await this.ticketFundQuotaService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter()
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetQuotas(excelPackage, "Cotas", resultQuota.ToArray(), getSettlements()));
            }

            if (roles.ContainsKey((int)Roles.ViewPublicFixedIncomeMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.PublicFixedIncome))
            {
                var resultPublicFixed = await this.publicFixedIncomeService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetPublicFixed(excelPackage, "Títulos Públicos", resultPublicFixed.ToArray(), getSettlements()));
            }

            if (roles.ContainsKey((int)Roles.ViewPrivateFixedIncomeMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.PrivateFixedIncome))
            {
                var resultPrivateFixed = await this.privateFixedService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetPrivateFixed(excelPackage, "Títulos Privados", resultPrivateFixed.ToArray()));
            }

            if (roles.ContainsKey((int)Roles.ViewFuturesMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.Futures))
            {
                var resultFutures = await this.futuresService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetFutures(excelPackage, "Futuros", resultFutures.ToArray()));
            }

            if (roles.ContainsKey((int)Roles.ViewMarginMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.Margin))
            {
                var resultMargin = await this.marginService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetMargin(excelPackage, "Margem", resultMargin.ToArray()));
            }

            if (roles.ContainsKey((int)Roles.ViewContractedMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.Contracted))
            {
                var resultContrated = await this.contractedService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetContracted(excelPackage, "Compromissada de Compra", resultContrated.ToArray()));
            }

            if (roles.ContainsKey((int)Roles.ViewCurrencyTermMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.CurrencyTerm))
            {
                var resultTerm = await this.currencyTermService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetTerm(excelPackage, "Termo de Moeda", resultTerm.ToArray(), getSettlements()));
            }

            if (roles.ContainsKey((int)Roles.ViewSwapCETIPMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.SwapCetip))
            {
                var resultSwapCetip = await this.swapCetipService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetSwapCetip(excelPackage, "Swap-CETIP", resultSwapCetip.ToArray()));
            }

            if (roles.ContainsKey((int)Roles.ViewVariableIncomeMonitor)
                && (ticketType == null || ticketType == (int)TicketTypes.VariableIncome))
            {
                var resultTerm = await this.variableIncomeService.ListAsync(
                    User.Identity.Name,
                    new TicketFilter
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = statusId,
                        PortfolioCode = portfolioCode,
                        PortfolioName = portfolioName,
                        PortfolioDocument = portfolioDocument,
                        TicketId = ticketId,
                        AccountManagerId = accountManagerId
                    }
                );
                methods.Add(() => GetWorkSheetVariableIncome(excelPackage, "Renda Variável", resultTerm.ToArray(), getSettlements()));
            }

            if (methods.Count == 0)
            {
                return Forbid();
            }

            using (excelPackage = new ExcelPackage())
            {
                var errors = new List<Exception>();
                foreach (var method in methods)
                {
                    try
                    {
                        method();
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }

                if (errors.Count == methods.Count) {
                    throw errors[0];
                }

                return new MemoryStream(excelPackage.GetAsByteArray());
            }
        }

        private void GetWorkSheetQuotas(
            ExcelPackage package,
            string title,
            IEnumerable<TicketFundQuotaModel> data,
            IDictionary<short, string> settlements
        )
        {
            var mapper = new ColumnMap<TicketFundQuotaModel>[] {
                new ColumnMap<TicketFundQuotaModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketFundQuotaModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketFundQuotaModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketFundQuotaModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketFundQuotaModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketFundQuotaModel>("TIPO LIQUIDAÇÃO", (t) => settlements[t.SettlementTypeId]),
                new ColumnMap<TicketFundQuotaModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketFundQuotaModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketFundQuotaModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketFundQuotaModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketFundQuotaModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketFundQuotaModel>("FUNDO INVESTIDO", (t) => t.FundName),
                new ColumnMap<TicketFundQuotaModel>("CNPJ DO FUNDO INVESTIDO", (t) => t.FundDocument),
                new ColumnMap<TicketFundQuotaModel>("CLASSE DO FUNDO INVESTIDO", (t) => t.FundClassSeries),
                new ColumnMap<TicketFundQuotaModel>("CÓDIGO MNEMONICO / CETIP DO FUNDO", (t) => t.MnemonicCode),
                new ColumnMap<TicketFundQuotaModel>("TIPO DE COTA DO CONDOMÍNIO", (t) => t.FundType),
                new ColumnMap<TicketFundQuotaModel>("PU DE EMISSÃO?", (t) => t.IsIssueUnitPrice ? "SIM" : "NÃO"),
                new ColumnMap<TicketFundQuotaModel>("VALOR DA COTA", (t) => t.QuotaValue),
                new ColumnMap<TicketFundQuotaModel>("DATA DE LIQUIDAÇÃO", (t) => t.SettlementDate.HasValue ? t.SettlementDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketFundQuotaModel>("DATA DE COTIZAÇÃO", (t) => t.QuotationDate.HasValue ? t.QuotationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketFundQuotaModel>("CONTA CETIP DO INVESTIDOR", (t) => t.PortfolioCetipAccount),
                new ColumnMap<TicketFundQuotaModel>("NOME CONTRAPARTE", (t) => t.CounterpartName),
                new ColumnMap<TicketFundQuotaModel>("CNPJ CONTRAPARTE", (t) => t.CounterpartDocument),
                new ColumnMap<TicketFundQuotaModel>("CONTA CETIP CONTRAPARTE", (t) => t.CounterpartCetipAccount),
                new ColumnMap<TicketFundQuotaModel>("QUANTIDADE", (t) => t.Amount),
                new ColumnMap<TicketFundQuotaModel>("VALOR DA OPERAÇÃO", (t) => t.OperationValue),
                new ColumnMap<TicketFundQuotaModel>("É NOVO FUNDO NO MERCADO?", (t) => t.IsNewFund ? "SIM" : "NÃO"),
                new ColumnMap<TicketFundQuotaModel>("ARQUIRIDO EM MERCADO SECUNDÁRIO", (t) => t.IsSecondaryMarket ? "SIM" : "NÃO"),
                new ColumnMap<TicketFundQuotaModel>("FIDC", (t) => t.IsFIDC ? "SIM" : "NÃO"),
                new ColumnMap<TicketFundQuotaModel>("CETIP VOICE", (t) => t.IsCetipVoice.HasValue && t.IsCetipVoice.Value ? "SIM" : "NÃO"),
                new ColumnMap<TicketFundQuotaModel>("ID REGISTRO CETIP VOICE", (t) => t.CetipVoiceId),
                new ColumnMap<TicketFundQuotaModel>("ADMINISTRADOR FUNDO INVESTIDO", (t) => t.IssuerName),
                new ColumnMap<TicketFundQuotaModel>("MESMA TITULARIDADE", (t) => t.HasSameOwnership.Value ? "SIM" : "NÃO"),
                new ColumnMap<TicketFundQuotaModel>("BANCO", (t) => t.FundBank),
                new ColumnMap<TicketFundQuotaModel>("AGÊNCIA", (t) => t.FundBranch),
                new ColumnMap<TicketFundQuotaModel>("CONTA", (t) => t.FundAccount),
                new ColumnMap<TicketFundQuotaModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketFundQuotaModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketFundQuotaModel>(package, title, mapper, data);
        }

        private void GetWorkSheetPrivateFixed(
            ExcelPackage package,
            string title,
            IEnumerable<TicketPrivateFixedIncomeModel> data
        )
        {
            var mapper = new ColumnMap<TicketPrivateFixedIncomeModel>[] {
                new ColumnMap<TicketPrivateFixedIncomeModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketPrivateFixedIncomeModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketPrivateFixedIncomeModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketPrivateFixedIncomeModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketPrivateFixedIncomeModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CONTA SELIC / CETIP CARTEIRA", (t) => t.PortfolioCetipAccount),
                new ColumnMap<TicketPrivateFixedIncomeModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketPrivateFixedIncomeModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketPrivateFixedIncomeModel>("VALOR DA OPERAÇÃO", (t) => t.OperationValue),
                new ColumnMap<TicketPrivateFixedIncomeModel>("QTDE", (t) => t.Amount),
                new ColumnMap<TicketPrivateFixedIncomeModel>("PU", (t) => t.UnitPrice),
                new ColumnMap<TicketPrivateFixedIncomeModel>("TERMO", (t) => t.IsTerm ? "SIM" : "NÃO"),
                new ColumnMap<TicketPrivateFixedIncomeModel>("DATA DE AQUISIÇÃO", (t) => t.AcquisitionDate.HasValue ? t.AcquisitionDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketPrivateFixedIncomeModel>("TÍTULO", (t) => t.AssetType),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CÓDIGO DO TÍTULO", (t) => t.AssetCode),
                new ColumnMap<TicketPrivateFixedIncomeModel>("VENCIMENTO DO TÍTULO", (t) => t.ExpirationDate.ToString("yyyy-MM-dd")),
                new ColumnMap<TicketPrivateFixedIncomeModel>("TAXA DE EMISSÃO", (t) => t.IssueFee),
                new ColumnMap<TicketPrivateFixedIncomeModel>("DATA DE EMISSÃO", (t) => t.IssueDate.ToString("yyyy-MM-dd")),
                new ColumnMap<TicketPrivateFixedIncomeModel>("NOME CONTRAPARTE", (t) => t.CounterpartName),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CNPJ CONTRAPARTE", (t) => t.CounterpartDocument),
                new ColumnMap<TicketPrivateFixedIncomeModel>("CONTA SELIC / CETIP DA CONTRAPARTE", (t) => t.CounterpartCetipAccount),
                new ColumnMap<TicketPrivateFixedIncomeModel>("INDEXADOR", (t) => t.Index),
                new ColumnMap<TicketPrivateFixedIncomeModel>("PERCENTUAL DO INDEXADOR", (t) => t.IndexPercent),
                new ColumnMap<TicketPrivateFixedIncomeModel>("BASE INDEXADOR", (t) => t.IndexBase),
                new ColumnMap<TicketPrivateFixedIncomeModel>("EMISSOR", (t) => t.Issuer),
                new ColumnMap<TicketPrivateFixedIncomeModel>("TIPO DE JUROS", (t) => t.InterestType),
                new ColumnMap<TicketPrivateFixedIncomeModel>("COMANDO", (t) => t.Command),
                new ColumnMap<TicketPrivateFixedIncomeModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketPrivateFixedIncomeModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketPrivateFixedIncomeModel>(package, title, mapper, data);
        }

        private void GetWorkSheetPublicFixed(
            ExcelPackage package,
            string title,
            IEnumerable<TicketPublicFixedIncomeModel> data,
            IDictionary<short, string> settlements
        )
        {
            var term = settlements.FirstOrDefault(t => t.Value == "Term");

            var mapper = new ColumnMap<TicketPublicFixedIncomeModel>[] {
                new ColumnMap<TicketPublicFixedIncomeModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketPublicFixedIncomeModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketPublicFixedIncomeModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketPublicFixedIncomeModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketPublicFixedIncomeModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketPublicFixedIncomeModel>("TIPO LIQUIDAÇÃO", (t) => settlements[t.SettlementTypeId]),
                new ColumnMap<TicketPublicFixedIncomeModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketPublicFixedIncomeModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketPublicFixedIncomeModel>("CONTA SELIC / CETIP CARTEIRA", (t) => t.PortfolioSelicAccount),
                new ColumnMap<TicketPublicFixedIncomeModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketPublicFixedIncomeModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketPublicFixedIncomeModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketPublicFixedIncomeModel>("VALOR DA OPERAÇÃO", (t) => t.OperationValue),
                new ColumnMap<TicketPublicFixedIncomeModel>("QTDE", (t) => t.Amount),
                new ColumnMap<TicketPublicFixedIncomeModel>("PU", (t) => t.UnitPrice),
                new ColumnMap<TicketPublicFixedIncomeModel>("TERMO", (t) => term.Value != null && t.SettlementTypeId == term.Key ? "SIM" : "NÃO"),
                new ColumnMap<TicketPublicFixedIncomeModel>("DATA DE LIQUIDAÇÃO", (t) => t.SettlementDate.HasValue ? t.SettlementDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketPublicFixedIncomeModel>("TÍTULO", (t) => t.Security),
                new ColumnMap<TicketPublicFixedIncomeModel>("CÓDIGO DO TÍTULO", (t) => t.SecurityId),
                new ColumnMap<TicketPublicFixedIncomeModel>("VENCIMENTO DO TÍTULO", (t) => t.ExpirationDate.HasValue ? t.ExpirationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketPublicFixedIncomeModel>("TAXA DE EMISSÃO", (t) => t.IssueTax),
                new ColumnMap<TicketPublicFixedIncomeModel>("DATA DE EMISSÃO", (t) => t.IssueDate.HasValue ? t.IssueDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketPublicFixedIncomeModel>("NOME CONTRAPARTE", (t) => t.CounterpartName),
                new ColumnMap<TicketPublicFixedIncomeModel>("CNPJ CONTRAPARTE", (t) => t.CounterpartDocument),
                new ColumnMap<TicketPublicFixedIncomeModel>("CONTA SELIC / CETIP DA CONTRAPARTE", (t) => t.CounterpartSelicAccount),
                new ColumnMap<TicketPublicFixedIncomeModel>("COMANDO", (t) => t.Command),
                new ColumnMap<TicketPublicFixedIncomeModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketPublicFixedIncomeModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketPublicFixedIncomeModel>(package, title, mapper, data);
        }

        private void GetWorkSheetFutures(
            ExcelPackage package,
            string title,
            IEnumerable<TicketFuturesModel> data
        )
        {
            var mapper = new ColumnMap<TicketFuturesModel>[] {
                new ColumnMap<TicketFuturesModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketFuturesModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketFuturesModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketFuturesModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketFuturesModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketFuturesModel>("DATA DO PREGÃO", (t) => t.TradingDate.HasValue ? t.TradingDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketFuturesModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketFuturesModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketFuturesModel>("CONTA DA CARTEIRA NA CLEARING", (t) => t.PortfolioB3Account),
                new ColumnMap<TicketFuturesModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketFuturesModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketFuturesModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketFuturesModel>("QTDE NEGOCIADA", (t) => t.Amount),
                new ColumnMap<TicketFuturesModel>("PU / PREÇO", (t) => t.UnitPrice),
                new ColumnMap<TicketFuturesModel>("PERCENTUAL DE DESCONTO", (t) => t.PercentageDiscount),
                new ColumnMap<TicketFuturesModel>("ATIVO", (t) => t.PaperCode),
                new ColumnMap<TicketFuturesModel>("NOME CONTRAPARTE / CORRETORA", (t) => t.Broker),
                new ColumnMap<TicketFuturesModel>("CNPJ CONTRAPARTE / CORRETORA", (t) => t.BrokerDocument),
                new ColumnMap<TicketFuturesModel>("CONTA DA CORRETORA NA CLEARING", (t) => t.BrokerAccount),
                new ColumnMap<TicketFuturesModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketFuturesModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketFuturesModel>(package, title, mapper, data);
        }

        private void GetWorkSheetMargin(
            ExcelPackage package,
            string title,
            IEnumerable<TicketMarginModel> data
        )
        {
            var mapper = new ColumnMap<TicketMarginModel>[] {
                new ColumnMap<TicketMarginModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketMarginModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketMarginModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketMarginModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketMarginModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketMarginModel>("TIPO DE MERCADO", (t) => t.MarketTypeId),
                new ColumnMap<TicketMarginModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketMarginModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketMarginModel>("CONTA SELIC / CETIP CARTEIRA", (t) => t.PortfolioClearingAccount),
                new ColumnMap<TicketMarginModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketMarginModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketMarginModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketMarginModel>("VALOR DA OPERAÇÃO", (t) => t.OperationValue),
                new ColumnMap<TicketMarginModel>("QTDE", (t) => t.Amount),
                new ColumnMap<TicketMarginModel>("PU", (t) => t.UnitPrice),
                new ColumnMap<TicketMarginModel>("TÍTULO", (t) => t.SecurityName),
                new ColumnMap<TicketMarginModel>("CÓDIGO DO TÍTULO", (t) => t.SecurityCode),
                new ColumnMap<TicketMarginModel>("VENCIMENTO DO TÍTULO", (t) => t.DueDate.HasValue ? t.DueDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketMarginModel>("DATA DE AQUISIÇÃO", (t) => t.PurchaseDate.HasValue ? t.PurchaseDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketMarginModel>("NOME CONTRAPARTE / CORRETORA", (t) => t.CounterpartName),
                new ColumnMap<TicketMarginModel>("CNPJ CONTRAPARTE", (t) => t.CounterpartDocument),
                new ColumnMap<TicketMarginModel>("CONTA SELIC / CETIP DA CONTRAPARTE", (t) => t.CounterpartClearingAccount),
                new ColumnMap<TicketMarginModel>("CONTA BROKER CORRETORA", (t) => t.CounterpartBrokerAccount),
                new ColumnMap<TicketMarginModel>("BANCO", (t) => t.Bank),
                new ColumnMap<TicketMarginModel>("AGÊNCIA", (t) => t.Branch),
                new ColumnMap<TicketMarginModel>("CONTA", (t) => t.Account),
                new ColumnMap<TicketMarginModel>("INDEXADOR", (t) => t.Index),
                new ColumnMap<TicketMarginModel>("PERCENTUAL DO INDEXADOR", (t) => t.IndexPercent),
                new ColumnMap<TicketMarginModel>("BASE INDEXADOR", (t) => t.IndexBase),
                new ColumnMap<TicketMarginModel>("EMISSOR", (t) => t.Issuer),
                new ColumnMap<TicketMarginModel>("TIPO DE JUROS", (t) => t.InterestType),
                new ColumnMap<TicketMarginModel>("COMANDO", (t) => t.Command),
                new ColumnMap<TicketMarginModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketMarginModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketMarginModel>(package, title, mapper, data);
        }

        private void GetWorkSheetContracted(
            ExcelPackage package,
            string title,
            IEnumerable<TicketContractedModel> data
        )
        {
            var mapper = new ColumnMap<TicketContractedModel>[] {
                new ColumnMap<TicketContractedModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketContractedModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketContractedModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketContractedModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketContractedModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketContractedModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketContractedModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketContractedModel>("CONTA SELIC CARTEIRA / FUNDO", (t) => t.PortfolioSelicAccount),
                new ColumnMap<TicketContractedModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketContractedModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketContractedModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketContractedModel>("TAXA", (t) => t.IssueTax),
                new ColumnMap<TicketContractedModel>("NOME CONTRAPARTE", (t) => t.CounterpartName),
                new ColumnMap<TicketContractedModel>("CNPJ CONTRAPARTE", (t) => t.CounterpartDocument),
                new ColumnMap<TicketContractedModel>("CONTA SELIC CONTRAPARTE", (t) => t.CounterpartSelicAccount),
                new ColumnMap<TicketContractedModel>("TAXA", (t) => t.IssueTax),
                new ColumnMap<TicketContractedModel>("TÍTULO", (t) => t.Security),
                new ColumnMap<TicketContractedModel>("CÓDIGO DO TÍTULO", (t) => t.SecurityId),
                new ColumnMap<TicketContractedModel>("VENCIMENTO DO TÍTULO", (t) => t.ExpirationDate.HasValue ? t.ExpirationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketContractedModel>("DATA DE RETORNO", (t) => t.ReturnDate.HasValue ? t.ReturnDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketContractedModel>("VALOR DA OPERAÇÃO", (t) => t.OperationValue),
                new ColumnMap<TicketContractedModel>("PU IDA", (t) => t.UnitPriceOutward),
                new ColumnMap<TicketContractedModel>("QTDE", (t) => t.Amount),
                new ColumnMap<TicketContractedModel>("PU VOLTA", (t) => t.UnitPriceReturn),
                new ColumnMap<TicketContractedModel>("VALOR IDA", (t) => t.ValueOutward),
                new ColumnMap<TicketContractedModel>("VALOR VOLTA", (t) => t.ValueReturn),
                new ColumnMap<TicketContractedModel>("COMANDO", (t) => t.Command),
                new ColumnMap<TicketContractedModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketContractedModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketContractedModel>(package, title, mapper, data);
        }

        private void GetWorkSheetTerm(
            ExcelPackage package,
            string title,
            IEnumerable<TicketCurrencyTermModel> data,
            IDictionary<short, string> settlements)
        {
            var currencies = new Dictionary<int, string>();

            currencies.Add(1, "Dólar");
            currencies.Add(2, "Euro");
            currencies.Add(3, "Iene");

            var mapper = new ColumnMap<TicketCurrencyTermModel>[] {
                new ColumnMap<TicketCurrencyTermModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketCurrencyTermModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketCurrencyTermModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketCurrencyTermModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketCurrencyTermModel>("TIPO OPERAÇÃO", (t) => ((TipoOperacaoEnum)t.OperationTypeId).GetEnumName()),
                new ColumnMap<TicketCurrencyTermModel>("TIPO LIQUIDAÇÃO", (t) => t.CetipSettlement ? "CETIP" : null),
                new ColumnMap<TicketCurrencyTermModel>("NOME DO FUNDO", (t) => t.Portfolio.Name),
                new ColumnMap<TicketCurrencyTermModel>("CNPJ", (t) => t.Portfolio.Document),
                new ColumnMap<TicketCurrencyTermModel>("CONTA SELIC / CETIP CARTEIRA", (t) => t.Portfolio.ClearingAccount),
                new ColumnMap<TicketCurrencyTermModel>("BANCO", (t) => t.Portfolio.Bank),
                new ColumnMap<TicketCurrencyTermModel>("AGÊNCIA", (t) => t.Portfolio.Branch),
                new ColumnMap<TicketCurrencyTermModel>("CONTA CORRENTE", (t) => t.Portfolio.Account),
                new ColumnMap<TicketCurrencyTermModel>("VALOR DA OPERAÇÃO", (t) => t.OperationValue),
                new ColumnMap<TicketCurrencyTermModel>("NÚMERO DO CONTRATO", (t) => t.ContractNumber),
                new ColumnMap<TicketCurrencyTermModel>("TAXA FUTURA", (t) => t.FutureFee),
                new ColumnMap<TicketCurrencyTermModel>("COTAÇÃO NO VENCIMENTO", (t) => t.QuoteExpirationTypeId == 1 ? "P-TAX - D1" : null),
                new ColumnMap<TicketCurrencyTermModel>("MOEDA", (t) => currencies[t.CurrencyId]),
                new ColumnMap<TicketCurrencyTermModel>("CROSS RATE", (t) => t.CrossRate ? "SIM" : "NÃO"),
                new ColumnMap<TicketCurrencyTermModel>("DATA DE VENCIMENTO", (t) => t.ExpirationDate.ToString("yyyy-MM-dd")),
                new ColumnMap<TicketCurrencyTermModel>("NOME CONTRAPARTE / CORRETORA", (t) => t.Counterpart.Name),
                new ColumnMap<TicketCurrencyTermModel>("CNPJ CONTRAPARTE", (t) => t.Counterpart.Document),
                new ColumnMap<TicketCurrencyTermModel>("CONTA CETIP DA CONTRAPARTE", (t) => t.Counterpart.ClearingAccount),
                new ColumnMap<TicketCurrencyTermModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketCurrencyTermModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketCurrencyTermModel>(package, title, mapper, data);
        }

        private void GetWorkSheetSwapCetip(
            ExcelPackage package,
            string title,
            IEnumerable<TicketSwapCetipModel> data
        )
        {
            var mapper = new ColumnMap<TicketSwapCetipModel>[] {
                new ColumnMap<TicketSwapCetipModel>("NÚMERO BOLETA", (t) => t.Id),
                new ColumnMap<TicketSwapCetipModel>("NOME DO GESTOR", (t) => t.AccountManagerName),
                new ColumnMap<TicketSwapCetipModel>("CNPJ GESTOR", (t) => t.AccountManagerDocument),
                new ColumnMap<TicketSwapCetipModel>("DATA OPERAÇÃO", (t) => t.OperationDate.HasValue ? t.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty),
                new ColumnMap<TicketSwapCetipModel>("NOME DO FUNDO", (t) => t.PortfolioName),
                new ColumnMap<TicketSwapCetipModel>("CNPJ", (t) => t.PortfolioDocument),
                new ColumnMap<TicketSwapCetipModel>("CONTA CETIP CARTEIRA", (t) => t.PortfolioCetipAccount),
                new ColumnMap<TicketSwapCetipModel>("BANCO", (t) => t.PortfolioBank),
                new ColumnMap<TicketSwapCetipModel>("AGÊNCIA", (t) => t.PortfolioBranch),
                new ColumnMap<TicketSwapCetipModel>("CONTA CORRENTE", (t) => t.PortfolioAccount),
                new ColumnMap<TicketSwapCetipModel>("VALOR BASE", (t) => t.OperationValue),
                new ColumnMap<TicketSwapCetipModel>("VENCIMENTO", (t) => t.ExpirationDate.ToString("yyyy-MM-dd")),
                new ColumnMap<TicketSwapCetipModel>("INDEXADOR -  ATIVO", (t) => t.ActiveIndex),
                new ColumnMap<TicketSwapCetipModel>("PERCENTUAL DO INDEXADOR - ATIVO", (t) => t.ActiveIndexPercent),
                new ColumnMap<TicketSwapCetipModel>("TAXA DO INDEXADOR - ATIVO", (t) => t.ActiveIndexTax),
                new ColumnMap<TicketSwapCetipModel>("BASE INDEXADOR - ATIVO", (t) => t.ActiveIndexBase),
                new ColumnMap<TicketSwapCetipModel>("TIPO DE JUROS - ATIVO", (t) => t.ActiveInterestType),
                new ColumnMap<TicketSwapCetipModel>("INDEXADOR - PASSIVO", (t) => t.PassiveIndex),
                new ColumnMap<TicketSwapCetipModel>("PERCENTUAL DO INDEXADOR - PASSIVO", (t) => t.PassiveIndexPercent),
                new ColumnMap<TicketSwapCetipModel>("TAXA DO INDEXADOR - PASSIVO", (t) => t.PassiveIndexTax),
                new ColumnMap<TicketSwapCetipModel>("BASE INDEXADOR - PASSIVO", (t) => t.PassiveIndexBase),
                new ColumnMap<TicketSwapCetipModel>("TIPO DE JUROS - PASSIVO", (t) => t.PassiveInterestType),
                new ColumnMap<TicketSwapCetipModel>("NOME CONTRAPARTE", (t) => t.CounterpartName),
                new ColumnMap<TicketSwapCetipModel>("CNPJ CONTRAPARTE", (t) => t.CounterpartDocument),
                new ColumnMap<TicketSwapCetipModel>("CONTA CETIP DA CONTRAPARTE", (t) => t.CounterpartCetipAccount),
                new ColumnMap<TicketSwapCetipModel>("COMANDO", (t) => t.Command),
                new ColumnMap<TicketSwapCetipModel>("OBSERVAÇÃO", (t) => t.Annotations),
                new ColumnMap<TicketSwapCetipModel>("STATUS", (t) => t.StatusDescription ?? DefaultStatus)
            };

            exportService.GenerateWorksheet<TicketSwapCetipModel>(package, title, mapper, data);
        }

        private void GetWorkSheetVariableIncome(
            ExcelPackage excelPackage,
            string titulo,
            TicketVariableIncomeModel[] lista,
            IDictionary<short, string> settlements
        )
        {
            var workSheet = excelPackage.Workbook.Worksheets.Add(titulo);
            workSheet.View.ShowGridLines = false;

            var campos = new string[]
            {
                "ID",
                "NOME DO GESTOR",
                "CNPJ GESTOR",
                "DATA OPERAÇÃO",
                "DATA B3",
                "CONTA B3",
                "CÓDIGO DA CORRETORA",
                "MERCADO",
                "COMPRA/VENDA",
                "TÍTULO",
                "QUANTIDADE",
                "VALOR",
                "STATUS"
            };

            for (int c = 0; c < campos.Length; c++)
            {
                workSheet.Cells[1, c + 1].Value = campos[c];
            }

            workSheet.Cells[1, 1, 1, campos.Length].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells[1, 1, 1, campos.Length].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#00067A"));
            workSheet.Cells[1, 1, 1, campos.Length].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

            int amountOfLines = lista.Sum(l => l.Items.Count);
            int actualLine = 1;
            for (var l = 0; l < lista.Count(); l++)
            {
                var ticket = lista[l];
                for (var linhaItem = 0; linhaItem < ticket.Items.Count; linhaItem++)
                {
                    actualLine++;
                    var item = ticket.Items[linhaItem];

                    if (actualLine % 2 == 1)
                    {
                        workSheet.Cells[actualLine, 1, actualLine, campos.Length].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[actualLine, 1, actualLine, campos.Length].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#F0F0F0"));
                    }


                    workSheet.Cells[actualLine, 1].Value = ticket.Id.GetValueOrDefault();
                    workSheet.Cells[actualLine, 2].Value = ticket.AccountManagerName;
                    workSheet.Cells[actualLine, 3].Value = ticket.AccountManagerDocument;
                    workSheet.Cells[actualLine, 4].Value = ticket.OperationDate.HasValue ? ticket.OperationDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                    workSheet.Cells[actualLine, 5].Value = ticket.StockExchangeDate.HasValue ? ticket.StockExchangeDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                    workSheet.Cells[actualLine, 6].Value = string.Format("{0} - {1}", ticket.ClientCode, ticket.ClientCodeDigit);
                    workSheet.Cells[actualLine, 7].Value = ticket.BrokerCode;
                    workSheet.Cells[actualLine, 8].Value = item.MarketType;
                    workSheet.Cells[actualLine, 9].Value = item.BuySell;
                    workSheet.Cells[actualLine, 10].Value = string.Format("{0} - {1} {2}", item.TradingCode?.Trim(), item.CompanyName?.Trim(), item.Specification?.Trim());
                    workSheet.Cells[actualLine, 11].Value = item.Amount;
                    workSheet.Cells[actualLine, 12].Value = item.Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-br"));
                    workSheet.Cells[actualLine, 13].Value = ticket.StatusDescription ?? DefaultStatus;
                }

            }

            workSheet.Cells[1, 1, 1, campos.Length].Style.Font.Bold = true;
            workSheet.Cells[1, 1, amountOfLines + 1, campos.Length].AutoFitColumns();
        }
    }
}