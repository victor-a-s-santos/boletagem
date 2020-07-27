IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703205055_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Role]'))
        SET IDENTITY_INSERT [Security].[Role] ON;
    INSERT INTO [Security].[Role] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Name])
    VALUES (30, '2019-06-04T15:40:50.895-03:00', NULL, '2019-06-04T15:40:50.895-03:00', NULL, N'Alterar horário limite default signoff'),
    (31, '2019-06-04T15:40:50.895-03:00', NULL, '2019-06-04T15:40:50.895-03:00', NULL, N'Estender horário limite signoff');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Role]'))
        SET IDENTITY_INSERT [Security].[Role] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703205055_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Group]'))
        SET IDENTITY_INSERT [Security].[Group] ON;
    INSERT INTO [Security].[Group] ([Id], [CreationDate], [Name])
    VALUES (8, '2019-07-15T19:33:43.232-03:00', N'Signoff');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Name') AND [object_id] = OBJECT_ID(N'[Security].[Group]'))
        SET IDENTITY_INSERT [Security].[Group] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703205055_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'RoleId') AND [object_id] = OBJECT_ID(N'[Security].[GroupRole]'))
        SET IDENTITY_INSERT [Security].[GroupRole] ON;
    INSERT INTO [Security].[GroupRole] ([GroupId], [RoleId])
    VALUES (8, 30),
    (8, 31);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'RoleId') AND [object_id] = OBJECT_ID(N'[Security].[GroupRole]'))
        SET IDENTITY_INSERT [Security].[GroupRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703205055_1.0.0.3')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'RoleId') AND [object_id] = OBJECT_ID(N'[Security].[GroupRole]'))
        SET IDENTITY_INSERT [Security].[GroupRole] ON;
    INSERT INTO [Security].[GroupRole] ([GroupId], [RoleId])
    VALUES (1, 8);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupId', N'RoleId') AND [object_id] = OBJECT_ID(N'[Security].[GroupRole]'))
        SET IDENTITY_INSERT [Security].[GroupRole] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703205055_1.0.0.3')
BEGIN
    DELETE FROM [Security].[GroupRole]
    WHERE [GroupId] = 3 AND [RoleId] = 8;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190703205055_1.0.0.3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190703205055_1.0.0.3', N'2.2.4-servicing-10062');
END;

GO

