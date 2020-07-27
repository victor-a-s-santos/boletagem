IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731143155_1.0.0.9.zzzsettlementdate')
BEGIN
    EXEC sp_rename N'[Ticket].[TicketFundQuotas].[LiquidationDate]', N'SettlementDate', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731143155_1.0.0.9.zzzsettlementdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190731143155_1.0.0.9.zzzsettlementdate', N'2.2.4-servicing-10062');
END;

GO

