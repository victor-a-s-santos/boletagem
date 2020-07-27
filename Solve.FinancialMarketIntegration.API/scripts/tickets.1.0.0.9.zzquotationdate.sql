IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731041508_1.0.0.9.zzquotationdate')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [QuotationDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731041508_1.0.0.9.zzquotationdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190731041508_1.0.0.9.zzquotationdate', N'2.2.4-servicing-10062');
END;

GO

