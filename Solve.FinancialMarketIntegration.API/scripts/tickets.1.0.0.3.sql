IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703200225_1.0.0.3.types')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Type]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Type] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[Type] ALTER COLUMN [Name] varchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703200225_1.0.0.3.types')
BEGIN

                    UPDATE Ticket.Type SET Name = 'Cotas' WHERE Id = 1 AND Name = 'Cotas'
                    UPDATE Ticket.Type SET Name = 'Renda Fixa - Título Privado' WHERE Id = 2 AND Name = 'CETIP'
                    UPDATE Ticket.Type SET Name = 'Renda Fixa - Título Público' WHERE Id = 3 AND Name = 'SELIC'
                    UPDATE Ticket.Type SET Name = 'Compromissada de Compra' WHERE Id = 4 AND Name = 'Compromissada'
                    UPDATE Ticket.Type SET Name = 'Futuros' WHERE Id = 5 AND Name = 'Futuros'
                    UPDATE Ticket.Type SET Name = 'Swap CETIP' WHERE Id = 6 AND Name = 'SWAP CETIP'
                    UPDATE Ticket.Type SET Name = 'Margem' WHERE Id = 7 AND Name = 'Margem'
                    UPDATE Ticket.Type SET Name = 'Termo de Moeda' WHERE Id = 8 AND Name = 'Termo de Moeda'
                    UPDATE Ticket.Type SET Name = 'Renda Variável' WHERE Id = 9 AND Name = 'Renda Variável'
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703200225_1.0.0.3.types')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190703200225_1.0.0.3.types', N'2.2.4-servicing-10062');
END;

GO

