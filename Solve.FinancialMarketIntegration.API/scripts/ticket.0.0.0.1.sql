IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF SCHEMA_ID(N'Ticket') IS NULL EXEC(N'CREATE SCHEMA [Ticket];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[AccountManager] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Document] nvarchar(max) NULL,
        [SacCode] nvarchar(max) NULL,
        CONSTRAINT [PK_AccountManager] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[Fund] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [FundName] varchar(80) NOT NULL,
        [FundDocument] varchar(14) NOT NULL,
        [FundCode] varchar(50) NULL,
        [FundCetipAccount] varchar(8) NOT NULL,
        [IsFIDC] bit NOT NULL,
        [FundClass] varchar(50) NOT NULL,
        [FundCodeIssuer] varchar(10) NOT NULL,
        [IssueDate] datetimeoffset NOT NULL,
        [IsNewIssue] bit NOT NULL,
        [FundType] char(3) NOT NULL,
        CONSTRAINT [PK_Fund] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[MarketType] (
        [Id] smallint NOT NULL IDENTITY,
        [Name] varchar(20) NULL,
        CONSTRAINT [PK_MarketType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[OperationType] (
        [Id] smallint NOT NULL IDENTITY,
        [Name] varchar(20) NULL,
        CONSTRAINT [PK_OperationType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[SettlementType] (
        [Id] smallint NOT NULL IDENTITY,
        [Name] varchar(20) NULL,
        CONSTRAINT [PK_SettlementType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[Type] (
        [Id] smallint NOT NULL IDENTITY,
        [Name] varchar(20) NULL,
        CONSTRAINT [PK_Type] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[UserAccountManager] (
        [UserIdentifier] nvarchar(450) NOT NULL,
        [AccountManagerId] int NOT NULL,
        [IsMaster] bit NOT NULL,
        [Id] int NOT NULL,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        CONSTRAINT [PK_UserAccountManager] PRIMARY KEY ([AccountManagerId], [UserIdentifier], [IsMaster]),
        CONSTRAINT [FK_UserAccountManager_AccountManager_AccountManagerId] FOREIGN KEY ([AccountManagerId]) REFERENCES [Ticket].[AccountManager] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[AccountManagerFund] (
        [AccountManagerId] int NOT NULL,
        [FundId] int NOT NULL,
        CONSTRAINT [PK_AccountManagerFund] PRIMARY KEY ([AccountManagerId], [FundId]),
        CONSTRAINT [FK_AccountManagerFund_AccountManager_AccountManagerId] FOREIGN KEY ([AccountManagerId]) REFERENCES [Ticket].[AccountManager] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AccountManagerFund_Fund_FundId] FOREIGN KEY ([FundId]) REFERENCES [Ticket].[Fund] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[Ticket] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetimeoffset NOT NULL,
        [CreationUser] nvarchar(max) NULL,
        [ChangeDate] datetimeoffset NULL,
        [ChangeUser] nvarchar(max) NULL,
        [TypeId] smallint NOT NULL,
        [PortfolioName] varchar(90) NOT NULL,
        [PortfolioCode] varchar(21) NULL,
        [PortfolioDocument] varchar(14) NOT NULL,
        [PortfolioAccount] varchar(10) NOT NULL,
        [PortfolioClearingAccount] varchar(15) NULL,
        [AccountManagerId] int NOT NULL,
        [OperationDate] datetimeoffset NOT NULL,
        [Annotations] varchar(200) NULL,
        [WorkflowId] int NULL,
        [WorkflowStartDate] datetimeoffset NULL,
        CONSTRAINT [PK_Ticket] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Ticket_AccountManager_AccountManagerId] FOREIGN KEY ([AccountManagerId]) REFERENCES [Ticket].[AccountManager] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Ticket_Type_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [Ticket].[Type] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketContracted] (
        [TicketId] int NOT NULL,
        [OperationTypeId] smallint NOT NULL,
        [OperationValue] decimal(18,8) NOT NULL,
        [Amount] int NULL,
        [UnitPriceOutward] decimal(18,8) NOT NULL,
        [UnitPriceReturn] decimal(18,8) NOT NULL,
        [ReturnDate] datetimeoffset NULL,
        [ValueOutward] decimal(18,8) NOT NULL,
        [ValueReturn] decimal(18,8) NOT NULL,
        [Security] varchar(80) NOT NULL,
        [SecurityId] varchar(50) NOT NULL,
        [ExpirationDate] datetimeoffset NULL,
        [IssueTax] decimal(12,8) NOT NULL,
        [CounterpartName] varchar(90) NOT NULL,
        [CounterpartDocument] varchar(14) NOT NULL,
        [CounterpartClearingAccount] varchar(15) NOT NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        CONSTRAINT [PK_TicketContracted] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketContracted_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketContracted_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketCurrencyTerm] (
        [TicketId] int NOT NULL,
        [OperationValue] decimal(14,2) NOT NULL,
        [OperationTypeId] smallint NULL,
        [ExpirationDate] datetimeoffset NOT NULL,
        [CetipSettlement] bit NOT NULL,
        [ContractNumber] nvarchar(max) NULL,
        [FutureFee] decimal(12,8) NOT NULL,
        [QuoteExpirationTypeId] int NOT NULL,
        [CurrencyId] smallint NOT NULL,
        [CrossRate] bit NOT NULL,
        [CounterpartName] varchar(90) NULL,
        [CounterpartDocument] varchar(14) NULL,
        [CounterpartClearingAccount] varchar(10) NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        CONSTRAINT [PK_TicketCurrencyTerm] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketCurrencyTerm_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TicketCurrencyTerm_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketFundQuotas] (
        [TicketId] int NOT NULL,
        [OperationTypeId] smallint NOT NULL,
        [SettlementTypeId] smallint NOT NULL,
        [FundName] varchar(90) NOT NULL,
        [FundDocument] varchar(14) NOT NULL,
        [IsFIDC] bit NOT NULL,
        [FundClassSeries] varchar(12) NULL,
        [FundIssuerName] varchar(90) NULL,
        [IsNewFund] bit NOT NULL,
        [FundType] char(3) NOT NULL,
        [FundBranch] varchar(10) NULL,
        [FundBank] varchar(20) NULL,
        [FundAcccount] varchar(15) NULL,
        [FundMnemonicCode] varchar(20) NULL,
        [FullRedeem] bit NULL,
        [IsSecondaryMarket] bit NOT NULL,
        [IsIssueUnitPrice] bit NOT NULL,
        [OperationValue] decimal(18,8) NOT NULL,
        [Amount] decimal(18,8) NULL,
        [QuotaValue] decimal(18,8) NULL,
        [HasSameOwnership] bit NULL,
        [IsCetipVoice] bit NULL,
        [CounterpartName] varchar(90) NULL,
        [CounterpartDocument] varchar(14) NULL,
        [CounterpartClearingAccount] varchar(10) NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        CONSTRAINT [PK_TicketFundQuotas] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketFundQuotas_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketFundQuotas_SettlementType_SettlementTypeId] FOREIGN KEY ([SettlementTypeId]) REFERENCES [Ticket].[SettlementType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketFundQuotas_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketFutures] (
        [TicketId] int NOT NULL,
        [OperationTypeId] smallint NOT NULL,
        [Amount] int NOT NULL,
        [UnitPrice] decimal(18,8) NOT NULL,
        [TradingDate] datetimeoffset NOT NULL,
        [PercentageDiscount] decimal(18,2) NOT NULL,
        [PaperCode] nvarchar(max) NOT NULL,
        [PaperSerie] nvarchar(10) NOT NULL,
        [Annotations] varchar(max) NULL,
        [Broker] nvarchar(max) NULL,
        [BrokerCode] nvarchar(max) NOT NULL,
        [BrokerAccount] nvarchar(max) NOT NULL,
        [BrokerDocument] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TicketFutures] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketFutures_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketFutures_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketMargin] (
        [TicketId] int NOT NULL,
        [OperationTypeId] smallint NOT NULL,
        [MarketTypeId] smallint NOT NULL,
        [Amount] decimal(18,8) NULL,
        [UnitPrice] decimal(18,8) NULL,
        [OperationValue] decimal(18,8) NULL,
        [SecurityType] nvarchar(max) NULL,
        [SecurityName] nvarchar(max) NULL,
        [SecurityCode] nvarchar(max) NULL,
        [DueDate] datetimeoffset NULL,
        [PurchaseDate] datetimeoffset NULL,
        [Quotation] decimal(18,8) NULL,
        [Asset] nvarchar(max) NULL,
        [Bank] nvarchar(max) NULL,
        [Branch] nvarchar(max) NULL,
        [Account] nvarchar(max) NULL,
        [CounterpartName] varchar(90) NULL,
        [CounterpartDocument] varchar(14) NULL,
        [CounterpartClearingAccount] varchar(10) NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        [CounterpartBrokerAccount] varchar(10) NULL,
        CONSTRAINT [PK_TicketMargin] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketMargin_MarketType_MarketTypeId] FOREIGN KEY ([MarketTypeId]) REFERENCES [Ticket].[MarketType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketMargin_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketMargin_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketPrivateFixedIncome] (
        [TicketId] int NOT NULL,
        [OperationTypeId] smallint NOT NULL,
        [Amount] decimal(18,8) NOT NULL,
        [UnitPrice] decimal(18,8) NOT NULL,
        [IsSecondaryMarket] bit NOT NULL,
        [IsTerm] bit NOT NULL,
        [AcquisitionDate] datetimeoffset NULL,
        [OperationValue] decimal(18,2) NOT NULL,
        [AssetType] nvarchar(6) NOT NULL,
        [AssetCode] nvarchar(15) NULL,
        [ExpirationDate] datetimeoffset NOT NULL,
        [IssueFee] decimal(9,6) NOT NULL,
        [IssueDate] datetimeoffset NOT NULL,
        [CounterpartName] varchar(90) NULL,
        [CounterpartDocument] varchar(14) NULL,
        [CounterpartClearingAccount] varchar(10) NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        [Annotations] nvarchar(max) NULL,
        [ObjectAction] nvarchar(max) NULL,
        CONSTRAINT [PK_TicketPrivateFixedIncome] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketPrivateFixedIncome_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketPrivateFixedIncome_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketPublicFixedIncome] (
        [TicketId] int NOT NULL,
        [SettlementTypeId] smallint NULL,
        [OperationTypeId] smallint NOT NULL,
        [Amount] int NULL,
        [UnitPrice] decimal(18,8) NOT NULL,
        [SettlementDate] datetimeoffset NULL,
        [AcquisitionDate] datetimeoffset NULL,
        [OperationValue] decimal(18,8) NOT NULL,
        [Security] varchar(80) NOT NULL,
        [SecurityId] varchar(50) NOT NULL,
        [ExpirationDate] datetimeoffset NULL,
        [IssueTax] decimal(18,8) NULL,
        [IssueDate] datetimeoffset NULL,
        [CounterpartName] varchar(90) NOT NULL,
        [CounterpartDocument] varchar(14) NOT NULL,
        [CounterpartClearingAccount] varchar(10) NOT NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        CONSTRAINT [PK_TicketPublicFixedIncome] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketPublicFixedIncome_OperationType_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [Ticket].[OperationType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketPublicFixedIncome_SettlementType_SettlementTypeId] FOREIGN KEY ([SettlementTypeId]) REFERENCES [Ticket].[SettlementType] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TicketPublicFixedIncome_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketSwapCetip] (
        [TicketId] int NOT NULL,
        [OperationValue] decimal(18,8) NOT NULL,
        [ExpirationDate] datetimeoffset NOT NULL,
        [Command] nvarchar(max) NULL,
        [ActiveIndex] nvarchar(max) NULL,
        [ActiveIndexPercent] decimal(18,8) NOT NULL,
        [ActiveIndexTax] decimal(18,8) NOT NULL,
        [ActiveIndexBase] decimal(18,2) NOT NULL,
        [ActiveInterestType] decimal(18,2) NOT NULL,
        [PassiveIndex] nvarchar(max) NULL,
        [PassiveIndexPercent] decimal(18,8) NOT NULL,
        [PassiveIndexTax] decimal(18,8) NOT NULL,
        [PassiveIndexBase] decimal(18,2) NOT NULL,
        [PassiveInterestType] decimal(18,2) NOT NULL,
        [Annotations] nvarchar(max) NULL,
        [CounterpartName] varchar(80) NOT NULL,
        [CounterpartDocument] varchar(14) NOT NULL,
        [CounterpartClearingAccount] varchar(15) NOT NULL,
        [CounterpartCommand] varchar(10) NULL DEFAULT N'0',
        CONSTRAINT [PK_TicketSwapCetip] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketSwapCetip_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketVariableIncome] (
        [TicketId] int NOT NULL,
        [BrokerCode] nvarchar(max) NULL,
        [OperationDate] datetimeoffset NULL,
        [StockExchangeDate] datetimeoffset NULL,
        [ClientCode] nvarchar(max) NULL,
        [ClientCodeDigit] nvarchar(max) NULL,
        [SellTotal] decimal(18,8) NOT NULL,
        [BuyTotal] decimal(18,8) NOT NULL,
        CONSTRAINT [PK_TicketVariableIncome] PRIMARY KEY ([TicketId]),
        CONSTRAINT [FK_TicketVariableIncome_Ticket_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[Ticket] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE TABLE [Ticket].[TicketVariableIncomeItems] (
        [Id] int NOT NULL IDENTITY,
        [TicketId] int NOT NULL,
        [TradingCode] nvarchar(max) NULL,
        [TradingCodeBusinessCode] nvarchar(max) NULL,
        [BuyOrSell] nvarchar(max) NULL,
        [Amount] decimal(18,8) NOT NULL,
        [SettlementTypeOfSecondaryTerm] nvarchar(max) NULL,
        [Price] decimal(18,8) NOT NULL,
        [Factor] int NOT NULL,
        [SettlementType] nvarchar(max) NULL,
        [AssetCode] nvarchar(max) NULL,
        [ISINCode] nvarchar(max) NULL,
        [ISINCodeDistribution] nvarchar(max) NULL,
        [CompanyName] nvarchar(max) NULL,
        [Specification] nvarchar(max) NULL,
        [SpecificationIndicator] nvarchar(max) NULL,
        [IsAfterMarket] nvarchar(max) NULL,
        [MarketType] nvarchar(max) NULL,
        CONSTRAINT [PK_TicketVariableIncomeItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TicketVariableIncomeItems_TicketVariableIncome_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Ticket].[TicketVariableIncome] ([TicketId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Document', N'Name', N'SacCode') AND [object_id] = OBJECT_ID(N'[Ticket].[AccountManager]'))
        SET IDENTITY_INSERT [Ticket].[AccountManager] ON;
    INSERT INTO [Ticket].[AccountManager] ([Id], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Document], [Name], [SacCode])
    VALUES (1, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'16.804.280/0001-20', N'KRON GESTÃO DE INVESTIMENTOS', N''),
    (27, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'28.529.868/0001-21', N'WNT GESTORA DE RECURSOS LTDA.', N''),
    (28, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'05.563.299/0001-06', N'SOMMA INVESTIMENTOS S.A', N''),
    (29, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'09.442.277/0001-49', N'AUSTRO CAPITAL', N'AUSTROGE'),
    (30, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'12.263.316/0001-55', N'NOVA MILANO INVESTIMENTOS LTDA', N''),
    (31, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'28.925.400/0001-27', N'TRIGONO CAPITAL', N'TRIGONOC'),
    (32, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'08.926.786/0001-84', N'DETOMASO ADMINISTRADORA DE RECURSOS LTDA', N'DETOMASO'),
    (33, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'12.678.380/0001-05', N'VERITAS - VCM Gestão de Capital - (Log Found)', N'NINKA FI'),
    (34, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'24.503.059/0001-60', N'CIX CAPITAL', N''),
    (35, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'25.098.663/0001-11', N'KP GESTÃO DE RECURSOS (demais fundos)', N'KP GESTA'),
    (36, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'07.559.989/0001-17', N'VALORA GESTÃO DE INVESTIMENTOS', N'VALORA'),
    (38, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'27.390.441/0001-01', N'HOA ASET MANAGEMENT GESTÃO DE RECURSOS', N''),
    (39, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'27.913.835/0001-99', N'AVENTIS GESTÃO DE RECURSOS LTDA ME (TRIDAFIN)', N'AVENTIS'),
    (40, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'29.263.481/0001-00', N'EVEREST TRUST GESTORA DE RECURSOS LTDA.', N'EVEREST'),
    (41, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'10.924.308/0001-87', N'IGUANA INVESTIMENTOS LTDA.', N'IGUANA'),
    (42, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'27.451.028/0001-00', N'PRISMA CAPITAL LTDA', N'PRISMA'),
    (43, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'16.789.525/0001-98', N'XP VISTA ASSET MANAGMENT LTDA.', N'XP VISTA'),
    (44, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'29.063.944/0001-90', N'3J GESTORA DE RECURSOS LTDA.', N''),
    (45, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'27.690.986/0001-25', N'ARC (antiga ARKON INVESTIMENTOS LTDA.)', N'ARC CAPI'),
    (46, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'16.707.841/0001-73', N'TYR GESTÃO DE RECURSOS LTDA.', N''),
    (47, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'12.620.044/0001-01', N'ALPS CAPITAL GESTÃO E INVESTIMENTOS LTDA.', N'ALPS CAP'),
    (48, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'20.043.909/0001-34', N'FARM INVESTIMENTOS GESTÃO DE RECURSOS LTDA', N''),
    (26, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'21.551.986.0001-68', N'PARAGUAÇU INVESTIMENTOS EIRELLI', N''),
    (25, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'15.032.609/0001-10', N'STARBOARD ASSET LTDA', N'STARBOAR'),
    (37, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'16.492.426/0001-40', N'VALER INVESTIMENTOS (BRAZCORE)', N'VALER'),
    (23, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'07.913.960/0001-91', N'ITAJUI GESTÃO DE INVESTIMENTOS LTDA', N'ITAJUI'),
    (2, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'05.043.746/0001-04', N'ZERO CONFLICT WEALTH MANAGEMENT LTDA', N'ZERO CON'),
    (3, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'07.252.227/0001-73', N'GRAU GESTÃO', N'GRAU GES'),
    (4, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'10.757.908/0001-06', N'BERKANA INVESTIMENTOS E GESTÃO DE RECURSOS LTDA', N'BERKANA'),
    (5, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'20.495.002/0001-06', N'R2C', N'R2C'),
    (6, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'25.098.663/0001-11', N'KP GESTÃO DE RECURSOS', N'KP GESTA'),
    (7, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'24.515.907/0001-51', N'ATRIO GESTORA DE ATIVOS LTDA. (antiga Sul Brasil Gestora)', N'ATRIO'),
    (8, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'09.311.153/0001-24', N'RIO DAS PEDRAS ADMINISTRAÇÃO E PARTICIPAÇÕES LTDA', N'RIODASPE'),
    (9, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'06.576.569/0001-86', N'INTEGRAL INVESTIMENTOS LTDA', N''),
    (10, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'11.916.849/0001-26', N'OURO PRETO GESTÃO DE RECURSOS S.A.', N'OURO'),
    (11, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'04.661.817/0001-61', N'KINEA PRIVATE VariableIncome INVESTIMENTOS S/A', N'KINEAPEQ'),
    (12, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'09.121.454/0001-95', N'TERCON INVESTIMENTOS LTDA', N'J&M INV'),
    (13, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'17.480.662/0001-09', N'FACT INVESTMENTS GESTAO DE RECURSOS LTDA', N'FACT INV'),
    (14, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'09.456.933/0001-62', N'QUATÁ GESTÃO DE RECURSOS LTDA', N'QUATA'),
    (15, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'09.446.129/0001-00', N'G5 ADMINISTRADORA DE RECURSOS LTDA.', N'G5'),
    (16, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'05.451.668/0001-79', N'POLO CAPITAL GESTÃO DE RECURSOS LTDA', N'POLO CAP'),
    (17, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'16.492.426/0001-40', N'VALER INVESTIMENTOS (BRAZCORE)', N'VALER'),
    (18, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'08.592.877/0001-20', N'DEL MONTE - GESTÃO DE INVESTIMENTOS LTDA', N''),
    (19, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'04.608.141/0001-42', N'SFI INVESTIMENTOS LTDA', N'SFI INV'),
    (20, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'08.639.165/0001-10', N'ABC CAPITAL - GESTAO DE INVESTIMENTOS LTDA.', N''),
    (21, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'19.207.159/0001-00', N'PRIVATTO INVESTIMENTOS', N''),
    (22, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'16.925.467/0001-82', N'GLOBAL GESTÃO E INVESTIMENTOS LTDA', N'GLOBAL'),
    (24, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, N'12.461.742/0001-01', N'LATACHE GESTÃO DE RECURSOS LTDA', N'LATACHE');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Document', N'Name', N'SacCode') AND [object_id] = OBJECT_ID(N'[Ticket].[AccountManager]'))
        SET IDENTITY_INSERT [Ticket].[AccountManager] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[MarketType]'))
        SET IDENTITY_INSERT [Ticket].[MarketType] ON;
    INSERT INTO [Ticket].[MarketType] ([Id], [Name])
    VALUES (CAST(3 AS smallint), 'Renda Variável'),
    (CAST(4 AS smallint), 'Dinheiro'),
    (CAST(2 AS smallint), 'RF Título Público'),
    (CAST(1 AS smallint), 'RF Título Privado');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[MarketType]'))
        SET IDENTITY_INSERT [Ticket].[MarketType] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[OperationType]'))
        SET IDENTITY_INSERT [Ticket].[OperationType] ON;
    INSERT INTO [Ticket].[OperationType] ([Id], [Name])
    VALUES (CAST(8 AS smallint), 'Moeda Vendida'),
    (CAST(1 AS smallint), 'Compra'),
    (CAST(2 AS smallint), 'Venda'),
    (CAST(3 AS smallint), 'Aplicacao'),
    (CAST(4 AS smallint), 'Resgate'),
    (CAST(5 AS smallint), 'Deposito'),
    (CAST(6 AS smallint), 'Retirada'),
    (CAST(7 AS smallint), 'Moeda Comprada'),
    (CAST(9 AS smallint), 'Transferência');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[OperationType]'))
        SET IDENTITY_INSERT [Ticket].[OperationType] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[SettlementType]'))
        SET IDENTITY_INSERT [Ticket].[SettlementType] ON;
    INSERT INTO [Ticket].[SettlementType] ([Id], [Name])
    VALUES (CAST(4 AS smallint), 'À Vista'),
    (CAST(3 AS smallint), 'Termo'),
    (CAST(2 AS smallint), 'TED'),
    (CAST(1 AS smallint), 'CETIP');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[SettlementType]'))
        SET IDENTITY_INSERT [Ticket].[SettlementType] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[Type]'))
        SET IDENTITY_INSERT [Ticket].[Type] ON;
    INSERT INTO [Ticket].[Type] ([Id], [Name])
    VALUES (CAST(6 AS smallint), 'SWAP CETIP'),
    (CAST(7 AS smallint), 'Margem'),
    (CAST(5 AS smallint), 'Futuros'),
    (CAST(8 AS smallint), 'Termo de Moeda'),
    (CAST(3 AS smallint), 'SELIC'),
    (CAST(2 AS smallint), 'CETIP'),
    (CAST(1 AS smallint), 'Cotas'),
    (CAST(4 AS smallint), 'Compromissada'),
    (CAST(9 AS smallint), 'Renda Variável');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Ticket].[Type]'))
        SET IDENTITY_INSERT [Ticket].[Type] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountManagerId', N'UserIdentifier', N'IsMaster', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Id') AND [object_id] = OBJECT_ID(N'[Ticket].[UserAccountManager]'))
        SET IDENTITY_INSERT [Ticket].[UserAccountManager] ON;
    INSERT INTO [Ticket].[UserAccountManager] ([AccountManagerId], [UserIdentifier], [IsMaster], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Id])
    VALUES (1, N'gestor2', 1, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, 1);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountManagerId', N'UserIdentifier', N'IsMaster', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Id') AND [object_id] = OBJECT_ID(N'[Ticket].[UserAccountManager]'))
        SET IDENTITY_INSERT [Ticket].[UserAccountManager] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountManagerId', N'UserIdentifier', N'IsMaster', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Id') AND [object_id] = OBJECT_ID(N'[Ticket].[UserAccountManager]'))
        SET IDENTITY_INSERT [Ticket].[UserAccountManager] ON;
    INSERT INTO [Ticket].[UserAccountManager] ([AccountManagerId], [UserIdentifier], [IsMaster], [ChangeDate], [ChangeUser], [CreationDate], [CreationUser], [Id])
    VALUES (2, N'gestor', 1, NULL, NULL, '0001-01-01T00:00:00.000+00:00', NULL, 1);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountManagerId', N'UserIdentifier', N'IsMaster', N'ChangeDate', N'ChangeUser', N'CreationDate', N'CreationUser', N'Id') AND [object_id] = OBJECT_ID(N'[Ticket].[UserAccountManager]'))
        SET IDENTITY_INSERT [Ticket].[UserAccountManager] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_AccountManagerFund_FundId] ON [Ticket].[AccountManagerFund] ([FundId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_Ticket_AccountManagerId] ON [Ticket].[Ticket] ([AccountManagerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_Ticket_TypeId] ON [Ticket].[Ticket] ([TypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketContracted_OperationTypeId] ON [Ticket].[TicketContracted] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketCurrencyTerm_OperationTypeId] ON [Ticket].[TicketCurrencyTerm] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketFundQuotas_OperationTypeId] ON [Ticket].[TicketFundQuotas] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketFundQuotas_SettlementTypeId] ON [Ticket].[TicketFundQuotas] ([SettlementTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketFutures_OperationTypeId] ON [Ticket].[TicketFutures] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketMargin_MarketTypeId] ON [Ticket].[TicketMargin] ([MarketTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketMargin_OperationTypeId] ON [Ticket].[TicketMargin] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketPrivateFixedIncome_OperationTypeId] ON [Ticket].[TicketPrivateFixedIncome] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketPublicFixedIncome_OperationTypeId] ON [Ticket].[TicketPublicFixedIncome] ([OperationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketPublicFixedIncome_SettlementTypeId] ON [Ticket].[TicketPublicFixedIncome] ([SettlementTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    CREATE INDEX [IX_TicketVariableIncomeItems_TicketId] ON [Ticket].[TicketVariableIncomeItems] ([TicketId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202819_0.0.0.1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190603202819_0.0.0.1', N'2.2.4-servicing-10062');
END;

GO

