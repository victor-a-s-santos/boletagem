using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Migrations
{
    public partial class _1003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Signoff Roles
            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
                values: new object[,]
                {
                    { 30, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 895, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 895, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), null, "Alterar horário limite default signoff" },
                    { 31, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 895, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 895, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), null, "Estender horário limite signoff" },

                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Group",
                columns: new [] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { 8, DateTimeOffset.Now, "Signoff" }
                }
            );

            migrationBuilder.InsertData(
                schema: "Security",
                table: "GroupRole",
                columns: new[] { "GroupId", "RoleId" },
                values: new object[,]
                {
                    { 8, 30 },
                    { 8, 31 },
                });
            
            // Variable Income Roles

             migrationBuilder.InsertData(
                schema: "Security",
                table: "GroupRole",
                columns: new[] { "GroupId", "RoleId" },
                values: new object[,]
                {
                    { 1, 8 },
                });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[,]
                {
                    { 3, 8 },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 8, 30 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 8, 31 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Group",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[,]
                {
                    { 1, 8 },
                });
            
             migrationBuilder.InsertData(
                schema: "Security",
                table: "GroupRole",
                columns: new[] { "GroupId", "RoleId" },
                values: new object[,]
                {
                    { 3, 8 },
                });
        }
    }
}
