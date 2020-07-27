using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _1003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "Type",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "Name" },
                values: new object[,]
                {
                    { 14, null, null, new DateTimeOffset(new DateTime(2019, 6, 12, 16, 33, 58, 321, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Variável pelo Gestor" },
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 94, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 14 },
                    { 93, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 14 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 92, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 94, 93, (short)8, 14 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[,]
                {
                    { 41, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 92 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 91, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 92, 93, (short)7, 14 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[,]
                {
                    { 40, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 91 }
                });

            migrationBuilder.UpdateData(
                schema: "Workflow",
                table: "Type",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsActive",
                value: false
            );
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Workflow",
                table: "Type",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsActive",
                value: true
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalRole",
                keyColumn: "Id",
                keyValues: new object[] {
                    40, 41
                }
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValues: new object[] {
                    91
                }
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValues: new object[] {
                    92
                }
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "ApprovalStep",
                keyColumn: "Id",
                keyValues: new object[] {
                    93, 94
                }
            );

            migrationBuilder.DeleteData(
                schema: "Workflow",
                table: "Type",
                keyColumn: "Id",
                keyValues: new object[] {
                    14
                }
            );
        }
    }
}
