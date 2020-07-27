IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627174123_1.0.0.2_Dates')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Security].[UserPassword]') AND [c].[name] = N'CreationDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Security].[UserPassword] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Security].[UserPassword] ALTER COLUMN [CreationDate] datetimeoffset NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627174123_1.0.0.2_Dates')
BEGIN

                    UPDATE
                        [Security].UserPassword
                    SET
                        CreationDate = DATEADD(HOUR, 3, CreationDate)
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627174123_1.0.0.2_Dates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190627174123_1.0.0.2_Dates', N'2.2.4-servicing-10062');
END;

GO

