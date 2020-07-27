IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Type]'))
        SET IDENTITY_INSERT [Workflow].[Type] ON;
    INSERT INTO [Workflow].[Type] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [Name])
    VALUES (14, NULL, NULL, '2019-06-12T16:33:58.321-03:00', N'admin', 1, N'Fluxo de Renda Variável pelo Gestor');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Type]'))
        SET IDENTITY_INSERT [Workflow].[Type] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (94, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 14),
    (93, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(9 AS smallint), 14);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (92, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, 94, 93, CAST(8 AS smallint), 14);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (41, 4, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 15, 92);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (91, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 1, 92, 93, CAST(7 AS smallint), 14);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (40, 4, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 16, 91);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    UPDATE [Workflow].[Type] SET [IsActive] = 0
    WHERE [Id] = 13;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190712192955_1.0.0.3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190712192955_1.0.0.3', N'2.2.4-servicing-10062');
END;

GO

