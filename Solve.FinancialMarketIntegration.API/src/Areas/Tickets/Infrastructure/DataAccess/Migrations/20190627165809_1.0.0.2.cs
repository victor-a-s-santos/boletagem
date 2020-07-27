using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WorkflowEndDate",
                schema: "Ticket",
                table: "Ticket",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkflowEndDate",
                schema: "Ticket",
                table: "Ticket");
        }
    }
}
