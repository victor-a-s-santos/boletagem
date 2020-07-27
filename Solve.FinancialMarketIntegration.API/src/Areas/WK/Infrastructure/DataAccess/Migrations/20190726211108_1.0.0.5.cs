using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _1005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Workflow",
                table: "Type",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsActive",
                value: false);

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 109, null, null, new DateTimeOffset(new DateTime(2019, 7, 26, 18, 15, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 14 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 108, null, null, new DateTimeOffset(new DateTime(2019, 7, 26, 18, 15, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 91, 109, (short)1, 14 }
                });

            migrationBuilder.UpdateData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValue: 91,
                column: "IsFirstStep",
                value: false
            );

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[,]
                {
                    { 42, 2, null, null, new DateTimeOffset(new DateTime(2019, 7, 26, 18, 15, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 108 }
                });
        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Workflow",
                table: "Type",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsActive",
                value: true);

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalRole",
                keyColumn: "Id",
                keyValue: 42
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValue: 108
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValue: 109
            );

            migrationBuilder.UpdateData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValue: 91,
                column: "IsFirstStep",
                value: true
            );
        }
    }
}
