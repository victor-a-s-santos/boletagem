IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726211108_1.0.0.5')
BEGIN
    UPDATE [Workflow].[Type] SET [IsActive] = 0
    WHERE [Id] = 9;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726211108_1.0.0.5')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (109, NULL, NULL, '2019-07-26T18:15:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 14);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726211108_1.0.0.5')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (108, NULL, NULL, '2019-07-26T18:15:44.694-03:00', N'admin', 1, 1, 91, 109, CAST(1 AS smallint), 14);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726211108_1.0.0.5')
BEGIN
    UPDATE [Workflow].[ApprovalStep] SET [IsFirstStep] = 0
    WHERE [Id] = 91;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726211108_1.0.0.5')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (42, 2, NULL, NULL, '2019-07-26T18:15:44.694-03:00', N'admin', 10, 108);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190726211108_1.0.0.5')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190726211108_1.0.0.5', N'2.2.4-servicing-10062');
END;

GO

