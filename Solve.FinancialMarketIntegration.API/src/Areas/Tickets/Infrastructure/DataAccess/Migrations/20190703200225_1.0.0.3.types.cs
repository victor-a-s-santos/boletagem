using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1003types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Ticket",
                table: "Type",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.Sql(@"
                UPDATE Ticket.Type SET Name = 'Cotas' WHERE Id = 1 AND Name = 'Cotas'
                UPDATE Ticket.Type SET Name = 'Renda Fixa - Título Privado' WHERE Id = 2 AND Name = 'CETIP'
                UPDATE Ticket.Type SET Name = 'Renda Fixa - Título Público' WHERE Id = 3 AND Name = 'SELIC'
                UPDATE Ticket.Type SET Name = 'Compromissada de Compra' WHERE Id = 4 AND Name = 'Compromissada'
                UPDATE Ticket.Type SET Name = 'Futuros' WHERE Id = 5 AND Name = 'Futuros'
                UPDATE Ticket.Type SET Name = 'Swap CETIP' WHERE Id = 6 AND Name = 'SWAP CETIP'
                UPDATE Ticket.Type SET Name = 'Margem' WHERE Id = 7 AND Name = 'Margem'
                UPDATE Ticket.Type SET Name = 'Termo de Moeda' WHERE Id = 8 AND Name = 'Termo de Moeda'
                UPDATE Ticket.Type SET Name = 'Renda Variável' WHERE Id = 9 AND Name = 'Renda Variável'
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Ticket.Type SET Name = 'Cotas' WHERE Id = 1
                UPDATE Ticket.Type SET Name = 'CETIP' WHERE Id = 2
                UPDATE Ticket.Type SET Name = 'SELIC' WHERE Id = 3
                UPDATE Ticket.Type SET Name = 'Compromissada' WHERE Id = 4
                UPDATE Ticket.Type SET Name = 'Futuros' WHERE Id = 5
                UPDATE Ticket.Type SET Name = 'SWAP CETIP' WHERE Id = 6
                UPDATE Ticket.Type SET Name = 'Margem' WHERE Id = 7
                UPDATE Ticket.Type SET Name = 'Termo de Moeda' WHERE Id = 8
                UPDATE Ticket.Type SET Name = 'Renda Variável' WHERE Id = 9
            ");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Ticket",
                table: "Type",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: false);
        }
    }
}
