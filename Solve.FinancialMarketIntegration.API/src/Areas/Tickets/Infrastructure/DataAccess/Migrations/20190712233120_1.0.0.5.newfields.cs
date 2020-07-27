using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1005newfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Index",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndexBase",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndexPercent",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterestType",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Index",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndexBase",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndexPercent",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterestType",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetipVoiceId",
                schema: "Ticket",
                table: "TicketFundQuotas",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PortfolioAccount",
                schema: "Ticket",
                table: "Ticket",
                unicode: false,
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioBank",
                schema: "Ticket",
                table: "Ticket",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioBranch",
                schema: "Ticket",
                table: "Ticket",
                unicode: false,
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "IndexBase",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "IndexPercent",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "InterestType",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "Index",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "IndexBase",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "IndexPercent",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "InterestType",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "CetipVoiceId",
                schema: "Ticket",
                table: "TicketFundQuotas");

            migrationBuilder.DropColumn(
                name: "PortfolioBank",
                schema: "Ticket",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PortfolioBranch",
                schema: "Ticket",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "PortfolioAccount",
                schema: "Ticket",
                table: "Ticket",
                unicode: false,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 12);
        }
    }
}
