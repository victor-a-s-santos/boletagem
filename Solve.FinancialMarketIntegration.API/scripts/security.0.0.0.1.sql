IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF SCHEMA_ID(N'Security') IS NULL EXEC(N'CREATE SCHEMA [Security];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE TABLE [Security].[Group] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_Group] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE TABLE [Security].[Role] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(150) NOT NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE TABLE [Security].[User] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(70) NOT NULL,
        [Active] bit NOT NULL,
        [LastPasswordChangedDate] datetimeoffset NULL,
        [Email] nvarchar(70) NOT NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(15) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [LockoutEndDateUtc] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] smallint NOT NULL,
        [UserName] nvarchar(25) NULL,
        [UserDocument] nvarchar(max) NULL,
        [PasswordExpirationDate] datetimeoffset NOT NULL,
        [Salt] nvarchar(50) NOT NULL,
        [PasswordResetHash] nvarchar(max) NULL,
        [LastAccessToken] nvarchar(250) NULL,
        [LastAccessDate] datetimeoffset NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE TABLE [Security].[GroupRole] (
        [RoleId] int NOT NULL,
        [GroupId] int NOT NULL,
        CONSTRAINT [PK_GroupRole] PRIMARY KEY ([GroupId], [RoleId]),
        CONSTRAINT [FK_GroupRole_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Security].[Group] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GroupRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[Role] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE TABLE [Security].[UserGroup] (
        [UserId] int NOT NULL,
        [GroupId] int NOT NULL,
        CONSTRAINT [PK_UserGroup] PRIMARY KEY ([GroupId], [UserId]),
        CONSTRAINT [FK_UserGroup_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Security].[Group] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserGroup_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[User] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE TABLE [Security].[UserPassword] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserPassword] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserPassword_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[User] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Group]'))
        SET IDENTITY_INSERT [Security].[Group] ON;
    INSERT INTO [Security].[Group] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Name])
    VALUES (1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Gestores'),
    (2, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Adm. Fiduci'),
    (3, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Custodia'),
    (4, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Liquidação (Open)'),
    (5, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Middle Adm. Fiduci'),
    (6, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Administrator do Sistema');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Group]'))
        SET IDENTITY_INSERT [Security].[Group] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Role]'))
        SET IDENTITY_INSERT [Security].[Role] ON;
    INSERT INTO [Security].[Role] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Name])
    VALUES (17, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar histórico de aprovação'),
    (18, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Cotas'),
    (19, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de RF - Título Privado'),
    (20, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de RF - Título Público'),
    (21, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Futuros'),
    (22, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Margem'),
    (25, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Renda Variável'),
    (24, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Termo de Moeda'),
    (27, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Pendente Aprovação do Gestor'),
    (26, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Swap-CETIP'),
    (28, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar Usuário Master'),
    (29, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar Usuário Subordinado'),
    (23, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Visualizar monitor de Compromissada de Compra'),
    (16, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Em Liquidação (Custódia)'),
    (15, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Aguardando Liquidação (Custódia)'),
    (14, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Em Liquidação (Middle ADM Fiduci)'),
    (1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Cotas'),
    (2, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de RF - Título Privado'),
    (3, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de RF - Título Público'),
    (5, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Margem'),
    (6, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Compromissada de Compra'),
    (7, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Termo de Moeda'),
    (4, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Futuros'),
    (9, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Swap-CETIP'),
    (10, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Pendente de Aprovação pela ADM'),
    (11, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Aguardando Liquidação (Open)'),
    (12, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Em Liquidação (Open)'),
    (13, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Aprovar boletas Aguardando Liquidação (Middle ADM Fiduci)'),
    (8, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'Criar boletas de Renda Variável');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Role]'))
        SET IDENTITY_INSERT [Security].[Role] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Active', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Email', N'EmailConfirmed', N'LastAccessDate', N'LastAccessToken', N'LastPasswordChangedDate', N'LockoutEnabled', N'LockoutEndDateUtc', N'Name', N'PasswordExpirationDate', N'PasswordHash', N'PasswordResetHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'Salt', N'UserDocument', N'UserName') AND [object_id] = OBJECT_ID(N'[Security].[User]'))
        SET IDENTITY_INSERT [Security].[User] ON;
    INSERT INTO [Security].[User] ([Id], [AccessFailedCount], [Active], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Email], [EmailConfirmed], [LastAccessDate], [LastAccessToken], [LastPasswordChangedDate], [LockoutEnabled], [LockoutEndDateUtc], [Name], [PasswordExpirationDate], [PasswordHash], [PasswordResetHash], [PhoneNumber], [PhoneNumberConfirmed], [Salt], [UserDocument], [UserName])
    VALUES (6, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'admin@admin.com.br', 1, NULL, NULL, NULL, 0, NULL, N'Administrator', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTF==', NULL, N'admin'),
    (1, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.894-03:00', NULL, '2019-06-04T15:40:50.895-03:00', NULL, N'estelle_ankunding36@hotmail.com', 1, NULL, NULL, NULL, 0, NULL, N'Estelle Ankunding (Gestor)', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTA==', NULL, N'gestor'),
    (2, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'dee_rippin@yahoo.com', 1, NULL, NULL, NULL, 0, NULL, N'Dee Rippin (Adm Fiduci)', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTB==', NULL, N'admin.fiduci'),
    (3, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'garrett_dickinson37@hotmail.com', 1, NULL, NULL, NULL, 0, NULL, N'Garrett Dickinson (Custódia)', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTC==', NULL, N'custodia'),
    (4, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'zula_stokes37@gmail.com', 1, NULL, NULL, NULL, 0, NULL, N'Zula Stokes V (Open)', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTD==', NULL, N'open'),
    (5, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'maia_spinka@gmail.com', 1, NULL, NULL, NULL, 0, NULL, N'Maia Spinka (Middle ADM)', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTE==', NULL, N'middle.adm'),
    (7, CAST(0 AS smallint), 1, '2019-06-04T15:40:50.896-03:00', NULL, '2019-06-04T15:40:50.896-03:00', NULL, N'bart_larson@yahoo.com', 1, NULL, NULL, NULL, 0, NULL, N'Ms. Bart Larson (Gestor)', '2020-06-04T15:40:50.896-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', NULL, NULL, 1, N'IHxey3JXmJa0gWCXqUxvTG==', NULL, N'gestor2');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Active', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Email', N'EmailConfirmed', N'LastAccessDate', N'LastAccessToken', N'LastPasswordChangedDate', N'LockoutEnabled', N'LockoutEndDateUtc', N'Name', N'PasswordExpirationDate', N'PasswordHash', N'PasswordResetHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'Salt', N'UserDocument', N'UserName') AND [object_id] = OBJECT_ID(N'[Security].[User]'))
        SET IDENTITY_INSERT [Security].[User] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'RoleId') AND [object_id] = OBJECT_ID(N'[Security].[GroupRole]'))
        SET IDENTITY_INSERT [Security].[GroupRole] ON;
    INSERT INTO [Security].[GroupRole] ([GroupId], [RoleId])
    VALUES (1, 1),
    (1, 21),
    (2, 21),
    (3, 21),
    (4, 21),
    (1, 22),
    (2, 22),
    (3, 22),
    (4, 22),
    (5, 22),
    (1, 23),
    (2, 23),
    (3, 23),
    (4, 23),
    (5, 23),
    (1, 24),
    (2, 24),
    (3, 24),
    (1, 29),
    (6, 28),
    (5, 26),
    (4, 26),
    (3, 26),
    (2, 26),
    (5, 20),
    (1, 26),
    (4, 25),
    (3, 25),
    (2, 25),
    (1, 25),
    (5, 24),
    (4, 24),
    (5, 25),
    (4, 20),
    (5, 21),
    (2, 20),
    (3, 15),
    (5, 14),
    (5, 13),
    (3, 20),
    (4, 11),
    (2, 10),
    (3, 16),
    (1, 9),
    (1, 7),
    (1, 6),
    (1, 5),
    (1, 4),
    (1, 3),
    (1, 2),
    (3, 8),
    (1, 27),
    (4, 12),
    (2, 17),
    (1, 20),
    (1, 17),
    (4, 19),
    (3, 19),
    (2, 19),
    (1, 19),
    (5, 18),
    (5, 19),
    (3, 18),
    (2, 18),
    (1, 18),
    (5, 17),
    (4, 17),
    (3, 17),
    (4, 18);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'RoleId') AND [object_id] = OBJECT_ID(N'[Security].[GroupRole]'))
        SET IDENTITY_INSERT [Security].[GroupRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'UserId') AND [object_id] = OBJECT_ID(N'[Security].[UserGroup]'))
        SET IDENTITY_INSERT [Security].[UserGroup] ON;
    INSERT INTO [Security].[UserGroup] ([GroupId], [UserId])
    VALUES (1, 7),
    (1, 1),
    (2, 2),
    (3, 3),
    (6, 6),
    (4, 4),
    (5, 5);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'UserId') AND [object_id] = OBJECT_ID(N'[Security].[UserGroup]'))
        SET IDENTITY_INSERT [Security].[UserGroup] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'PasswordHash', N'UserId') AND [object_id] = OBJECT_ID(N'[Security].[UserPassword]'))
        SET IDENTITY_INSERT [Security].[UserPassword] ON;
    INSERT INTO [Security].[UserPassword] ([Id], [CreationDate], [PasswordHash], [UserId])
    VALUES (6, '2019-06-04T15:40:50.8962947-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 6),
    (5, '2019-06-04T15:40:50.8962945-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 5),
    (2, '2019-06-04T15:40:50.8962922-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 2),
    (3, '2019-06-04T15:40:50.8962940-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 3),
    (1, '2019-06-04T15:40:50.8962139-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 1),
    (4, '2019-06-04T15:40:50.8962943-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 4),
    (7, '2019-06-04T15:40:50.8962949-03:00', N'IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=', 7);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'PasswordHash', N'UserId') AND [object_id] = OBJECT_ID(N'[Security].[UserPassword]'))
        SET IDENTITY_INSERT [Security].[UserPassword] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE UNIQUE INDEX [IX_Group_Name] ON [Security].[Group] ([Name]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE INDEX [IX_GroupRole_RoleId] ON [Security].[GroupRole] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE UNIQUE INDEX [IX_Role_Name] ON [Security].[Role] ([Name]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE UNIQUE INDEX [IX_User_UserName] ON [Security].[User] ([UserName]) WHERE [UserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE INDEX [IX_UserGroup_UserId] ON [Security].[UserGroup] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    CREATE INDEX [IX_UserPassword_UserId] ON [Security].[UserPassword] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202828_0.0.0.1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190603202828_0.0.0.1', N'2.2.4-servicing-10062');
END;

GO

