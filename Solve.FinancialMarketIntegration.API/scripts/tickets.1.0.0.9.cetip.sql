IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190727002108_1.0.0.9.cetip')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketPrivateFixedIncome]') AND [c].[name] = N'IndexTax');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketPrivateFixedIncome] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] DROP COLUMN [IndexTax];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190727002108_1.0.0.9.cetip')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketMargin]') AND [c].[name] = N'IndexTax');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketMargin] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ticket].[TicketMargin] DROP COLUMN [IndexTax];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190727002108_1.0.0.9.cetip')
BEGIN
    ALTER TABLE [Ticket].[TicketPrivateFixedIncome] ADD [Issuer] nvarchar(90) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190727002108_1.0.0.9.cetip')
BEGIN
    ALTER TABLE [Ticket].[TicketMargin] ADD [Issuer] nvarchar(90) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190727002108_1.0.0.9.cetip')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190727002108_1.0.0.9.cetip', N'2.2.4-servicing-10062');
END;

GO

