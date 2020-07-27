using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1006signoff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketSignoff",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketTypeId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false, maxLength: 60),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSignoff", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Ticket",
                table: "TicketSignoff",
                columns: new[] { "Id", "TicketTypeId", "Type", "TimeLimit", "CreationDate" },
                values: new object[,]
                {
                    { 01, (int)TicketTypes.FundQuota, "Cotas - CETIP", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 02, (int)TicketTypes.FundQuota, "Cotas - TED", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 03, (int)TicketTypes.PrivateFixedIncome, "RF - Título Privado", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 04, (int)TicketTypes.PublicFixedIncome, "RF - Título Público", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 05, (int)TicketTypes.Contracted, "Compromissada de Compra", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 06, (int)TicketTypes.Futures, "Futuros", new TimeSpan(17, 00, 0), DateTimeOffset.Now },
                    { 07, (int)TicketTypes.SwapCetip, "SWAP - CETIP", new TimeSpan(16, 00, 0), DateTimeOffset.Now },

                    { 08, (int)TicketTypes.Margin, "Margem (Depósito\\Vinculação) Título Público", new TimeSpan(11, 30, 0), DateTimeOffset.Now },
                    { 09, (int)TicketTypes.Margin, "Margem (Depósito\\Vinculação) Título Privado", new TimeSpan(11, 30, 0), DateTimeOffset.Now },
                    { 10, (int)TicketTypes.Margin, "Margem (Depósito\\Vinculação) Renda Variável", new TimeSpan(11, 30, 0), DateTimeOffset.Now },
                    { 11, (int)TicketTypes.Margin, "Margem (Retirada\\Desvinculação) Título Público", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 12, (int)TicketTypes.Margin, "Margem (Retirada\\Desvinculação) Título Privado", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 13, (int)TicketTypes.Margin, "Margem (Retirada\\Desvinculação) Renda Variável", new TimeSpan(17, 00, 0), DateTimeOffset.Now },
                    { 14, (int)TicketTypes.Margin, "Margem Dinheiro", new TimeSpan(16, 00, 0), DateTimeOffset.Now },

                    { 15, (int)TicketTypes.CurrencyTerm, "Termo de Moeda", new TimeSpan(16, 00, 0), DateTimeOffset.Now },
                    { 16, (int)TicketTypes.VariableIncome, "Renda Variável", new TimeSpan(17, 00, 0), DateTimeOffset.Now }
                });

            migrationBuilder.CreateTable(
                name: "TicketSignoffLog",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketSignoffId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSignoffLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSignoffLog_TicketSignoff_TicketSignoffId",
                        column: x => x.TicketSignoffId,
                        principalSchema: "Ticket",
                        principalTable: "TicketSignoff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketSignoffRequest",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    TicketSignoffId = table.Column<int>(nullable: false),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: false),
                    Justificative = table.Column<string>(nullable: false, maxLength: 280)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSignoffRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSignoffRequest_TicketSignoff_TicketSignoffId",
                        column: x => x.TicketSignoffId,
                        principalSchema: "Ticket",
                        principalTable: "TicketSignoff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.DropColumn(
                name: "OperationDate",
                schema: "Ticket",
                table: "TicketVariableIncome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketSignoffLog",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketSignoffRequest",
                schema: "Ticket");
            
            migrationBuilder.DropTable(
                name: "TicketSignoff",
                schema: "Ticket");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OperationDate",
                schema: "Ticket",
                table: "TicketVariableIncome",
                nullable: true);
        }
    }
}
