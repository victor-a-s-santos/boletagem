IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'WorkflowEndDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[Ticket] DROP COLUMN [WorkflowEndDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[Ticket]') AND [c].[name] = N'WorkflowStartDate');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[Ticket] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ticket].[Ticket] DROP COLUMN [WorkflowStartDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    CREATE TABLE [Ticket].[EventType] (
        [Id] smallint NOT NULL,
        [Description] varchar(150) NULL,
        CONSTRAINT [PK_EventType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    CREATE TABLE [Ticket].[History] (
        [Id] bigint NOT NULL IDENTITY,
        [Moment] datetimeoffset NOT NULL,
        [EventId] smallint NOT NULL,
        [Details] varchar(max) NULL,
        [Comments] varchar(max) NULL,
        [User] nvarchar(max) NULL,
        [TicketId] int NOT NULL,
        CONSTRAINT [PK_History] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_History_EventType_EventId] FOREIGN KEY ([EventId]) REFERENCES [Ticket].[EventType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_History_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    CREATE INDEX [IX_History_EventId] ON [Ticket].[History] ([EventId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    CREATE INDEX [IX_History_TicketId] ON [Ticket].[History] ([TicketId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[Ticket].[EventType]'))
        SET IDENTITY_INSERT [Ticket].[EventType] ON;
    INSERT INTO [Ticket].[EventType] ([Id], [Description])
    VALUES (CAST(1 AS smallint), 'Boleta Cancelada'),
    (CAST(2 AS smallint), 'Boleta Encaminhada'),
    (CAST(3 AS smallint), 'Boleta Em Elaboração'),
    (CAST(4 AS smallint), 'Status foi alterado');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[Ticket].[EventType]'))
        SET IDENTITY_INSERT [Ticket].[EventType] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    INSERT INTO Ticket.History
                                        SELECT 
                                            CreationDate AS Moment, 
                                            3 AS Boleta_Elaboracao, 
                                            NULL AS Details, 
                                            NULL AS Comments, 
                                            CreationUser AS [User], 
                                            Id AS TicketId  
                                        FROM 
                                            Ticket.Ticket

                                        INSERT INTO Ticket.History
                                        SELECT 
                                            B.CreationDate AS Moment, 
                                            2 AS Boleta_Encaminhada, 
                                            NULL AS Details, 
                                            NULL AS Comments, 
                                            B.CreationUser AS [User], 
                                            A.Id AS TicketId  
                                        FROM 
                                            Ticket.Ticket AS A 
                                        INNER JOIN
                                                Workflow.Workflow AS B ON B.Id = A.WorkflowId

                                        INSERT INTO Ticket.History
                                        SELECT 
                                            A.ChangeDate AS Moment, 
                                            4 AS Status_Alterado, 
                                            'O status ' + c.Name + ' foi ' + CASE WHEN a.IsApproved = 1 THEN 'Aprovado' ELSE 'Cancelado' END AS Details, 
                                            A.Comments AS Comments, 
                                            A.ChangeUser AS [User], 
                                            A.Id AS TicketId  
                                        FROM 
                                            Workflow.ApprovalStepResult AS A
                                        INNER JOIN
                                            Workflow.StepResult AS B ON B.Id = A.WorkflowStepResultId
                                        INNER JOIN
                                            Workflow.Status AS C ON C.ID  = B.WorkflowStatusId
                                        WHERE 
                                            IsApproved != null
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190722204507_1.0.0.8.draftcancel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190722204507_1.0.0.8.draftcancel', N'2.2.4-servicing-10062');
END;

GO

