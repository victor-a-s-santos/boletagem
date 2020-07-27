IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627144606_1.0.0.2_Dates')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Files].[FilesPESC]') AND [c].[name] = N'DateMarket');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Files].[FilesPESC] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Files].[FilesPESC] ALTER COLUMN [DateMarket] datetimeoffset NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627144606_1.0.0.2_Dates')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Files].[FilesPESC]') AND [c].[name] = N'DateFile');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Files].[FilesPESC] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Files].[FilesPESC] ALTER COLUMN [DateFile] datetimeoffset NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627144606_1.0.0.2_Dates')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Files].[Files]') AND [c].[name] = N'CreationDate');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Files].[Files] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Files].[Files] ALTER COLUMN [CreationDate] datetimeoffset NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627144606_1.0.0.2_Dates')
BEGIN

                    UPDATE
                        Files.Files
                    SET
                        CreationDate = DATEADD(HOUR, 3, CreationDate)
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627144606_1.0.0.2_Dates')
BEGIN

                    UPDATE 
                        Files.FilesPESC
                    SET
                        DateMarket = DATEADD(HOUR, 3, DateMarket),
                        DateFile =
                            CASE
                                WHEN DATEPART(HOUR, DateFile) = 12 THEN DATEADD(HOUR, -9, DateFile)
                                ELSE DATEADD(HOUR, 3, DateFile)
                            END
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627144606_1.0.0.2_Dates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190627144606_1.0.0.2_Dates', N'2.2.4-servicing-10062');
END;

GO

