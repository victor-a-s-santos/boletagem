IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    ALTER TABLE [Workflow].[Workflow] ADD [EndDate] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    ALTER TABLE [Workflow].[Workflow] ADD [StartDate] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (1, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (2, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (3, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (4, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (5, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (6, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (7, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (8, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (9, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (10, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (11, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (12, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([WorkflowTypeId], [WorkflowStatusId], [IsActive], [isFirstStep], [CreationDate], [CreationUser])
    VALUES (14, CAST(15 AS smallint), 1, 0, '2019-07-08T15:40:44.688-03:00', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WorkflowTypeId', N'WorkflowStatusId', N'IsActive', N'isFirstStep', N'CreationDate', N'CreationUser') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    UPDATE Workflow.Workflow SET StartDate = CreationDate
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    UPDATE Workflow.Workflow SET EndDate = ChangeDate WHERE IsFinished = 1
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724202457_1.0.0.4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190724202457_1.0.0.4', N'2.2.4-servicing-10062');
END;

GO

