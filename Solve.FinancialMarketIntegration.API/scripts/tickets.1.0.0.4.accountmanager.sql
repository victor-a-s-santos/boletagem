IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704193000_1.0.0.4')
BEGIN
    ALTER TABLE [Ticket].[Ticket] ADD [AccountManagerDocument] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704193000_1.0.0.4')
BEGIN
    ALTER TABLE [Ticket].[Ticket] ADD [AccountManagerName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704193000_1.0.0.4')
BEGIN

                    UPDATE 
                        TT
                    SET
                        AccountManagerName = (
                            SELECT
                                AM.Name
                            FROM 
                                Ticket.AccountManager AM
                            WHERE 
                                Id = TT.AccountManagerId
                        ),
                        AccountManagerDocument = (
                            SELECT
                                AM.Document
                            FROM 
                                Ticket.AccountManager AM
                            WHERE
                                Id = TT.AccountManagerId
                        )
                    FROM
                        Ticket.Ticket AS TT
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704193000_1.0.0.4')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'AccountManagerDocument');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[Ticket] ALTER COLUMN [AccountManagerDocument] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704193000_1.0.0.4')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'AccountManagerName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ticket].[Ticket] ALTER COLUMN [AccountManagerName] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190704193000_1.0.0.4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190704193000_1.0.0.4', N'2.2.4-servicing-10062');
END;

GO

