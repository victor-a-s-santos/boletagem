using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annotations",
                schema: "Ticket",
                table: "TicketFutures");

            migrationBuilder.RenameColumn(
                name: "Annotations",
                schema: "Ticket",
                table: "TicketSwapCetip",
                newName: "CreationUser");

            migrationBuilder.RenameColumn(
                name: "Annotations",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                newName: "CreationUser");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketVariableIncome",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketVariableIncome",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketVariableIncome",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketVariableIncome",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CounterpartName",
                schema: "Ticket",
                table: "TicketSwapCetip",
                unicode: false,
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketSwapCetip",
                type: "decimal(28,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketSwapCetip",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketSwapCetip",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketSwapCetip",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                type: "decimal(28,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                type: "decimal(28,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AssetType",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketMargin",
                type: "decimal(28,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketMargin",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketFutures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketFutures",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketFutures",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketFutures",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketFundQuotas",
                type: "decimal(28,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketFundQuotas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketFundQuotas",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketFundQuotas",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketFundQuotas",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                type: "decimal(28,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketContracted",
                type: "decimal(28,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketContracted",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketContracted",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketContracted",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketContracted",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketVariableIncome");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketVariableIncome");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketVariableIncome");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketVariableIncome");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketSwapCetip");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketSwapCetip");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketSwapCetip");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketPublicFixedIncome");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketPublicFixedIncome");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketPublicFixedIncome");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketPublicFixedIncome");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketMargin");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketFutures");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketFutures");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketFutures");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketFutures");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketFundQuotas");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketFundQuotas");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketFundQuotas");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketFundQuotas");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketCurrencyTerm");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketCurrencyTerm");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketCurrencyTerm");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketCurrencyTerm");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                schema: "Ticket",
                table: "TicketContracted");

            migrationBuilder.DropColumn(
                name: "ChangeUser",
                schema: "Ticket",
                table: "TicketContracted");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Ticket",
                table: "TicketContracted");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketContracted");

            migrationBuilder.RenameColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketSwapCetip",
                newName: "Annotations");

            migrationBuilder.RenameColumn(
                name: "CreationUser",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                newName: "Annotations");

            migrationBuilder.AlterColumn<string>(
                name: "CounterpartName",
                schema: "Ticket",
                table: "TicketSwapCetip",
                unicode: false,
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 90);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketSwapCetip",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketPublicFixedIncome",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)");

            migrationBuilder.AlterColumn<string>(
                name: "AssetType",
                schema: "Ticket",
                table: "TicketPrivateFixedIncome",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketMargin",
                type: "decimal(18,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Annotations",
                schema: "Ticket",
                table: "TicketFutures",
                unicode: false,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketFundQuotas",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketCurrencyTerm",
                type: "decimal(14,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationValue",
                schema: "Ticket",
                table: "TicketContracted",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,8)");
        }
    }
}
