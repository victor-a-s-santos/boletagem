using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1009zliquidationdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuotationDate",
                schema: "Ticket",
                table: "TicketFundQuotas",
                newName: "LiquidationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LiquidationDate",
                schema: "Ticket",
                table: "TicketFundQuotas",
                newName: "QuotationDate");
        }
    }
}
