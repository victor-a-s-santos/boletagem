IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '20:00:00'
    WHERE [Id] = 6;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 7;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '14:30:00'
    WHERE [Id] = 8;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '14:30:00'
    WHERE [Id] = 9;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '14:30:00'
    WHERE [Id] = 10;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 11;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 12;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '20:00:00'
    WHERE [Id] = 13;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 14;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '19:00:00'
    WHERE [Id] = 15;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    UPDATE [Ticket].[TicketSignoff] SET [TimeLimit] = '20:00:00'
    WHERE [Id] = 16;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719191523_1.0.0.8.timelimit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190719191523_1.0.0.8.timelimit', N'2.2.4-servicing-10062');
END;

GO

