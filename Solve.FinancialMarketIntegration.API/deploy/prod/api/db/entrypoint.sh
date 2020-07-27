#!/bin/sh
rm -rf Areas/Tickets/Infrastructure/DataAccess/Migrations
rm -rf Areas/Security/Infrastructure/DataAccess/Migrations
rm -rf Areas/WK/Infrastructure/DataAccess/Migrations
rm -rf Areas/Files/Infrastructure/DataAccess/Migrations

dotnet ef migrations add 0.0.0.1 --context Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.TicketDataContext -o Areas/Tickets/Infrastructure/DataAccess/Migrations
dotnet ef migrations add 0.0.0.1 --context Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.DataContext -o Areas/Security/Infrastructure/DataAccess/Migrations
dotnet ef migrations add 0.0.0.1 --context Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.WfDataContext -o Areas/WK/Infrastructure/DataAccess/Migrations
dotnet ef migrations add 0.0.0.1 --context Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.FileDataContext -o Areas/Files/Infrastructure/DataAccess/Migrations

dotnet ef database update --context Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.TicketDataContext
dotnet ef database update --context Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.DataContext
dotnet ef database update --context Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.WfDataContext
dotnet ef database update --context Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.FileDataContext

