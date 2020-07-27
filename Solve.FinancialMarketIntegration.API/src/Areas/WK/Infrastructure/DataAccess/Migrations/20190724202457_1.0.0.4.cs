using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _1004 : Migration
    {

        private static DateTimeOffset initialSeedTimestamp = new DateTimeOffset(new DateTime(2019, 7, 8, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0));
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndDate",
                schema: "Workflow",
                table: "Workflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "Workflow",
                table: "Workflow",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            var types = Enum.GetValues(typeof(WorkflowTypes))
                            .Cast<int>()
                            .Where(w => w != (int)WorkflowTypes.VariableIncome);

            foreach (var type in types)
            {
                migrationBuilder.InsertData(
                    schema: "Workflow",
                    table: "ApprovalStep",
                    columns: new[] { "WorkflowTypeId", "WorkflowStatusId", "IsActive", "isFirstStep", "CreationDate", "CreationUser" },
                    values: new object[] { type, (int)WorkflowStatuses.CancelledByAccountManager, true, false, initialSeedTimestamp, "admin" });
            }

            migrationBuilder.Sql("UPDATE Workflow.Workflow SET StartDate = CreationDate");
            migrationBuilder.Sql("UPDATE Workflow.Workflow SET EndDate = ChangeDate WHERE IsFinished = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "Workflow",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "Workflow",
                table: "Workflow");

            migrationBuilder.Sql(String.Format(@"UPDATE 
                                    Workflow.ApprovalStep 
                                SET 
                                    NextStepRejectedId = NULL 
                                WHERE 
                                    NextStepRejectedId IN (
                                        SELECT 
                                            Id 
                                        FROM 
                                            Workflow.ApprovalStep 
                                        WHERE WorkflowStatusId = {0}  AND WorkflowTypeId <> {1}
                                    )

                                UPDATE A SET A.CurrentStepId = NULL, IsFinished = 1
                                FROM 
                                    Workflow.Workflow AS A
                                INNER JOIN
                                    Workflow.StepResult AS B ON B.Id = A.CurrentStepId
                                INNER JOIN 
                                    Workflow.ApprovalStep AS C ON C.ID = B.StepOriginId
                                WHERE 
                                    C.WorkflowStatusId = {0} AND C.WorkflowTypeId <> {1}

                                DELETE Workflow.ApprovalStep WHERE WorkflowStatusId = {0} AND WorkflowTypeId <> {1}",
                                           (int)WorkflowStatuses.CancelledByAccountManager, (int)WorkflowTypes.VariableIncome));
        }
    }
}
