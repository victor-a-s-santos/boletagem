using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.Migrations
{
    public partial class _0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Files");

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    IdFileType = table.Column<short>(unicode: false, nullable: false),
                    CreationDate = table.Column<DateTime>(unicode: false, nullable: false),
                    Status = table.Column<short>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilesPESC",
                schema: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFile = table.Column<int>(nullable: false),
                    BrokerCode = table.Column<string>(nullable: true),
                    DateFile = table.Column<DateTime>(nullable: false),
                    DateMarket = table.Column<DateTime>(nullable: false),
                    TradingCode = table.Column<string>(nullable: true),
                    TradingCodeBusinessCode = table.Column<string>(nullable: true),
                    BuyOrSell = table.Column<string>(nullable: true),
                    ClientCode = table.Column<string>(nullable: true),
                    ClientCodeDigit = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    PortfolioCode = table.Column<string>(nullable: true),
                    PortfolioCodeDigit = table.Column<string>(nullable: true),
                    CustodianUserCode = table.Column<string>(nullable: true),
                    CustodianClientCode = table.Column<string>(nullable: true),
                    CustodianClientCodeDigit = table.Column<string>(nullable: true),
                    SettlementTypeOfSecondaryTerm = table.Column<string>(nullable: true),
                    MarketType = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Factor = table.Column<int>(nullable: false),
                    SettlementType = table.Column<string>(nullable: true),
                    AssetCode = table.Column<string>(nullable: true),
                    ISINCode = table.Column<string>(nullable: true),
                    ISINCodeDistribution = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    SpecificationIndicator = table.Column<string>(nullable: true),
                    IsAfterMarket = table.Column<string>(nullable: true),
                    StockExchangeCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesPESC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                schema: "Files",
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

            migrationBuilder.InsertData(
                schema: "Files",
                table: "Type",
                columns: new[] { "Id", "Name" },
                values: new object[] { (short)1, "NEGS" });

            migrationBuilder.InsertData(
                schema: "Files",
                table: "Type",
                columns: new[] { "Id", "Name" },
                values: new object[] { (short)2, "PESQ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files",
                schema: "Files");

            migrationBuilder.DropTable(
                name: "FilesPESC",
                schema: "Files");

            migrationBuilder.DropTable(
                name: "Type",
                schema: "Files");
        }
    }
}
