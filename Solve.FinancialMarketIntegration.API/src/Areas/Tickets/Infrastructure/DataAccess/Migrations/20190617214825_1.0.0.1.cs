using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CounterpartDocument",
                schema: "Ticket",
                table: "TicketSwapCetip",
                unicode: false,
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                schema: "Ticket",
                table: "TicketFutures",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CounterpartCommand",
                schema: "Ticket",
                table: "TicketContracted",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "0",
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true,
                oldDefaultValue: "0");

            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                schema: "Ticket",
                table: "TicketContracted",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CounterpartDocument",
                schema: "Ticket",
                table: "TicketSwapCetip",
                unicode: false,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                schema: "Ticket",
                table: "TicketFutures",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CounterpartCommand",
                schema: "Ticket",
                table: "TicketContracted",
                unicode: false,
                maxLength: 10,
                nullable: true,
                defaultValue: "0",
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 10,
                oldDefaultValue: "0");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                schema: "Ticket",
                table: "TicketContracted",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
