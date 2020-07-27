using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess
{
    public class Seed
    {
        public static void Apply(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketType>().HasData(
                new TicketType() { Id = 1, Name = "Cotas" },
                new TicketType() { Id = 2, Name = "CETIP" },
                new TicketType() { Id = 3, Name = "SELIC" },
                new TicketType() { Id = 4, Name = "Compromissada" },
                new TicketType() { Id = 5, Name = "Futuros" },
                new TicketType() { Id = 6, Name = "SWAP CETIP" },
                new TicketType() { Id = 7, Name = "Margem" },
                new TicketType() { Id = 8, Name = "Termo de Moeda" },
                new TicketType() { Id = 9, Name = "Renda Variável" }
            );

            modelBuilder.Entity<OperationType>().HasData(
                new OperationType() { Id = 1, Name = "Compra" },
                new OperationType() { Id = 2, Name = "Venda" },
                new OperationType() { Id = 3, Name = "Aplicacao" },
                new OperationType() { Id = 4, Name = "Resgate" },
                new OperationType() { Id = 5, Name = "Deposito" },
                new OperationType() { Id = 6, Name = "Retirada" },
                new OperationType() { Id = 7, Name = "Moeda Comprada" },
                new OperationType() { Id = 8, Name = "Moeda Vendida" },
                new OperationType() { Id = 9, Name = "Transferência" }
            );

            modelBuilder.Entity<SettlementType>().HasData(
                new SettlementType() { Id = 1, Name = "CETIP" },
                new SettlementType() { Id = 2, Name = "TED" },
                new SettlementType() { Id = 3, Name = "Termo" },
                new SettlementType() { Id = 4, Name = "À Vista" }
            );

            modelBuilder.Entity<AccountManager>().HasData(
                new AccountManager() { Id = 1, Name = "KRON GESTÃO DE INVESTIMENTOS", Document = "16.804.280/0001-20", SacCode = "" },
                new AccountManager() { Id = 2, Name = "ZERO CONFLICT WEALTH MANAGEMENT LTDA", Document = "05.043.746/0001-04", SacCode = "ZERO CON" },
                new AccountManager() { Id = 3, Name = "GRAU GESTÃO", Document = "07.252.227/0001-73", SacCode = "GRAU GES" },
                new AccountManager() { Id = 4, Name = "BERKANA INVESTIMENTOS E GESTÃO DE RECURSOS LTDA", Document = "10.757.908/0001-06", SacCode = "BERKANA" },
                new AccountManager() { Id = 5, Name = "R2C", Document = "20.495.002/0001-06", SacCode = "R2C" },
                new AccountManager() { Id = 6, Name = "KP GESTÃO DE RECURSOS", Document = "25.098.663/0001-11", SacCode = "KP GESTA" },
                new AccountManager() { Id = 7, Name = "ATRIO GESTORA DE ATIVOS LTDA. (antiga Sul Brasil Gestora)", Document = "24.515.907/0001-51", SacCode = "ATRIO" },
                new AccountManager() { Id = 8, Name = "RIO DAS PEDRAS ADMINISTRAÇÃO E PARTICIPAÇÕES LTDA", Document = "09.311.153/0001-24", SacCode = "RIODASPE" },
                new AccountManager() { Id = 9, Name = "INTEGRAL INVESTIMENTOS LTDA", Document = "06.576.569/0001-86", SacCode = "" },
                new AccountManager() { Id = 10, Name = "OURO PRETO GESTÃO DE RECURSOS S.A.", Document = "11.916.849/0001-26", SacCode = "OURO" },
                new AccountManager() { Id = 11, Name = "KINEA PRIVATE VariableIncome INVESTIMENTOS S/A", Document = "04.661.817/0001-61", SacCode = "KINEAPEQ" },
                new AccountManager() { Id = 12, Name = "TERCON INVESTIMENTOS LTDA", Document = "09.121.454/0001-95", SacCode = "J&M INV" },
                new AccountManager() { Id = 13, Name = "FACT INVESTMENTS GESTAO DE RECURSOS LTDA", Document = "17.480.662/0001-09", SacCode = "FACT INV" },
                new AccountManager() { Id = 14, Name = "QUATÁ GESTÃO DE RECURSOS LTDA", Document = "09.456.933/0001-62", SacCode = "QUATA" },
                new AccountManager() { Id = 15, Name = "G5 ADMINISTRADORA DE RECURSOS LTDA.", Document = "09.446.129/0001-00", SacCode = "G5" },
                new AccountManager() { Id = 16, Name = "POLO CAPITAL GESTÃO DE RECURSOS LTDA", Document = "05.451.668/0001-79", SacCode = "POLO CAP" },
                new AccountManager() { Id = 17, Name = "VALER INVESTIMENTOS (BRAZCORE)", Document = "16.492.426/0001-40", SacCode = "VALER" },
                new AccountManager() { Id = 18, Name = "DEL MONTE - GESTÃO DE INVESTIMENTOS LTDA", Document = "08.592.877/0001-20", SacCode = "" },
                new AccountManager() { Id = 19, Name = "SFI INVESTIMENTOS LTDA", Document = "04.608.141/0001-42", SacCode = "SFI INV" },
                new AccountManager() { Id = 20, Name = "ABC CAPITAL - GESTAO DE INVESTIMENTOS LTDA.", Document = "08.639.165/0001-10", SacCode = "" },
                new AccountManager() { Id = 21, Name = "PRIVATTO INVESTIMENTOS", Document = "19.207.159/0001-00", SacCode = "" },
                new AccountManager() { Id = 22, Name = "GLOBAL GESTÃO E INVESTIMENTOS LTDA", Document = "16.925.467/0001-82", SacCode = "GLOBAL" },
                new AccountManager() { Id = 23, Name = "ITAJUI GESTÃO DE INVESTIMENTOS LTDA", Document = "07.913.960/0001-91", SacCode = "ITAJUI" },
                new AccountManager() { Id = 24, Name = "LATACHE GESTÃO DE RECURSOS LTDA", Document = "12.461.742/0001-01", SacCode = "LATACHE" },
                new AccountManager() { Id = 25, Name = "STARBOARD ASSET LTDA", Document = "15.032.609/0001-10", SacCode = "STARBOAR" },
                new AccountManager() { Id = 26, Name = "PARAGUAÇU INVESTIMENTOS EIRELLI", Document = "21.551.986.0001-68", SacCode = "" },
                new AccountManager() { Id = 27, Name = "WNT GESTORA DE RECURSOS LTDA.", Document = "28.529.868/0001-21", SacCode = "" },
                new AccountManager() { Id = 28, Name = "SOMMA INVESTIMENTOS S.A", Document = "05.563.299/0001-06", SacCode = "" },
                new AccountManager() { Id = 29, Name = "AUSTRO CAPITAL", Document = "09.442.277/0001-49", SacCode = "AUSTROGE" },
                new AccountManager() { Id = 30, Name = "NOVA MILANO INVESTIMENTOS LTDA", Document = "12.263.316/0001-55", SacCode = "" },
                new AccountManager() { Id = 31, Name = "TRIGONO CAPITAL", Document = "28.925.400/0001-27", SacCode = "TRIGONOC" },
                new AccountManager() { Id = 32, Name = "DETOMASO ADMINISTRADORA DE RECURSOS LTDA", Document = "08.926.786/0001-84", SacCode = "DETOMASO" },
                new AccountManager() { Id = 33, Name = "VERITAS - VCM Gestão de Capital - (Log Found)", Document = "12.678.380/0001-05", SacCode = "NINKA FI" },
                new AccountManager() { Id = 34, Name = "CIX CAPITAL", Document = "24.503.059/0001-60", SacCode = "" },
                new AccountManager() { Id = 35, Name = "KP GESTÃO DE RECURSOS (demais fundos)", Document = "25.098.663/0001-11", SacCode = "KP GESTA" },
                new AccountManager() { Id = 36, Name = "VALORA GESTÃO DE INVESTIMENTOS", Document = "07.559.989/0001-17", SacCode = "VALORA" },
                new AccountManager() { Id = 37, Name = "VALER INVESTIMENTOS (BRAZCORE)", Document = "16.492.426/0001-40", SacCode = "VALER" },
                new AccountManager() { Id = 38, Name = "HOA ASET MANAGEMENT GESTÃO DE RECURSOS", Document = "27.390.441/0001-01", SacCode = "" },
                new AccountManager() { Id = 39, Name = "AVENTIS GESTÃO DE RECURSOS LTDA ME (TRIDAFIN)", Document = "27.913.835/0001-99", SacCode = "AVENTIS" },
                new AccountManager() { Id = 40, Name = "EVEREST TRUST GESTORA DE RECURSOS LTDA.", Document = "29.263.481/0001-00", SacCode = "EVEREST" },
                new AccountManager() { Id = 41, Name = "IGUANA INVESTIMENTOS LTDA.", Document = "10.924.308/0001-87", SacCode = "IGUANA" },
                new AccountManager() { Id = 42, Name = "PRISMA CAPITAL LTDA", Document = "27.451.028/0001-00", SacCode = "PRISMA" },
                new AccountManager() { Id = 43, Name = "XP VISTA ASSET MANAGMENT LTDA.", Document = "16.789.525/0001-98", SacCode = "XP VISTA" },
                new AccountManager() { Id = 44, Name = "3J GESTORA DE RECURSOS LTDA.", Document = "29.063.944/0001-90", SacCode = "" },
                new AccountManager() { Id = 45, Name = "ARC (antiga ARKON INVESTIMENTOS LTDA.)", Document = "27.690.986/0001-25", SacCode = "ARC CAPI" },
                new AccountManager() { Id = 46, Name = "TYR GESTÃO DE RECURSOS LTDA.", Document = "16.707.841/0001-73", SacCode = "" },
                new AccountManager() { Id = 47, Name = "ALPS CAPITAL GESTÃO E INVESTIMENTOS LTDA.", Document = "12.620.044/0001-01", SacCode = "ALPS CAP" },
                new AccountManager() { Id = 48, Name = "FARM INVESTIMENTOS GESTÃO DE RECURSOS LTDA", Document = "20.043.909/0001-34", SacCode = "" }
            );

            modelBuilder.Entity<UserAccountManager>().HasData(
                new UserAccountManager() { Id = 1, AccountManagerId = 2, UserIdentifier = "gestor", IsMaster = true },
                new UserAccountManager() { Id = 1, AccountManagerId = 1, UserIdentifier = "gestor2", IsMaster = true });

            modelBuilder.Entity<MarketType>().HasData(
                new SettlementType() { Id = 1, Name = "RF Título Privado" },
                new SettlementType() { Id = 2, Name = "RF Título Público" },
                new SettlementType() { Id = 3, Name = "Renda Variável" },
                new SettlementType() { Id = 4, Name = "Dinheiro" }
            );

            modelBuilder.Entity<TicketSignoff>().HasData(
                new TicketSignoff() { Id = 1, TicketTypeId = TicketTypes.FundQuota, Type = "Cotas - CETIP", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 2, TicketTypeId = TicketTypes.FundQuota, Type = "Cotas - TED", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 3, TicketTypeId = TicketTypes.PrivateFixedIncome, Type = "RF - Título Privado", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 4, TicketTypeId = TicketTypes.PublicFixedIncome, Type = "RF - Título Público", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 5, TicketTypeId = TicketTypes.Contracted, Type = "Compromissada de Compra", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 6, TicketTypeId = TicketTypes.Futures, Type = "Futuros", TimeLimit = new TimeSpan(20, 00, 0) },
                new TicketSignoff() { Id = 7, TicketTypeId = TicketTypes.SwapCetip, Type = "SWAP - CETIP", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 8, TicketTypeId = TicketTypes.Margin, Type = "Margem (Depósito\\Vinculação) Título Público", TimeLimit = new TimeSpan(14, 30, 0) },
                new TicketSignoff() { Id = 9, TicketTypeId = TicketTypes.Margin, Type = "Margem (Depósito\\Vinculação) Título Privado", TimeLimit = new TimeSpan(14, 30, 0) },
                new TicketSignoff() { Id = 10, TicketTypeId = TicketTypes.Margin, Type = "Margem (Depósito\\Vinculação) Renda Variável", TimeLimit = new TimeSpan(14, 30, 0) },
                new TicketSignoff() { Id = 11, TicketTypeId = TicketTypes.Margin, Type = "Margem (Retirada\\Desvinculação) Título Público", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 12, TicketTypeId = TicketTypes.Margin, Type = "Margem (Retirada\\Desvinculação) Título Privado", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 13, TicketTypeId = TicketTypes.Margin, Type = "Margem (Retirada\\Desvinculação) Renda Variável", TimeLimit = new TimeSpan(20, 00, 0) },
                new TicketSignoff() { Id = 14, TicketTypeId = TicketTypes.Margin, Type = "Margem Dinheiro", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 15, TicketTypeId = TicketTypes.CurrencyTerm, Type = "Termo de Moeda", TimeLimit = new TimeSpan(19, 00, 0) },
                new TicketSignoff() { Id = 16, TicketTypeId = TicketTypes.VariableIncome, Type = "Renda Variável", TimeLimit = new TimeSpan(20, 00, 0) }
            );           
        }
    }
}
