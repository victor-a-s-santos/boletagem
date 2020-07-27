IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724200647_1.0.0.8.zbank')
BEGIN
    CREATE TABLE [Ticket].[Bank] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(10) NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [ShortName] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Bank] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724200647_1.0.0.8.zbank')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'ShortName') AND [object_id] = OBJECT_ID(N'[Ticket].[Bank]'))
        SET IDENTITY_INSERT [Ticket].[Bank] ON;
    INSERT INTO [Ticket].[Bank] ([Id], [Code], [Name], [ShortName])
    VALUES (1, N'001', N'Banco do Brasil S.A.', N'BCO DO BRASIL S.A.'),
    (73, N'604', N'Banco Industrial do Brasil S.A.', N'BCO INDUSTRIAL DO BRASIL S.A.'),
    (72, N'600', N'Banco Luso Brasileiro S.A.', N'BCO LUSO BRASILEIRO S.A.'),
    (71, N'505', N'Banco Credit Suisse (Brasil) S.A.', N'BCO CREDIT SUISSE S.A.'),
    (70, N'487', N'Deutsche Bank S.A. - Banco Alemão', N'DEUTSCHE BANK S.A.BCO ALEMAO'),
    (69, N'477', N'Citibank N.A.', N'CITIBANK N.A.'),
    (68, N'473', N'Banco Caixa Geral - Brasil S.A.', N'BCO CAIXA GERAL BRASIL S.A.'),
    (67, N'464', N'Banco Sumitomo Mitsui Brasileiro S.A.', N'BCO SUMITOMO MITSUI BRASIL S.A.'),
    (66, N'456', N'Banco MUFG Brasil S.A.', N'BCO MUFG BRASIL S.A.'),
    (65, N'422', N'Banco Safra S.A.', N'BCO SAFRA S.A.'),
    (64, N'412', N'Banco Capital S. A.', N'BCO CAPITAL S.A.'),
    (63, N'394', N'Banco Bradesco Financiamentos S.A.', N'BCO BRADESCO FINANC. S.A.'),
    (62, N'389', N'Banco Mercantil do Brasil S.A.', N'BCO MERCANTIL DO BRASIL S.A.'),
    (61, N'376', N'Banco J. P. Morgan S. A.', N'BCO J.P. MORGAN S.A.'),
    (60, N'366', N'Banco Société Générale Brasil S.A.', N'BCO SOCIETE GENERALE BRASIL'),
    (59, N'341', N'Itaú Unibanco  S.A.', N'ITAÚ UNIBANCO S.A.'),
    (58, N'320', N'China Construction Bank (Brasil) Banco Múltiplo S/A', N'BCO CCB BRASIL S.A.'),
    (57, N'318', N'Banco BMG S.A.', N'BCO BMG S.A.'),
    (56, N'300', N'Banco de la Nacion Argentina', N'BCO LA NACION ARGENTINA'),
    (55, N'279', N'Cooperativa de Crédito Rural de Primavera do Leste', N'CCR DE PRIMAVERA DO LESTE'),
    (54, N'266', N'Banco Cédula S.A.', N'BCO CEDULA S.A.'),
    (53, N'265', N'Banco Fator S.A.', N'BCO FATOR S.A.'),
    (74, N'610', N'Banco VR S.A.', N'BCO VR S.A.'),
    (52, N'254', N'Parana Banco S. A.', N'PARANA BCO S.A.'),
    (75, N'611', N'Banco Paulista S.A.', N'BCO PAULISTA S.A.'),
    (77, N'613', N'Omni Banco S.A.', N'OMNI BANCO S.A.'),
    (98, N'755', N'Bank of America Merrill Lynch Banco Múltiplo S.A.', N'BOFA MERRILL LYNCH BM S.A.'),
    (97, N'753', N'Novo Banco Continental S.A. - Banco Múltiplo', N'NOVO BCO CONTINENTAL S.A. - BM'),
    (96, N'752', N'Banco BNP Paribas Brasil S.A.', N'BCO BNP PARIBAS BRASIL S A'),
    (95, N'748', N'Banco Cooperativo Sicredi S. A.', N'BCO COOPERATIVO SICREDI S.A.'),
    (94, N'747', N'Banco Rabobank International Brasil S.A.', N'BCO RABOBANK INTL BRASIL S.A.'),
    (93, N'746', N'Banco Modal S.A.', N'BCO MODAL S.A.'),
    (92, N'745', N'Banco Citibank S.A.', N'BCO CITIBANK S.A.'),
    (91, N'743', N'Banco Semear S.A.', N'BANCO SEMEAR'),
    (90, N'741', N'Banco Ribeirão Preto S.A.', N'BCO RIBEIRAO PRETO S.A.'),
    (89, N'739', N'Banco Cetelem S.A.', N'BCO CETELEM S.A.'),
    (88, N'707', N'Banco Daycoval S.A.', N'BCO DAYCOVAL S.A'),
    (87, N'655', N'Banco Votorantim S.A.', N'BCO VOTORANTIM S.A.'),
    (86, N'654', N'Banco A. J. Renner S.A.', N'BCO A.J. RENNER S.A.'),
    (85, N'653', N'Banco Indusval S. A.', N'BCO INDUSVAL S.A.'),
    (84, N'652', N'Itaú Unibanco Holding S.A.', N'ITAÚ UNIBANCO HOLDING S.A.'),
    (83, N'643', N'Banco Pine S.A.', N'BCO PINE S.A.'),
    (82, N'637', N'Banco Sofisa S. A.', N'BCO SOFISA S.A.'),
    (81, N'634', N'Banco Triângulo S.A.', N'BCO TRIANGULO S.A.'),
    (80, N'633', N'Banco Rendimento S.A.', N'BCO RENDIMENTO S.A.'),
    (79, N'626', N'Banco Ficsa S. A.', N'BCO FICSA S.A.'),
    (78, N'623', N'Banco Pan S.A.', N'BANCO PAN'),
    (76, N'612', N'Banco Guanabara S.A.', N'BCO GUANABARA S.A.'),
    (51, N'249', N'Banco Investcred Unibanco S.A.', N'BANCO INVESTCRED UNIBANCO S.A.'),
    (50, N'246', N'Banco ABC Brasil S.A.', N'BCO ABC BRASIL S.A.'),
    (49, N'243', N'Banco Máxima S.A.', N'BCO MÁXIMA S.A.'),
    (22, N'084', N'Uniprime Norte do Paraná - Cooperativa de Crédito Ltda.', N'UNIPRIME NORTE DO PARANÁ - CC'),
    (21, N'083', N'Banco da China Brasil S.A.', N'BCO DA CHINA BRASIL S.A.'),
    (20, N'077', N'Banco Inter S.A.', N'BANCO INTER'),
    (19, N'074', N'Banco J. Safra S.A.', N'BCO. J.SAFRA S.A.'),
    (18, N'070', N'Banco de Brasília S.A.', N'BRB - BCO DE BRASILIA S.A.'),
    (17, N'069', N'Banco Crefisa S.A.', N'BCO CREFISA S.A.'),
    (16, N'065', N'Banco AndBank (Brasil) S.A.', N'BCO ANDBANK S.A.'),
    (15, N'063', N'Banco Bradescard S.A.', N'BANCO BRADESCARD'),
    (14, N'062', N'Hipercard Banco Múltiplo S.A.', N'HIPERCARD BM S.A.'),
    (13, N'047', N'Banco do Estado de Sergipe S.A.', N'BCO DO EST. DE SE S.A.'),
    (12, N'041', N'Banco do Estado do Rio Grande do Sul S.A.', N'BCO DO ESTADO DO RS S.A.'),
    (11, N'037', N'Banco do Estado do Pará S.A.', N'BCO DO EST. DO PA S.A.'),
    (10, N'036', N'Banco Bradesco BBI S.A.', N'BCO BBI S.A.'),
    (9, N'033', N'Banco Santander (Brasil) S. A.', N'BCO SANTANDER (BRASIL) S.A.'),
    (8, N'025', N'Banco Alfa S.A.', N'BCO ALFA S.A.'),
    (7, N'021', N'Banestes S.A. Banco do Estado do Espírito Santo', N'BCO BANESTES S.A.'),
    (6, N'018', N'Banco Tricury S.A.', N'BCO TRICURY S.A.'),
    (5, N'017', N'BNY Mellon Banco S.A.', N'BNY MELLON BCO S.A.'),
    (4, N'010', N'Credicoamo Crédito Rural Cooperativa', N'CREDICOAMO'),
    (3, N'004', N'Banco do Nordeste do Brasil S.A.', N'BCO DO NORDESTE DO BRASIL S.A.'),
    (2, N'003', N'Banco da Amazônia S.A.', N'BCO DA AMAZONIA S.A.'),
    (23, N'085', N'Cooperativa Central de Crédito - Ailos', N'COOP CENTRAL AILOS'),
    (24, N'089', N'Cooperativa de Crédito Rural da Região da Mogiana', N'CCR REG MOGIANA'),
    (25, N'094', N'Banco Finaxis S.A.', N'BANCO FINAXIS'),
    (26, N'097', N'Cooperativa Central de Crédito Noroeste Brasileiro Ltda - CentralCredi', N'CCC NOROESTE BRASILEIRO LTDA.'),
    (48, N'241', N'Banco Clássico S.A.', N'BCO CLASSICO S.A.'),
    (47, N'237', N'Banco Bradesco S.A.', N'BCO BRADESCO S.A.'),
    (46, N'224', N'Banco Fibra S.A.', N'BCO FIBRA S.A.'),
    (45, N'222', N'Banco Credit Agrícole Brasil S.A.', N'BCO CRÉDIT AGRICOLE BR S.A.'),
    (44, N'218', N'Banco BS2 S.A.', N'BCO BS2 S.A.'),
    (43, N'217', N'Banco John Deere S.A.', N'BANCO JOHN DEERE S.A.'),
    (42, N'213', N'Banco Arbi S.A.', N'BCO ARBI S.A.'),
    (41, N'212', N'Banco Original S.A.', N'BANCO ORIGINAL'),
    (40, N'208', N'Banco BTG Pactual S.A.', N'BANCO BTG PACTUAL S.A.'),
    (39, N'204', N'Banco Bradesco Cartões S.A.', N'BCO BRADESCO CARTOES S.A.'),
    (99, N'756', N'Banco Cooperativo do Brasil S/A - Bancoob', N'BANCOOB'),
    (38, N'136', N'Confederação Nacional das Cooperativas Centrais Unicred Ltda – Unicred do Brasil', N'CONF NAC COOP CENTRAIS UNICRED'),
    (36, N'125', N'Brasil Plural S.A. Banco Múltiplo', N'BRASIL PLURAL S.A. BCO.'),
    (35, N'124', N'Banco Woori Bank do Brasil S.A.', N'BCO WOORI BANK DO BRASIL S.A.'),
    (34, N'122', N'Banco Bradesco BERJ S.A.', N'BCO BRADESCO BERJ S.A.'),
    (33, N'121', N'Banco Agibank S.A.', N'BCO AGIBANK S.A.'),
    (32, N'120', N'Banco Rodobens SA', N'BCO RODOBENS S.A.'),
    (31, N'114', N'Central Cooperativa de Crédito no Estado do Espírito Santo - CECOOP', N'CENTRAL COOPERATIVA DE CRÉDITO NO ESTADO DO ESPÍRITO SANTO'),
    (30, N'107', N'Banco Bocom BBM S.A.', N'BCO BOCOM BBM S.A.'),
    (29, N'104', N'Caixa Econômica Federal', N'CAIXA ECONOMICA FEDERAL'),
    (28, N'099', N'Uniprime Central – Central Interestadual de Cooperativas de Crédito Ltda.', N'UNIPRIME CENTRAL CCC LTDA.'),
    (27, N'098', N'Credialiança Cooperativa de Crédito Rural', N'CREDIALIANÇA CCR'),
    (37, N'133', N'Confederação Nacional das Cooperativas Centrais de Crédito e Economia Familiar e', N'CRESOL CONFEDERAÇÃO'),
    (100, N'757', N'Banco Keb Hana do Brasil S. A.', N'BCO KEB HANA DO BRASIL S.A.');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'ShortName') AND [object_id] = OBJECT_ID(N'[Ticket].[Bank]'))
        SET IDENTITY_INSERT [Ticket].[Bank] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190724200647_1.0.0.8.zbank')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190724200647_1.0.0.8.zbank', N'2.2.4-servicing-10062');
END;

GO

