using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1009cetip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                maxLength: 90,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                schema: "Ticket",
                table: "TicketMargin",
                maxLength: 90,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issuer",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "Issuer",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.AddColumn<decimal>(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndexTax",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);
        }
    }
}
