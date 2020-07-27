IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190715142856_1.0.0.6.signoff')
BEGIN
    CREATE TABLE [Ticket].[TicketSignoff] (
        [Id] int NOT NULL IDENTITY,
        [TicketTypeId] int NOT NULL,
        [Type] nvarchar(60) NOT NULL,
        [TimeLimit] time NOT NULL,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        CONSTRAINT [PK_TicketSignoff] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190715142856_1.0.0.6.signoff')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'TicketTypeId', N'Type', N'TimeLimit', N'CreationDate') AND [object_id] = OBJECT_ID(N'[Ticket].[TicketSignoff]'))
        SET IDENTITY_INSERT [Ticket].[TicketSignoff] ON;
    INSERT INTO [Ticket].[TicketSignoff] ([Id], [TicketTypeId], [Type], [TimeLimit], [CreationDate])
    VALUES (1, 1, N'Cotas - CETIP', '16:00:00', '2019-07-17T18:11:35.948-03:00'),
    (2, 1, N'Cotas - TED', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (3, 2, N'RF - Título Privado', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (4, 3, N'RF - Título Público', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (5, 4, N'Compromissada de Compra', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (6, 5, N'Futuros', '17:00:00', '2019-07-17T18:11:35.952-03:00'),
    (7, 6, N'SWAP - CETIP', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (8, 7, N'Margem (Depósito\Vinculação) Título Público', '11:30:00', '2019-07-17T18:11:35.952-03:00'),
    (9, 7, N'Margem (Depósito\Vinculação) Título Privado', '11:30:00', '2019-07-17T18:11:35.952-03:00'),
    (10, 7, N'Margem (Depósito\Vinculação) Renda Variável', '11:30:00', '2019-07-17T18:11:35.952-03:00'),
    (11, 7, N'Margem (Retirada\Desvinculação) Título Público', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (12, 7, N'Margem (Retirada\Desvinculação) Título Privado', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (13, 7, N'Margem (Retirada\Desvinculação) Renda Variável', '17:00:00', '2019-07-17T18:11:35.952-03:00'),
    (14, 7, N'Margem Dinheiro', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (15, 8, N'Termo de Moeda', '16:00:00', '2019-07-17T18:11:35.952-03:00'),
    (16, 9, N'Renda Variável', '17:00:00', '2019-07-17T18:11:35.952-03:00');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'TicketTypeId', N'Type', N'TimeLimit', N'CreationDate') AND [object_id] = OBJECT_ID(N'[Ticket].[TicketSignoff]'))
        SET IDENTITY_INSERT [Ticket].[TicketSignoff] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190715142856_1.0.0.6.signoff')
BEGIN
    CREATE TABLE [Ticket].[TicketSignoffLog] (
        [Id] int NOT NULL IDENTITY,
        [TicketSignoffId] int NOT NULL,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [TimeLimit] time NOT NULL,
        CONSTRAINT [PK_TicketSignoffLog] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TicketSignoffLog_TicketSignoff_TicketSignoffId] FOREIGN KEY ([TicketSignoffId]) REFERENCES [Ticket].[TicketSignoff] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190715142856_1.0.0.6.signoff')
BEGIN
    CREATE TABLE [Ticket].[TicketSignoffRequest] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [TicketSignoffId] int NOT NULL,
        [TimeLimit] time NOT NULL,
        [Justificative] nvarchar(280) NOT NULL,
        CONSTRAINT [PK_TicketSignoffRequest] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TicketSignoffRequest_TicketSignoff_TicketSignoffId] FOREIGN KEY ([TicketSignoffId]) REFERENCES [Ticket].[TicketSignoff] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190715142856_1.0.0.6.signoff')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ticket].[TicketVariableIncome]') AND [c].[name] = N'OperationDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Ticket].[TicketVariableIncome] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Ticket].[TicketVariableIncome] DROP COLUMN [OperationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190715142856_1.0.0.6.signoff')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190715142856_1.0.0.6.signoff', N'2.2.4-servicing-10062');
END;

GO

