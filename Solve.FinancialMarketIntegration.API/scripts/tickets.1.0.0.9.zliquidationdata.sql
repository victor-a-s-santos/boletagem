IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190729174003_1.0.0.9.zliquidationdata')
BEGIN
    EXEC sp_rename N'[Ticket].[TicketFundQuotas].[QuotationDate]', N'LiquidationDate', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190729174003_1.0.0.9.zliquidationdata')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190729174003_1.0.0.9.zliquidationdata', N'2.2.4-servicing-10062');
END;

GO

