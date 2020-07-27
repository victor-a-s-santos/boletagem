IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketFutures]') AND [c].[name] = N'Annotations');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketFutures] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[TicketFutures] DROP COLUMN [Annotations];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    EXEC sp_rename N'[Ticket].[TicketSwapCetip].[Annotations]', N'CreationUser', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    EXEC sp_rename N'[Ticket].[TicketPrivateFixedIncome].[Annotations]', N'CreationUser', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketVariableIncome] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketVariableIncome] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketVariableIncome] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketVariableIncome] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketSwapCetip]') AND [c].[name] = N'CounterpartName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketSwapCetip] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ticket].[TicketSwapCetip] ALTER COLUMN [CounterpartName] varchar(90) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketSwapCetip]') AND [c].[name] = N'OperationValue');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketSwapCetip] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Ticket].[TicketSwapCetip] ALTER COLUMN [OperationValue] decimal(28,8) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketSwapCetip] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketSwapCetip] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketSwapCetip] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketPublicFixedIncome]') AND [c].[name] = N'OperationValue');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketPublicFixedIncome] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Ticket].[TicketPublicFixedIncome] ALTER COLUMN [OperationValue] decimal(28,8) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPublicFixedIncome] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPublicFixedIncome] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPublicFixedIncome] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPublicFixedIncome] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketPrivateFixedIncome]') AND [c].[name] = N'OperationValue');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketPrivateFixedIncome] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ALTER COLUMN [OperationValue] decimal(28,8) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketPrivateFixedIncome]') AND [c].[name] = N'AssetType');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketPrivateFixedIncome] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ALTER COLUMN [AssetType] nvarchar(20) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketMargin]') AND [c].[name] = N'OperationValue');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketMargin] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Ticket].[TicketMargin] ALTER COLUMN [OperationValue] decimal(28,8) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFutures] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFutures] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFutures] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFutures] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketFundQuotas]') AND [c].[name] = N'OperationValue');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketFundQuotas] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Ticket].[TicketFundQuotas] ALTER COLUMN [OperationValue] decimal(28,8) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketFundQuotas] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketCurrencyTerm]') AND [c].[name] = N'OperationValue');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketCurrencyTerm] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Ticket].[TicketCurrencyTerm] ALTER COLUMN [OperationValue] decimal(28,8) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketCurrencyTerm] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketCurrencyTerm] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketCurrencyTerm] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketCurrencyTerm] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketContracted]') AND [c].[name] = N'OperationValue');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketContracted] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Ticket].[TicketContracted] ALTER COLUMN [OperationValue] decimal(28,8) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketContracted] ADD [ChangeDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketContracted] ADD [ChangeUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketContracted] ADD [CreationDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    ALTER TABLE [Ticket].[TicketContracted] ADD [CreationUser] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190612145353_1.0.0.0')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190612145353_1.0.0.0', N'2.2.4-servicing-10062');
END;

GO

