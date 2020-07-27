IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [Index] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [IndexBase] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [IndexPercent] decimal(18,2) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [IndexTax] decimal(18,2) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [InterestType] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [Index] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [IndexBase] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [IndexPercent] decimal(18,2) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [IndexTax] decimal(18,2) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [InterestType] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [CetipVoiceId] nvarchar(20) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'PortfolioAccount');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[Ticket] ALTER COLUMN [PortfolioAccount] varchar(12) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[Ticket] ADD [PortfolioBank] varchar(20) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    ALTER TABLE [Ticket].[Ticket] ADD [PortfolioBranch] varchar(5) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712233120_1.0.0.5.newfields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190712233120_1.0.0.5.newfields', N'2.2.4-servicing-10062');
END;

GO

