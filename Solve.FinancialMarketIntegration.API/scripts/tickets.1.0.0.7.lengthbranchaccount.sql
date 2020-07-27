IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717212033_1.0.0.7.lengthbranchaccount')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'PortfolioBranch');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[Ticket] ALTER COLUMN [PortfolioBranch] varchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717212033_1.0.0.7.lengthbranchaccount')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'PortfolioAccount');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ticket].[Ticket] ALTER COLUMN [PortfolioAccount] varchar(15) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717212033_1.0.0.7.lengthbranchaccount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190717212033_1.0.0.7.lengthbranchaccount', N'2.2.4-servicing-10062');
END;

GO

