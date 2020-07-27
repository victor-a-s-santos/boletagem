using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1008draftcancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkflowEndDate",
                schema: "Ticket",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "WorkflowStartDate",
                schema: "Ticket",
                table: "Ticket");

            migrationBuilder.CreateTable(
                name: "EventType",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Moment = table.Column<DateTimeOffset>(nullable: false),
                    EventId = table.Column<short>(nullable: false),
                    Details = table.Column<string>(unicode: false, nullable: true),
                    Comments = table.Column<string>(unicode: false, nullable: true),
                    User = table.Column<string>(nullable: true),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_EventType_EventId",
                        column: x => x.EventId,
                        principalSchema: "Ticket",
                        principalTable: "EventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Ticket",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_EventId",
                schema: "Ticket",
                table: "History",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_History_TicketId",
                schema: "Ticket",
                table: "History",
                column: "TicketId");

            migrationBuilder.InsertData(
                                       schema: "Ticket",
                                       table: "EventType",
                                       columns: new[] { "Id", "Description" },
                                       values: new object[,]
                                       {
                                           { (short)EventTypes.TicketCancelled, "Boleta Cancelada" },
                                           { (short)EventTypes.TicketForwarded, "Boleta Encaminhada" },
                                           { (short)EventTypes.TicketRegistered, "Boleta Em Elaboração" },
                                           { (short)EventTypes.TicketStatusChaged, "Status foi alterado" }});

            migrationBuilder.Sql(@"INSERT INTO Ticket.History
                                    SELECT 
                                        CreationDate AS Moment, 
                                        3 AS Boleta_Elaboracao, 
                                        NULL AS Details, 
                                        NULL AS Comments, 
                                        CreationUser AS [User], 
                                        Id AS TicketId  
                                    FROM 
                                        Ticket.Ticket

                                    INSERT INTO Ticket.History
                                    SELECT 
                                        B.CreationDate AS Moment, 
                                        2 AS Boleta_Encaminhada, 
                                        NULL AS Details, 
                                        NULL AS Comments, 
                                        B.CreationUser AS [User], 
                                        A.Id AS TicketId  
                                    FROM 
                                        Ticket.Ticket AS A 
                                    INNER JOIN
                                            Workflow.Workflow AS B ON B.Id = A.WorkflowId

                                    INSERT INTO Ticket.History
                                    SELECT 
                                        A.ChangeDate AS Moment, 
                                        4 AS Status_Alterado, 
                                        'O status ' + c.Name + ' foi ' + CASE WHEN a.IsApproved = 1 THEN 'Aprovado' ELSE 'Cancelado' END AS Details, 
                                        A.Comments AS Comments, 
                                        A.ChangeUser AS [User], 
                                        A.Id AS TicketId  
                                    FROM 
                                        Workflow.ApprovalStepResult AS A
                                    INNER JOIN
                                        Workflow.StepResult AS B ON B.Id = A.WorkflowStepResultId
                                    INNER JOIN
                                        Workflow.Status AS C ON C.ID  = B.WorkflowStatusId
                                    WHERE 
                                        IsApproved != null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "EventType",
                schema: "Ticket");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WorkflowEndDate",
                schema: "Ticket",
                table: "Ticket",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WorkflowStartDate",
                schema: "Ticket",
                table: "Ticket",
                nullable: true);
        }
    }
}
