IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    IF SCHEMA_ID(N'Files') IS NULL EXEC(N'CREATE SCHEMA [Files];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    CREATE TABLE [Files].[Files] (
        [Id] int NOT NULL IDENTITY,
        [Name] varchar(200) NULL,
        [IdFileType] smallint NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [Status] smallint NOT NULL,
        CONSTRAINT [PK_Files] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    CREATE TABLE [Files].[FilesPESC] (
        [Id] int NOT NULL IDENTITY,
        [IdFile] int NOT NULL,
        [BrokerCode] nvarchar(max) NULL,
        [DateFile] datetime2 NOT NULL,
        [DateMarket] datetime2 NOT NULL,
        [TradingCode] nvarchar(max) NULL,
        [TradingCodeBusinessCode] nvarchar(max) NULL,
        [BuyOrSell] nvarchar(max) NULL,
        [ClientCode] nvarchar(max) NULL,
        [ClientCodeDigit] nvarchar(max) NULL,
        [Amount] int NOT NULL,
        [PortfolioCode] nvarchar(max) NULL,
        [PortfolioCodeDigit] nvarchar(max) NULL,
        [CustodianUserCode] nvarchar(max) NULL,
        [CustodianClientCode] nvarchar(max) NULL,
        [CustodianClientCodeDigit] nvarchar(max) NULL,
        [SettlementTypeOfSecondaryTerm] nvarchar(max) NULL,
        [MarketType] nvarchar(max) NULL,
        [Price] decimal(18,2) NOT NULL,
        [Factor] int NOT NULL,
        [SettlementType] nvarchar(max) NULL,
        [AssetCode] nvarchar(max) NULL,
        [ISINCode] nvarchar(max) NULL,
        [ISINCodeDistribution] nvarchar(max) NULL,
        [CompanyName] nvarchar(max) NULL,
        [Specification] nvarchar(max) NULL,
        [SpecificationIndicator] nvarchar(max) NULL,
        [IsAfterMarket] nvarchar(max) NULL,
        [StockExchangeCode] nvarchar(max) NULL,
        CONSTRAINT [PK_FilesPESC] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    CREATE TABLE [Files].[Type] (
        [Id] smallint NOT NULL IDENTITY,
        [Name] varchar(20) NULL,
        CONSTRAINT [PK_Type] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Files].[Type]'))
        SET IDENTITY_INSERT [Files].[Type] ON;
    INSERT INTO [Files].[Type] ([Id], [Name])
    VALUES (CAST(1 AS smallint), 'NEGS');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Files].[Type]'))
        SET IDENTITY_INSERT [Files].[Type] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Files].[Type]'))
        SET IDENTITY_INSERT [Files].[Type] ON;
    INSERT INTO [Files].[Type] ([Id], [Name])
    VALUES (CAST(2 AS smallint), 'PESQ');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Files].[Type]'))
        SET IDENTITY_INSERT [Files].[Type] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190603202837_0.0.0.1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190603202837_0.0.0.1', N'2.2.4-servicing-10062');
END;

GO

