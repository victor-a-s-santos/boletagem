using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountManagerDocument",
                schema: "Ticket",
                table: "Ticket",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountManagerName",
                schema: "Ticket",
                table: "Ticket",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE 
                    TT
                SET
                    AccountManagerName = (
                        SELECT
                            AM.Name
                        FROM 
                            Ticket.AccountManager AM
                        WHERE 
                            Id = TT.AccountManagerId
                    ),
                    AccountManagerDocument = (
                        SELECT
                            AM.Document
                        FROM 
                            Ticket.AccountManager AM
                        WHERE
                            Id = TT.AccountManagerId
                    )
                FROM
                    Ticket.Ticket AS TT
            ");
            
            migrationBuilder.AlterColumn<string>(
                name: "AccountManagerDocument",
                schema: "Ticket",
                table: "Ticket",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AccountManagerName",
                schema: "Ticket",
                table: "Ticket",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountManagerDocument",
                schema: "Ticket",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "AccountManagerName",
                schema: "Ticket",
                table: "Ticket");
        }
    }
}
