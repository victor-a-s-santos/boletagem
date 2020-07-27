IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190710202031_1.0.0.2.WfStatus')
BEGIN

                    UPDATE Workflow.Status SET Name = 'Aguardando envio/recebimento da TED', ChangeDate = GETDATE(), ChangeUser = 'admin' WHERE Id = 10 AND Name = 'Aguardando Liquidação Middle'
                    UPDATE Workflow.Status SET Name = 'Em Liquidação TED', ChangeDate = GETDATE(), ChangeUser = 'admin' WHERE Id = 11 AND Name = 'Em Liquidação Middle'
                    UPDATE Workflow.Status SET Name = 'Cancelada TED', ChangeDate = GETDATE(), ChangeUser = 'admin' WHERE Id = 12 AND Name = 'Cancelada Middle'
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190710202031_1.0.0.2.WfStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190710202031_1.0.0.2.WfStatus', N'2.2.4-servicing-10062');
END;

GO

