
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using TicketEntities = Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Microsoft.AspNetCore.Hosting;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Services
{
    public class FileService : IFileService
    {

        private FileDataContext Context { get; set; }
        public TicketDataContext TicketDataContext { get; set; }
        private ITicketBulkService<TicketVariableIncomeModel> TicketService { get; set; }
        private readonly IHostingEnvironment HostingEnvironment;

        public FileService(FileDataContext context, TicketDataContext ticketDataContext, ITicketBulkService<TicketVariableIncomeModel> ticketService, IHostingEnvironment hostingEnvironment)
        {
            this.Context = context;
            this.TicketDataContext = ticketDataContext;
            this.HostingEnvironment = hostingEnvironment;
            this.TicketService = ticketService;
        }

        public async Task<TicketEntities.Ticket> GetTicket()
        {
            return await this.TicketDataContext.Ticket.FirstOrDefaultAsync();
        }

        public async Task<bool> ProcessRVAsync(string fileBase64, int offset, string username)
        {
            string filename = this.GetRandomFilename("txt");

            // Status 1: File entity created
            Entities.File file = new Entities.File
            {
                Name = filename,
                IdFileType = 1,
                CreationDate = System.DateTimeOffset.Now,
                Status = 0
            };

            Context.File.Add(file);
            await Context.SaveChangesAsync();

            // Status 2: File saved on disk
            byte[] fileBytes = this.GetBytesFromBase64(fileBase64);
            await this.SaveFileOnDisk(fileBytes, filename);
            file.Status = 2;
            Context.File.Update(file);
            Context.SaveChanges();
        
            // Status 3: File processed into Database
            this.LoadFileInDatabase(fileBytes, file.Id, offset);
            file.Status = 3;
            Context.File.Update(file);
            Context.SaveChanges();
    
            // Status 4: Ticketed created
            await this.ProcessTickets(username, file.Id);
            file.Status = 4;
            Context.File.Update(file);
            Context.SaveChanges();
    
            return true;
        }

        #region Store file on disk
        private byte[] GetBytesFromBase64(string base64)
        {
            if (base64.IndexOf("base64,") == -1)
            {
                throw new Exception("Base64 preffix not found.");
            }
            base64 = base64.Substring(base64.IndexOf("base64,") + 7);
            return System.Convert.FromBase64String(base64);

        }

        private async Task<string> SaveFileOnDisk(byte[] file, string filename)
        {
            string path = this.GetPath(filename);
            FileStream fileStream = System.IO.File.Create(path);
            await fileStream.WriteAsync(file, 0, file.Length);
            return filename;
        }

        private string GetRandomFilename(string extension)
        {
            string filename = null;
            do
            {
                filename = string.Format("{0}.{1}", Guid.NewGuid().ToString(), extension);
            } while (this.FileExists(filename));

            return filename;
        }

        private bool FileExists(string filename)
        {
            return System.IO.File.Exists(this.GetPath(filename));
        }

        private string GetPath(string filename)
        {
            string path = "uploads/";
            string rootPath = this.HostingEnvironment.WebRootPath ?? this.HostingEnvironment.ContentRootPath;
            return System.IO.Path.Combine(rootPath, path, filename);
        }
        #endregion

        #region Process file -> DB
        private void LoadFileInDatabase(byte[] fileBytes, int fileId, int offset)
        {
            using (MemoryStream stream = new MemoryStream(fileBytes))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";

                    string brokerCode = "";

                    var dateFile = DateTimeOffset.Now;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Substring(0, 2) == "00")
                        {
                            brokerCode = line.Substring(6, 4);
                            var parsedDate = DateTime.ParseExact(line.Substring(22, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                            // TODO: capturar timezone do cliente
                            dateFile = new DateTimeOffset(parsedDate.Year, parsedDate.Month, parsedDate.Day, 0, 0, 0, new TimeSpan(0, offset, 0));
                        }
                        if (line.Substring(0, 2) == "01")
                        {
                            var parsedDate = DateTime.ParseExact(line.Substring(2, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                            FilePESC filePESC = new FilePESC
                            {
                                IdFile = fileId,

                                BrokerCode = brokerCode,
                                DateFile = dateFile,                                

                                DateMarket = new DateTimeOffset(parsedDate.Year, parsedDate.Month, parsedDate.Day, 0, 0, 0, new TimeSpan(0, offset, 0)),
                                TradingCode = line.Substring(10, 12),
                                TradingCodeBusinessCode = line.Substring(22, 10),
                                BuyOrSell = line.Substring(29, 1),
                                ClientCode = line.Substring(30, 7),
                                ClientCodeDigit = line.Substring(37, 1),
                                Amount = Int32.Parse(line.Substring(38, 15)),
                                PortfolioCode = line.Substring(53, 2),
                                PortfolioCodeDigit = line.Substring(55, 1),
                                CustodianUserCode = line.Substring(56, 5),
                                CustodianClientCode = line.Substring(61, 9),
                                CustodianClientCodeDigit = line.Substring(70, 1),
                                SettlementTypeOfSecondaryTerm = line.Substring(71, 1),
                                MarketType = line.Substring(72, 3),
                                Price = (decimal.Parse(line.Substring(75, 11)) / 100),
                                Factor = Int32.Parse(line.Substring(86, 7)),
                                SettlementType = line.Substring(93, 1),
                                AssetCode = line.Substring(94, 12),
                                ISINCode = line.Substring(106, 12),
                                ISINCodeDistribution = line.Substring(118, 3),
                                CompanyName = line.Substring(121, 12),
                                Specification = line.Substring(133, 10),
                                SpecificationIndicator = line.Substring(143, 1),
                                IsAfterMarket = line.Substring(144, 1),
                                StockExchangeCode = line.Substring(145, 1)
                            };
                                Context.FilePESC.Add(filePESC);
                        }
                        if (line.Substring(0, 2) == "99") { }
                    }
                }
            }
        }

        #endregion

        #region Process File DB Entry -> Tickets
        private async Task ProcessTickets(string username, int fileId) {
            List<TicketVariableIncomeModel> tickets = new List<TicketVariableIncomeModel>();

            var data = Context.FilePESC
                        .Where(t => t.IdFile == fileId)
                        .GroupBy(t => new
                        {
                            t.BrokerCode,
                            t.DateMarket,
                            t.ClientCode,
                            t.ClientCodeDigit
                        })
                        .ToList();

            foreach (var item in data)
            {
                var dataTicket = Context.FilePESC
                .Where(
                    t =>
                    t.IdFile == fileId &&
                    t.BrokerCode == item.Key.BrokerCode &&
                    t.DateMarket == item.Key.DateMarket &&
                    t.ClientCode == item.Key.ClientCode &&
                    t.ClientCodeDigit == item.Key.ClientCodeDigit
                )
                .GroupBy(t => new
                {
                    t.TradingCode,
                    t.BuyOrSell,
                    t.SettlementTypeOfSecondaryTerm,
                    t.Price,
                    t.Factor,
                    t.SettlementType,
                    t.AssetCode,
                    t.ISINCode,
                    t.ISINCodeDistribution,
                    t.CompanyName,
                    t.Specification,
                    t.SpecificationIndicator,
                    t.IsAfterMarket,
                    t.MarketType
                })
                .Select(t => new
                    {
                    t.Key.TradingCode,
                    t.Key.BuyOrSell,
                    t.Key.SettlementTypeOfSecondaryTerm,
                    t.Key.Price,
                    t.Key.Factor,
                    t.Key.SettlementType,
                    t.Key.AssetCode,
                    t.Key.ISINCode,
                    t.Key.ISINCodeDistribution,
                    t.Key.CompanyName,
                    t.Key.Specification,
                    t.Key.SpecificationIndicator,
                    t.Key.IsAfterMarket,
                    t.Key.MarketType,
                    Amount = t.Sum(c => c.Amount)
                    })
                .ToList();


                List<TicketVariableIncomeItemModel> ticketItemList = new List<TicketVariableIncomeItemModel>();

                foreach (var itemItem in dataTicket)
                {

                    TicketVariableIncomeItemModel ticketItem = new TicketVariableIncomeItemModel();

                    ticketItem.TradingCode = itemItem.TradingCode;
                    ticketItem.TradingCodeBusinessCode = "";
                    ticketItem.BuySell = itemItem.BuyOrSell;
                    ticketItem.Amount = itemItem.Amount;
                    ticketItem.SettlementTypeOfSecondaryTerm = itemItem.SettlementTypeOfSecondaryTerm;
                    ticketItem.Price = itemItem.Price;
                    ticketItem.Factor = itemItem.Factor;
                    ticketItem.SettlementType = itemItem.SettlementType;
                    ticketItem.AssetCode = itemItem.AssetCode;
                    ticketItem.ISINCode = itemItem.ISINCode;
                    ticketItem.ISINCodeDistribution = itemItem.ISINCodeDistribution;
                    ticketItem.CompanyName = itemItem.CompanyName;
                    ticketItem.Specification = itemItem.Specification;
                    ticketItem.SpecificationIndicator = itemItem.SpecificationIndicator;
                    ticketItem.IsAfterMarket = itemItem.IsAfterMarket;
                    ticketItem.MarketType = itemItem.MarketType;

                    ticketItemList.Add(ticketItem);

                }

                TicketVariableIncomeModel ticket = new TicketVariableIncomeModel();
                ticket.BrokerCode = item.Key.BrokerCode;
                ticket.OperationDate = System.DateTimeOffset.Now;
                ticket.StockExchangeDate = item.First().DateFile; // é o mesmo em todas as boletas
                ticket.ClientCode = item.Key.ClientCode;
                ticket.ClientCodeDigit = item.Key.ClientCodeDigit;
                ticket.Items = ticketItemList;

                tickets.Add(ticket);
                //saveTicket(ticket.ToJson());
                
            }
            await this.TicketService.RegisterNewBulkAsync(username, tickets);
        }

        #endregion

    }
}
