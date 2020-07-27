IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627165809_1.0.0.2')
BEGIN
    ALTER TABLE [Ticket].[Ticket] ADD [WorkflowEndDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627165809_1.0.0.2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190627165809_1.0.0.2', N'2.2.4-servicing-10062');
END;

GO

