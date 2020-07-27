using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Migrations
{
    public partial class _1002_Dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Security",
                table: "UserPassword",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.Sql(@"
                UPDATE
                    [Security].UserPassword
                SET
                    CreationDate = DATEADD(HOUR, 3, CreationDate)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE
                    [Security].UserPassword
                SET
                    CreationDate = DATEADD(HOUR, -3, CreationDate)
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Security",
                table: "UserPassword",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
