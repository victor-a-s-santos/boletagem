IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726190339_1.0.0.9.quota')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [QuotationDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726190339_1.0.0.9.quota')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190726190339_1.0.0.9.quota', N'2.2.4-servicing-10062');
END;

GO

