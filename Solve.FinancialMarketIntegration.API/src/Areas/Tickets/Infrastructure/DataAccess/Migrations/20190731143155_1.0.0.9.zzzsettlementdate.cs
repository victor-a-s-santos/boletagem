using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1009zzzsettlementndate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LiquidationDate",
                schema: "Ticket",
                table: "TicketFundQuotas",
                newName: "SettlementDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SettlementDate",
                schema: "Ticket",
                table: "TicketFundQuotas",
                newName: "LiquidationDate");
        }
    }
}
