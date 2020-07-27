using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ticket");

            migrationBuilder.CreateTable(
                name: "AccountManager",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true),
                    SacCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fund",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    FundName = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    FundDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    FundCode = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    FundCetipAccount = table.Column<string>(unicode: false, maxLength: 8, nullable: false),
                    IsFIDC = table.Column<bool>(nullable: false),
                    FundClass = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FundCodeIssuer = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    IssueDate = table.Column<DateTimeOffset>(nullable: false),
                    IsNewIssue = table.Column<bool>(nullable: false),
                    FundType = table.Column<string>(type: "char(3)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fund", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketType",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationType",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettlementType",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountManager",
                schema: "Ticket",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(nullable: false),
                    AccountManagerId = table.Column<int>(nullable: false),
                    IsMaster = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountManager", x => new { x.AccountManagerId, x.UserIdentifier, x.IsMaster });
                    table.ForeignKey(
                        name: "FK_UserAccountManager_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalSchema: "Ticket",
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountManagerFund",
                schema: "Ticket",
                columns: table => new
                {
                    AccountManagerId = table.Column<int>(nullable: false),
                    FundId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountManagerFund", x => new { x.AccountManagerId, x.FundId });
                    table.ForeignKey(
                        name: "FK_AccountManagerFund_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalSchema: "Ticket",
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountManagerFund_Fund_FundId",
                        column: x => x.FundId,
                        principalSchema: "Ticket",
                        principalTable: "Fund",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    TypeId = table.Column<short>(nullable: false),
                    PortfolioName = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    PortfolioCode = table.Column<string>(unicode: false, maxLength: 21, nullable: true),
                    PortfolioDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    PortfolioAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    PortfolioClearingAccount = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    AccountManagerId = table.Column<int>(nullable: false),
                    OperationDate = table.Column<DateTimeOffset>(nullable: false),
                    Annotations = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    WorkflowId = table.Column<int>(nullable: true),
                    WorkflowStartDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalSchema: "Ticket",
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Type_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Ticket",
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketContracted",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationTypeId = table.Column<short>(nullable: false),
                    OperationValue = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Amount = table.Column<int>(nullable: true),
                    UnitPriceOutward = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    UnitPriceReturn = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    ReturnDate = table.Column<DateTimeOffset>(nullable: true),
                    ValueOutward = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    ValueReturn = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Security = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    SecurityId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    IssueTax = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketContracted", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketContracted_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketContracted_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketCurrencyTerm",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationValue = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    OperationTypeId = table.Column<short>(nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: false),
                    CetipSettlement = table.Column<bool>(nullable: false),
                    ContractNumber = table.Column<string>(nullable: true),
                    FutureFee = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    QuoteExpirationTypeId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<short>(nullable: false),
                    CrossRate = table.Column<bool>(nullable: false),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: true),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCurrencyTerm", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketCurrencyTerm_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketCurrencyTerm_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketFundQuotas",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationTypeId = table.Column<short>(nullable: false),
                    SettlementTypeId = table.Column<short>(nullable: false),
                    FundName = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    FundDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    IsFIDC = table.Column<bool>(nullable: false),
                    FundClassSeries = table.Column<string>(unicode: false, maxLength: 12, nullable: true),
                    FundIssuerName = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    IsNewFund = table.Column<bool>(nullable: false),
                    FundType = table.Column<string>(type: "char(3)", unicode: false, nullable: false),
                    FundBranch = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    FundBank = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    FundAcccount = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    FundMnemonicCode = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    FullRedeem = table.Column<bool>(nullable: true),
                    IsSecondaryMarket = table.Column<bool>(nullable: false),
                    IsIssueUnitPrice = table.Column<bool>(nullable: false),
                    OperationValue = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    QuotaValue = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    HasSameOwnership = table.Column<bool>(nullable: true),
                    IsCetipVoice = table.Column<bool>(nullable: true),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: true),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketFundQuotas", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketFundQuotas_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketFundQuotas_SettlementType_SettlementTypeId",
                        column: x => x.SettlementTypeId,
                        principalSchema: "Ticket",
                        principalTable: "SettlementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketFundQuotas_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketFutures",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationTypeId = table.Column<short>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TradingDate = table.Column<DateTimeOffset>(nullable: false),
                    PercentageDiscount = table.Column<decimal>(nullable: false),
                    PaperCode = table.Column<string>(nullable: false),
                    PaperSerie = table.Column<string>(maxLength: 10, nullable: false),
                    Annotations = table.Column<string>(unicode: false, nullable: true),
                    Broker = table.Column<string>(nullable: true),
                    BrokerCode = table.Column<string>(nullable: false),
                    BrokerAccount = table.Column<string>(nullable: false),
                    BrokerDocument = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketFutures", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketFutures_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketFutures_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketMargin",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationTypeId = table.Column<short>(nullable: false),
                    MarketTypeId = table.Column<short>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    OperationValue = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    SecurityType = table.Column<string>(nullable: true),
                    SecurityName = table.Column<string>(nullable: true),
                    SecurityCode = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    PurchaseDate = table.Column<DateTimeOffset>(nullable: true),
                    Quotation = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    Asset = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: true),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0"),
                    CounterpartBrokerAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMargin", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketMargin_MarketType_MarketTypeId",
                        column: x => x.MarketTypeId,
                        principalSchema: "Ticket",
                        principalTable: "MarketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketMargin_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketMargin_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketPrivateFixedIncome",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationTypeId = table.Column<short>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    IsSecondaryMarket = table.Column<bool>(nullable: false),
                    IsTerm = table.Column<bool>(nullable: false),
                    AcquisitionDate = table.Column<DateTimeOffset>(nullable: true),
                    OperationValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetType = table.Column<string>(maxLength: 6, nullable: false),
                    AssetCode = table.Column<string>(maxLength: 15, nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: false),
                    IssueFee = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    IssueDate = table.Column<DateTimeOffset>(nullable: false),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 90, nullable: true),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: true),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0"),
                    Annotations = table.Column<string>(nullable: true),
                    ObjectAction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrivateFixedIncome", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketPrivateFixedIncome_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPrivateFixedIncome_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketPublicFixedIncome",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    SettlementTypeId = table.Column<short>(nullable: true),
                    OperationTypeId = table.Column<short>(nullable: false),
                    Amount = table.Column<int>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SettlementDate = table.Column<DateTimeOffset>(nullable: true),
                    AcquisitionDate = table.Column<DateTimeOffset>(nullable: true),
                    OperationValue = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Security = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    SecurityId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    IssueTax = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    IssueDate = table.Column<DateTimeOffset>(nullable: true),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 90, nullable: false),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPublicFixedIncome", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketPublicFixedIncome_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "Ticket",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPublicFixedIncome_SettlementType_SettlementTypeId",
                        column: x => x.SettlementTypeId,
                        principalSchema: "Ticket",
                        principalTable: "SettlementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketPublicFixedIncome_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketSwapCetip",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    OperationValue = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: false),
                    Command = table.Column<string>(nullable: true),
                    ActiveIndex = table.Column<string>(nullable: true),
                    ActiveIndexPercent = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    ActiveIndexTax = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    ActiveIndexBase = table.Column<decimal>(nullable: false),
                    ActiveInterestType = table.Column<decimal>(nullable: false),
                    PassiveIndex = table.Column<string>(nullable: true),
                    PassiveIndexPercent = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PassiveIndexTax = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PassiveIndexBase = table.Column<decimal>(nullable: false),
                    PassiveInterestType = table.Column<decimal>(nullable: false),
                    Annotations = table.Column<string>(nullable: true),
                    CounterpartName = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    CounterpartDocument = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    CounterpartClearingAccount = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    CounterpartCommand = table.Column<string>(unicode: false, maxLength: 10, nullable: true, defaultValue: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSwapCetip", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketSwapCetip_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketVariableIncome",
                schema: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    BrokerCode = table.Column<string>(nullable: true),
                    OperationDate = table.Column<DateTimeOffset>(nullable: true),
                    StockExchangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ClientCode = table.Column<string>(nullable: true),
                    ClientCodeDigit = table.Column<string>(nullable: true),
                    SellTotal = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    BuyTotal = table.Column<decimal>(type: "decimal(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketVariableIncome", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketVariableIncome_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketVariableIncomeItems",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(nullable: false),
                    TradingCode = table.Column<string>(nullable: true),
                    TradingCodeBusinessCode = table.Column<string>(nullable: true),
                    BuyOrSell = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SettlementTypeOfSecondaryTerm = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Factor = table.Column<int>(nullable: false),
                    SettlementType = table.Column<string>(nullable: true),
                    AssetCode = table.Column<string>(nullable: true),
                    ISINCode = table.Column<string>(nullable: true),
                    ISINCodeDistribution = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    SpecificationIndicator = table.Column<string>(nullable: true),
                    IsAfterMarket = table.Column<string>(nullable: true),
                    MarketType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketVariableIncomeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketVariableIncomeItems_TicketVariableIncome_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "TicketVariableIncome",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "AccountManager",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Document", "Name", "SacCode" },
                values: new object[,]
                {
                    { 1, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "16.804.280/0001-20", "KRON GESTÃO DE INVESTIMENTOS", "" },
                    { 27, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "28.529.868/0001-21", "WNT GESTORA DE RECURSOS LTDA.", "" },
                    { 28, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "05.563.299/0001-06", "SOMMA INVESTIMENTOS S.A", "" },
                    { 29, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "09.442.277/0001-49", "AUSTRO CAPITAL", "AUSTROGE" },
                    { 30, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "12.263.316/0001-55", "NOVA MILANO INVESTIMENTOS LTDA", "" },
                    { 31, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "28.925.400/0001-27", "TRIGONO CAPITAL", "TRIGONOC" },
                    { 32, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "08.926.786/0001-84", "DETOMASO ADMINISTRADORA DE RECURSOS LTDA", "DETOMASO" },
                    { 33, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "12.678.380/0001-05", "VERITAS - VCM Gestão de Capital - (Log Found)", "NINKA FI" },
                    { 34, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "24.503.059/0001-60", "CIX CAPITAL", "" },
                    { 35, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "25.098.663/0001-11", "KP GESTÃO DE RECURSOS (demais fundos)", "KP GESTA" },
                    { 36, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "07.559.989/0001-17", "VALORA GESTÃO DE INVESTIMENTOS", "VALORA" },
                    { 38, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "27.390.441/0001-01", "HOA ASET MANAGEMENT GESTÃO DE RECURSOS", "" },
                    { 39, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "27.913.835/0001-99", "AVENTIS GESTÃO DE RECURSOS LTDA ME (TRIDAFIN)", "AVENTIS" },
                    { 40, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "29.263.481/0001-00", "EVEREST TRUST GESTORA DE RECURSOS LTDA.", "EVEREST" },
                    { 41, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "10.924.308/0001-87", "IGUANA INVESTIMENTOS LTDA.", "IGUANA" },
                    { 42, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "27.451.028/0001-00", "PRISMA CAPITAL LTDA", "PRISMA" },
                    { 43, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "16.789.525/0001-98", "XP VISTA ASSET MANAGMENT LTDA.", "XP VISTA" },
                    { 44, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "29.063.944/0001-90", "3J GESTORA DE RECURSOS LTDA.", "" },
                    { 45, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "27.690.986/0001-25", "ARC (antiga ARKON INVESTIMENTOS LTDA.)", "ARC CAPI" },
                    { 46, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "16.707.841/0001-73", "TYR GESTÃO DE RECURSOS LTDA.", "" },
                    { 47, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "12.620.044/0001-01", "ALPS CAPITAL GESTÃO E INVESTIMENTOS LTDA.", "ALPS CAP" },
                    { 48, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "20.043.909/0001-34", "FARM INVESTIMENTOS GESTÃO DE RECURSOS LTDA", "" },
                    { 26, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "21.551.986.0001-68", "PARAGUAÇU INVESTIMENTOS EIRELLI", "" },
                    { 25, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "15.032.609/0001-10", "STARBOARD ASSET LTDA", "STARBOAR" },
                    { 37, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "16.492.426/0001-40", "VALER INVESTIMENTOS (BRAZCORE)", "VALER" },
                    { 23, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "07.913.960/0001-91", "ITAJUI GESTÃO DE INVESTIMENTOS LTDA", "ITAJUI" },
                    { 2, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "05.043.746/0001-04", "ZERO CONFLICT WEALTH MANAGEMENT LTDA", "ZERO CON" },
                    { 3, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "07.252.227/0001-73", "GRAU GESTÃO", "GRAU GES" },
                    { 4, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "10.757.908/0001-06", "BERKANA INVESTIMENTOS E GESTÃO DE RECURSOS LTDA", "BERKANA" },
                    { 5, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "20.495.002/0001-06", "R2C", "R2C" },
                    { 6, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "25.098.663/0001-11", "KP GESTÃO DE RECURSOS", "KP GESTA" },
                    { 7, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "24.515.907/0001-51", "ATRIO GESTORA DE ATIVOS LTDA. (antiga Sul Brasil Gestora)", "ATRIO" },
                    { 8, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "09.311.153/0001-24", "RIO DAS PEDRAS ADMINISTRAÇÃO E PARTICIPAÇÕES LTDA", "RIODASPE" },
                    { 9, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "06.576.569/0001-86", "INTEGRAL INVESTIMENTOS LTDA", "" },
                    { 10, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "11.916.849/0001-26", "OURO PRETO GESTÃO DE RECURSOS S.A.", "OURO" },
                    { 11, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "04.661.817/0001-61", "KINEA PRIVATE VariableIncome INVESTIMENTOS S/A", "KINEAPEQ" },
                    { 12, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "09.121.454/0001-95", "TERCON INVESTIMENTOS LTDA", "J&M INV" },
                    { 13, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "17.480.662/0001-09", "FACT INVESTMENTS GESTAO DE RECURSOS LTDA", "FACT INV" },
                    { 14, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "09.456.933/0001-62", "QUATÁ GESTÃO DE RECURSOS LTDA", "QUATA" },
                    { 15, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "09.446.129/0001-00", "G5 ADMINISTRADORA DE RECURSOS LTDA.", "G5" },
                    { 16, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "05.451.668/0001-79", "POLO CAPITAL GESTÃO DE RECURSOS LTDA", "POLO CAP" },
                    { 17, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "16.492.426/0001-40", "VALER INVESTIMENTOS (BRAZCORE)", "VALER" },
                    { 18, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "08.592.877/0001-20", "DEL MONTE - GESTÃO DE INVESTIMENTOS LTDA", "" },
                    { 19, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "04.608.141/0001-42", "SFI INVESTIMENTOS LTDA", "SFI INV" },
                    { 20, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "08.639.165/0001-10", "ABC CAPITAL - GESTAO DE INVESTIMENTOS LTDA.", "" },
                    { 21, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "19.207.159/0001-00", "PRIVATTO INVESTIMENTOS", "" },
                    { 22, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "16.925.467/0001-82", "GLOBAL GESTÃO E INVESTIMENTOS LTDA", "GLOBAL" },
                    { 24, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "12.461.742/0001-01", "LATACHE GESTÃO DE RECURSOS LTDA", "LATACHE" }
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "MarketType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)3, "Renda Variável" },
                    { (short)4, "Dinheiro" },
                    { (short)2, "RF Título Público" },
                    { (short)1, "RF Título Privado" }
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "OperationType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)8, "Moeda Vendida" },
                    { (short)1, "Compra" },
                    { (short)2, "Venda" },
                    { (short)3, "Aplicacao" },
                    { (short)4, "Resgate" },
                    { (short)5, "Deposito" },
                    { (short)6, "Retirada" },
                    { (short)7, "Moeda Comprada" },
                    { (short)9, "Transferência" }
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "SettlementType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)4, "À Vista" },
                    { (short)3, "Termo" },
                    { (short)2, "TED" },
                    { (short)1, "CETIP" }
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "Type",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)6, "SWAP CETIP" },
                    { (short)7, "Margem" },
                    { (short)5, "Futuros" },
                    { (short)8, "Termo de Moeda" },
                    { (short)3, "SELIC" },
                    { (short)2, "CETIP" },
                    { (short)1, "Cotas" },
                    { (short)4, "Compromissada" },
                    { (short)9, "Renda Variável" }
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "UserAccountManager",
                columns: new[] { "AccountManagerId", "UserIdentifier", "IsMaster", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Id" },
                values: new object[] { 1, "gestor2", true, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 1 });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "UserAccountManager",
                columns: new[] { "AccountManagerId", "UserIdentifier", "IsMaster", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Id" },
                values: new object[] { 2, "gestor", true, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AccountManagerFund_FundId",
                schema: "Ticket",
                table: "AccountManagerFund",
                column: "FundId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AccountManagerId",
                schema: "Ticket",
                table: "Ticket",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TypeId",
                schema: "Ticket",
                table: "Ticket",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketContracted_OperationTypeId",
                schema: "Ticket",
                table: "TicketContracted",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCurrencyTerm_OperationTypeId",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFundQuotas_OperationTypeId",
                schema: "Ticket",
                table: "TicketFundQuotas",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFundQuotas_SettlementTypeId",
                schema: "Ticket",
                table: "TicketFundQuotas",
                column: "SettlementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFutures_OperationTypeId",
                schema: "Ticket",
                table: "TicketFutures",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMargin_MarketTypeId",
                schema: "Ticket",
                table: "TicketMargin",
                column: "MarketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMargin_OperationTypeId",
                schema: "Ticket",
                table: "TicketMargin",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPrivateFixedIncome_OperationTypeId",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPublicFixedIncome_OperationTypeId",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPublicFixedIncome_SettlementTypeId",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                column: "SettlementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketVariableIncomeItems_TicketId",
                schema: "Ticket",
                table: "TicketVariableIncomeItems",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountManagerFund",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketContracted",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketCurrencyTerm",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketFundQuotas",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketFutures",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketMargin",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketPrivateFixedIncome",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketPublicFixedIncome",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketSwapCetip",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketVariableIncomeItems",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "UserAccountManager",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "Fund",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "MarketType",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "OperationType",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "SettlementType",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketVariableIncome",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "AccountManager",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "Type",
                schema: "Ticket");
        }
    }
}
