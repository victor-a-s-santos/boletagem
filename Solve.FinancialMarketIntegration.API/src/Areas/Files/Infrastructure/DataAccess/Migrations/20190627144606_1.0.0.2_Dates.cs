using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.Migrations
{
    public partial class _1002_Dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateMarket",
                schema: "Files",
                table: "FilesPESC",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateFile",
                schema: "Files",
                table: "FilesPESC",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Files",
                table: "Files",
                unicode: false,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldUnicode: false);

            migrationBuilder.Sql(@"
                UPDATE
                    Files.Files
                SET
                    CreationDate = DATEADD(HOUR, 3, CreationDate)
            ");

            migrationBuilder.Sql(@"
                UPDATE 
                    Files.FilesPESC
                SET
                    DateMarket = DATEADD(HOUR, 3, DateMarket),
                    DateFile =
                        CASE
                            WHEN DATEPART(HOUR, DateFile) = 12 THEN DATEADD(HOUR, -9, DateFile)
                            ELSE DATEADD(HOUR, 3, DateFile)
                        END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE
                    Files.Files
                SET
                    CreationDate = DATEADD(HOUR, -3, CreationDate)
            ");

            migrationBuilder.Sql(@"
                UPDATE 
                    Files.FilesPESC
                SET
                    DateMarket = DATEADD(HOUR, -3, DateMarket),
                    DateFile = DATEADD(HOUR, 9, DateFile)
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateMarket",
                schema: "Files",
                table: "FilesPESC",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFile",
                schema: "Files",
                table: "FilesPESC",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Files",
                table: "Files",
                unicode: false,
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldUnicode: false);
        }
    }
}
