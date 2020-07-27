IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF SCHEMA_ID(N'Workflow') IS NULL EXEC(N'CREATE SCHEMA [Workflow];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[Approval] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Approval] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[Status] (
        [Id] smallint NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[Type] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Type] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[ApprovalStep] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [NextStepApprovedId] int NULL,
        [NextStepRejectedId] int NULL,
        [WorkflowStatusId] smallint NULL,
        [IsFirstStep] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [WorkflowTypeId] int NULL,
        CONSTRAINT [PK_ApprovalStep] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApprovalStep_ApprovalStep_NextStepApprovedId] FOREIGN KEY ([NextStepApprovedId]) REFERENCES [Workflow].[ApprovalStep] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ApprovalStep_ApprovalStep_NextStepRejectedId] FOREIGN KEY ([NextStepRejectedId]) REFERENCES [Workflow].[ApprovalStep] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ApprovalStep_Status_WorkflowStatusId] FOREIGN KEY ([WorkflowStatusId]) REFERENCES [Workflow].[Status] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ApprovalStep_Type_WorkflowTypeId] FOREIGN KEY ([WorkflowTypeId]) REFERENCES [Workflow].[Type] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[ApprovalRole] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [ApprovalId] int NULL,
        [RoleId] int NOT NULL,
        [WorkflowApprovalStepId] int NULL,
        CONSTRAINT [PK_ApprovalRole] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApprovalRole_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Workflow].[Approval] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ApprovalRole_ApprovalStep_WorkflowApprovalStepId] FOREIGN KEY ([WorkflowApprovalStepId]) REFERENCES [Workflow].[ApprovalStep] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[ApprovalStepResult] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [ApprovalId] int NULL,
        [RoleId] int NOT NULL,
        [IsApproved] bit NULL,
        [Comments] nvarchar(max) NULL,
        [WorkflowStepResultId] int NULL,
        CONSTRAINT [PK_ApprovalStepResult] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApprovalStepResult_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Workflow].[Approval] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[StepResult] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [NextStepApprovedId] int NULL,
        [NextStepRejectedId] int NULL,
        [WorkflowStatusId] smallint NULL,
        [StepOriginId] int NOT NULL,
        [IsFirstStep] bit NOT NULL,
        [WorkflowId] int NULL,
        CONSTRAINT [PK_StepResult] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_StepResult_StepResult_NextStepApprovedId] FOREIGN KEY ([NextStepApprovedId]) REFERENCES [Workflow].[StepResult] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_StepResult_StepResult_NextStepRejectedId] FOREIGN KEY ([NextStepRejectedId]) REFERENCES [Workflow].[StepResult] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_StepResult_ApprovalStep_StepOriginId] FOREIGN KEY ([StepOriginId]) REFERENCES [Workflow].[ApprovalStep] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_StepResult_Status_WorkflowStatusId] FOREIGN KEY ([WorkflowStatusId]) REFERENCES [Workflow].[Status] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE TABLE [Workflow].[Workflow] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [CurrentStepId] int NULL,
        [IsFinished] bit NOT NULL,
        [WorkflowTypeId] int NOT NULL,
        CONSTRAINT [PK_Workflow] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Workflow_StepResult_CurrentStepId] FOREIGN KEY ([CurrentStepId]) REFERENCES [Workflow].[StepResult] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Workflow_Type_WorkflowTypeId] FOREIGN KEY ([WorkflowTypeId]) REFERENCES [Workflow].[Type] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Approval]'))
        SET IDENTITY_INSERT [Workflow].[Approval] ON;
    INSERT INTO [Workflow].[Approval] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Name])
    VALUES (1, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'Gestores'),
    (2, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'Administração Fiduciária'),
    (3, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'Open'),
    (4, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'Custódia'),
    (5, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'Middle Adim');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Approval]'))
        SET IDENTITY_INSERT [Workflow].[Approval] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Status]'))
        SET IDENTITY_INSERT [Workflow].[Status] ON;
    INSERT INTO [Workflow].[Status] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Name])
    VALUES (CAST(17 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Pendente aprovação do Gestor'),
    (CAST(16 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Concluído'),
    (CAST(15 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Cancelado pelo Gestor'),
    (CAST(14 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Em Registro do Voice'),
    (CAST(12 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Cancelada Middle'),
    (CAST(11 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Em Liquidação Middle'),
    (CAST(10 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Aguardando Liquidação Middle'),
    (CAST(9 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Cancelada pela Custódia'),
    (CAST(13 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Aguardando Registro do Voice'),
    (CAST(7 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Aguardando Liquidação Custódia'),
    (CAST(6 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Cancelada pela Liquidação'),
    (CAST(5 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Liquidada'),
    (CAST(4 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Em Liquidação'),
    (CAST(3 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Aguardando Liquidação'),
    (CAST(2 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Cancelada pela ADM'),
    (CAST(1 AS smallint), NULL, NULL, '2019-06-04T15:40:44.685-03:00', N'admin', N'Pendente de Aprovação pela ADM'),
    (CAST(8 AS smallint), NULL, NULL, '2019-06-04T15:40:44.686-03:00', N'admin', N'Em Liquidação Custódia');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Status]'))
        SET IDENTITY_INSERT [Workflow].[Status] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Type]'))
        SET IDENTITY_INSERT [Workflow].[Type] ON;
    INSERT INTO [Workflow].[Type] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [Name])
    VALUES (11, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, N'Fluxo de Margem Renda Variável'),
    (10, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, N'Fluxo de Futuros BMF'),
    (9, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, N'Fluxo de Boletas de Cotas - CETIP Voice'),
    (8, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, N'Fluxo de Boletas de Cotas - TED'),
    (7, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Termo de Moeda'),
    (3, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Renda Fixa Pública'),
    (5, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Swap CETIP'),
    (4, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Compromissada de Compra'),
    (2, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Renda Fixa Privada'),
    (1, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Boletas de Cotas - CETIP'),
    (12, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, N'Fluxo de Margem Dinheiro'),
    (6, NULL, NULL, '2019-06-04T15:40:44.687-03:00', N'admin', 1, N'Fluxo de Margem'),
    (13, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, N'Fluxo de Renda Variável');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'Name') AND [object_id] = OBJECT_ID(N'[Workflow].[Type]'))
        SET IDENTITY_INSERT [Workflow].[Type] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (6, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 1),
    (48, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 8),
    (47, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 8),
    (45, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 8),
    (57, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, NULL, NULL, CAST(9 AS smallint), 9),
    (54, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 9),
    (56, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 9),
    (51, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 9),
    (39, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 7),
    (63, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, 0, NULL, NULL, CAST(9 AS smallint), 10),
    (60, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 10),
    (69, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, 0, NULL, NULL, CAST(9 AS smallint), 11),
    (68, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 11),
    (66, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 11),
    (75, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(9 AS smallint), 12),
    (74, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 12),
    (72, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 12),
    (62, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 10),
    (78, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(15 AS smallint), 13),
    (41, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 7),
    (33, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 6),
    (5, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 1),
    (3, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 1),
    (12, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 2),
    (11, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 2),
    (9, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 2),
    (18, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 3),
    (17, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 3),
    (42, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 7),
    (15, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 3),
    (23, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 4),
    (21, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 4),
    (30, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 5),
    (29, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 5),
    (27, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(2 AS smallint), 5),
    (36, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 6),
    (35, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(5 AS smallint), 6),
    (24, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, NULL, NULL, CAST(6 AS smallint), 4),
    (77, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, NULL, NULL, CAST(16 AS smallint), 13);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (4, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 1, 0, 5, 6, CAST(4 AS smallint), 1),
    (10, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 11, 12, CAST(4 AS smallint), 2),
    (16, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 17, 18, CAST(4 AS smallint), 3),
    (22, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 23, 24, CAST(4 AS smallint), 4),
    (28, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 29, 30, CAST(4 AS smallint), 5),
    (34, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 35, 36, CAST(4 AS smallint), 6),
    (40, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 41, 42, CAST(4 AS smallint), 7),
    (46, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 47, 48, CAST(11 AS smallint), 8),
    (55, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, 56, 54, CAST(4 AS smallint), 9),
    (61, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, 0, 62, 63, CAST(8 AS smallint), 10),
    (67, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, 0, 68, 69, CAST(8 AS smallint), 11),
    (73, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, 74, 75, CAST(8 AS smallint), 12),
    (76, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 1, 77, 78, CAST(17 AS smallint), 13);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (3, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 4),
    (38, 4, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 16, 73),
    (35, 4, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 16, 67),
    (32, 4, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 16, 61),
    (29, 3, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 12, 55),
    (24, 5, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 14, 46),
    (18, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 34),
    (15, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 28),
    (21, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 40),
    (12, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 22),
    (9, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 16),
    (6, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 12, 10),
    (39, 1, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 27, 76);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (26, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 28, 30, CAST(3 AS smallint), 5),
    (32, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 34, 36, CAST(3 AS smallint), 6),
    (71, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 0, 73, 75, CAST(7 AS smallint), 12),
    (38, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 40, 42, CAST(3 AS smallint), 7),
    (14, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 16, 18, CAST(3 AS smallint), 3),
    (44, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 46, 48, CAST(10 AS smallint), 8),
    (53, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, 55, 54, CAST(3 AS smallint), 9),
    (8, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 10, 12, CAST(3 AS smallint), 2),
    (59, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, 0, 61, 63, CAST(7 AS smallint), 10),
    (65, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, 0, 67, 69, CAST(7 AS smallint), 11),
    (2, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 1, 0, 4, 6, CAST(3 AS smallint), 1),
    (20, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 0, 22, 24, CAST(3 AS smallint), 4);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (2, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 2),
    (5, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 8),
    (34, 4, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 15, 65),
    (8, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 14),
    (11, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 20),
    (31, 4, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 15, 59),
    (14, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 26),
    (17, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 32),
    (37, 4, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 15, 71),
    (20, 3, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 11, 38),
    (28, 3, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 11, 53),
    (23, 5, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 13, 44);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (64, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 1, 1, 65, 66, CAST(1 AS smallint), 11),
    (58, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 1, 1, 59, 60, CAST(1 AS smallint), 10),
    (52, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, 53, 57, CAST(14 AS smallint), 9),
    (31, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 32, 33, CAST(1 AS smallint), 6),
    (37, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 38, 39, CAST(1 AS smallint), 7),
    (25, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 26, 27, CAST(1 AS smallint), 5),
    (19, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 20, 21, CAST(1 AS smallint), 4),
    (13, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 14, 15, CAST(1 AS smallint), 3),
    (7, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 8, 9, CAST(1 AS smallint), 2),
    (1, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 1, 1, 2, 3, CAST(1 AS smallint), 1),
    (43, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 1, 1, 44, 45, CAST(1 AS smallint), 8),
    (70, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 1, 1, 71, 72, CAST(1 AS smallint), 12);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (1, 2, NULL, NULL, '2019-06-04T15:40:44.688-03:00', N'admin', 10, 1),
    (4, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 7),
    (7, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 13),
    (10, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 19),
    (13, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 25),
    (16, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 31),
    (19, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 37),
    (22, 2, NULL, NULL, '2019-06-04T15:40:44.689-03:00', N'admin', 10, 43),
    (27, 4, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 16, 52),
    (30, 2, NULL, NULL, '2019-06-04T15:40:44.692-03:00', N'admin', 10, 58),
    (33, 2, NULL, NULL, '2019-06-04T15:40:44.693-03:00', N'admin', 10, 64),
    (36, 2, NULL, NULL, '2019-06-04T15:40:44.694-03:00', N'admin', 10, 70);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (50, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 0, 52, 57, CAST(13 AS smallint), 9);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (26, 4, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 15, 50);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] ON;
    INSERT INTO [Workflow].[ApprovalStep] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [IsActive], [IsFirstStep], [NextStepApprovedId], [NextStepRejectedId], [WorkflowStatusId], [WorkflowTypeId])
    VALUES (49, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 1, 1, 50, 51, CAST(1 AS smallint), 9);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'IsActive', N'IsFirstStep', N'NextStepApprovedId', N'NextStepRejectedId', N'WorkflowStatusId', N'WorkflowTypeId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalStep]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalStep] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] ON;
    INSERT INTO [Workflow].[ApprovalRole] ([Id], [ApprovalId], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [RoleId], [WorkflowApprovalStepId])
    VALUES (25, 2, NULL, NULL, '2019-06-04T15:40:44.691-03:00', N'admin', 10, 49);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ApprovalId', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'RoleId', N'WorkflowApprovalStepId') AND [object_id] = OBJECT_ID(N'[Workflow].[ApprovalRole]'))
        SET IDENTITY_INSERT [Workflow].[ApprovalRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalRole_ApprovalId] ON [Workflow].[ApprovalRole] ([ApprovalId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalRole_WorkflowApprovalStepId] ON [Workflow].[ApprovalRole] ([WorkflowApprovalStepId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalStep_NextStepApprovedId] ON [Workflow].[ApprovalStep] ([NextStepApprovedId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalStep_NextStepRejectedId] ON [Workflow].[ApprovalStep] ([NextStepRejectedId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalStep_WorkflowStatusId] ON [Workflow].[ApprovalStep] ([WorkflowStatusId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalStep_WorkflowTypeId] ON [Workflow].[ApprovalStep] ([WorkflowTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalStepResult_ApprovalId] ON [Workflow].[ApprovalStepResult] ([ApprovalId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_ApprovalStepResult_WorkflowStepResultId] ON [Workflow].[ApprovalStepResult] ([WorkflowStepResultId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_StepResult_NextStepApprovedId] ON [Workflow].[StepResult] ([NextStepApprovedId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_StepResult_NextStepRejectedId] ON [Workflow].[StepResult] ([NextStepRejectedId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_StepResult_StepOriginId] ON [Workflow].[StepResult] ([StepOriginId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_StepResult_WorkflowId] ON [Workflow].[StepResult] ([WorkflowId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_StepResult_WorkflowStatusId] ON [Workflow].[StepResult] ([WorkflowStatusId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_Workflow_CurrentStepId] ON [Workflow].[Workflow] ([CurrentStepId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    CREATE INDEX [IX_Workflow_WorkflowTypeId] ON [Workflow].[Workflow] ([WorkflowTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    ALTER TABLE [Workflow].[ApprovalStepResult] ADD CONSTRAINT [FK_ApprovalStepResult_StepResult_WorkflowStepResultId] FOREIGN KEY ([WorkflowStepResultId]) REFERENCES [Workflow].[StepResult] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    ALTER TABLE [Workflow].[StepResult] ADD CONSTRAINT [FK_StepResult_Workflow_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow].[Workflow] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202846_0.0.0.1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190603202846_0.0.0.1', N'2.2.4-servicing-10062');
END;

GO

