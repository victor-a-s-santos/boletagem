IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190617214825_1.0.0.1')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketSwapCetip]') AND [c].[name] = N'CounterpartDocument');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketSwapCetip] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[TicketSwapCetip] ALTER COLUMN [CounterpartDocument] varchar(14) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190617214825_1.0.0.1')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketPublicFixedIncome]') AND [c].[name] = N'Amount');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketPublicFixedIncome] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ticket].[TicketPublicFixedIncome] ALTER COLUMN [Amount] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190617214825_1.0.0.1')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketFutures]') AND [c].[name] = N'Amount');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketFutures] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Ticket].[TicketFutures] ALTER COLUMN [Amount] bigint NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190617214825_1.0.0.1')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketContracted]') AND [c].[name] = N'CounterpartCommand');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketContracted] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Ticket].[TicketContracted] ALTER COLUMN [CounterpartCommand] varchar(10) NOT NULL;
    ALTER TABLE [Ticket].[TicketContracted] ADD DEFAULT N'0' FOR [CounterpartCommand];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190617214825_1.0.0.1')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketContracted]') AND [c].[name] = N'Amount');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketContracted] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Ticket].[TicketContracted] ALTER COLUMN [Amount] bigint NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190617214825_1.0.0.1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190617214825_1.0.0.1', N'2.2.4-servicing-10062');
END;

GO

