using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess
{
    public class Seed
    {
        private static DateTime initialSeedTimestampNonOffset = new DateTime(2019, 6, 4, 15, 40, 50, 895, DateTimeKind.Unspecified);
        private static DateTimeOffset initialSeedTimestamp = new DateTimeOffset(initialSeedTimestampNonOffset, new TimeSpan(0, -3, 0, 0, 0));
        public static void Apply(ModelBuilder modelBuilder)
        {
            var password1 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTA==");
            var password2 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTB==");
            var password3 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTC==");
            var password4 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTD==");
            var password5 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTE==");
            var password6 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTF==");
            var password7 = AuthService.GetHashed("test", "IHxey3JXmJa0gWCXqUxvTG==");


            User[] users =
            {
                new User() { Id = 1, UserName = "gestor", Name = "Estelle Ankunding (Gestor)", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "estelle_ankunding36@hotmail.com", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password1, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTA==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
                new User() { Id = 2, UserName = "admin.fiduci", Name = "Dee Rippin (Adm Fiduci)", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "dee_rippin@yahoo.com", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password2, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTB==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
                new User() { Id = 3, UserName = "custodia", Name = "Garrett Dickinson (Custódia)", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "garrett_dickinson37@hotmail.com", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password3, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTC==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
                new User() { Id = 4, UserName = "open", Name = "Zula Stokes V (Open)", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "zula_stokes37@gmail.com", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password4, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTD==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
                new User() { Id = 5, UserName = "middle.adm", Name = "Maia Spinka (Middle ADM)", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "maia_spinka@gmail.com", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password5, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTE==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
                new User() { Id = 6, UserName = "admin", Name = "Administrator", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "admin@admin.com.br", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password6, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTF==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
                new User() { Id = 7, UserName = "gestor2", Name = "Ms. Bart Larson (Gestor)", AccessFailedCount = 0, Active = true, ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null, Email = "bart_larson@yahoo.com", EmailConfirmed = true, LastPasswordChangedDate = null, LockoutEnabled = false, LockoutEndDateUtc = null, PasswordHash = password7, PhoneNumber = null, PhoneNumberConfirmed = true, UserDocument = null, Salt = "IHxey3JXmJa0gWCXqUxvTG==", PasswordExpirationDate = Seed.initialSeedTimestamp.AddYears(1) },
            };

            UserPassword[] userPasswords =
            {
                new UserPassword() { Id = 1, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password1, UserId = 1 },
                new UserPassword() { Id = 2, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password2, UserId = 2 },
                new UserPassword() { Id = 3, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password3, UserId = 3 },
                new UserPassword() { Id = 4, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password4, UserId = 4 },
                new UserPassword() { Id = 5, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password5, UserId = 5 },
                new UserPassword() { Id = 6, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password6, UserId = 6 },
                new UserPassword() { Id = 7, CreationDate = Seed.initialSeedTimestampNonOffset, PasswordHash = password7, UserId = 7 },
            };

            Group[] groups =
            {
                new Group() { Id = 1, Name = "Gestores Master", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Group() { Id = 2, Name = "Adm. Fiduci", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Group() { Id = 3, Name = "Custodia", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Group() { Id = 4, Name = "Liquidação (Open)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Group() { Id = 5, Name = "Middle Adm. Fiduci", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Group() { Id = 6, Name = "Administrator do Sistema", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Group() { Id = 7, Name = "Gestores Boleta", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null }
            };

            UserGroup[] usersGroups =
            {
                new UserGroup() { UserId = 1, GroupId = 1 },
                new UserGroup() { UserId = 2, GroupId = 2 },
                new UserGroup() { UserId = 3, GroupId = 3 },
                new UserGroup() { UserId = 4, GroupId = 4 },
                new UserGroup() { UserId = 5, GroupId = 5 },
                new UserGroup() { UserId = 6, GroupId = 6 },
                new UserGroup() { UserId = 7, GroupId = 1 },
            };


            Role[] roles =
            {
                new Role(){ Id = (int)Roles.CreateQuotaTicket, Name = "Criar boletas de Cotas", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreatePrivateFixedIncomeTicket, Name = "Criar boletas de RF - Título Privado", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreatePublicFixedIncomeTicket, Name = "Criar boletas de RF - Título Público", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateFuturesTicket, Name = "Criar boletas de Futuros", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateMarginTicket, Name = "Criar boletas de Margem", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateContractedTicket, Name = "Criar boletas de Compromissada de Compra", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateCurrencyTermTicket, Name = "Criar boletas de Termo de Moeda", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateVariableIncomeTicket, Name = "Criar boletas de Renda Variável", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateSwapCETIPTicket, Name = "Criar boletas de Swap-CETIP", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },

                new Role(){ Id = (int)Roles.ApproveAdmPendingTickets, Name = "Aprovar boletas Pendente de Aprovação pela ADM", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveOpenAwaitingSettlement, Name = "Aprovar boletas Aguardando Liquidação (Open)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveOpenActiveSettlement, Name = "Aprovar boletas Em Liquidação (Open)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveMiddleAwaitingSettlement, Name = "Aprovar boletas Aguardando Liquidação (Middle ADM Fiduci)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveMiddleActiveSettlement, Name = "Aprovar boletas Em Liquidação (Middle ADM Fiduci)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveCustodyAwaitingSettlement, Name = "Aprovar boletas Aguardando Liquidação (Custódia)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveCustodyActiveSettlement, Name = "Aprovar boletas Em Liquidação (Custódia)", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ApproveAccountManagerPendingTicket, Name = "Aprovar boletas Pendente Aprovação do Gestor", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },

                new Role(){ Id = (int)Roles.ViewApprovalHistory, Name = "Visualizar histórico de aprovação", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },

                new Role(){ Id = (int)Roles.ViewQuotaMonitor, Name = "Visualizar monitor de Cotas", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewPrivateFixedIncomeMonitor, Name = "Visualizar monitor de RF - Título Privado", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewPublicFixedIncomeMonitor, Name = "Visualizar monitor de RF - Título Público", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewFuturesMonitor, Name = "Visualizar monitor de Futuros", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewMarginMonitor, Name = "Visualizar monitor de Margem", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewContractedMonitor, Name = "Visualizar monitor de Compromissada de Compra", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewCurrencyTermMonitor, Name = "Visualizar monitor de Termo de Moeda", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewVariableIncomeMonitor, Name = "Visualizar monitor de Renda Variável", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.ViewSwapCETIPMonitor, Name = "Visualizar monitor de Swap-CETIP", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateMasterUser, Name = "Criar Usuário Master", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                new Role(){ Id = (int)Roles.CreateSubordinateUser, Name = "Criar Usuário Subordinado", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                //new Role(){ Id = (int)Roles.ChangeDefaultLimitTime, Name = "Alterar horário limite default signoff", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
                //new Role(){ Id = (int)Roles.ChangeLimitTime, Name = "Estender horário limite signoff", ChangeDate = Seed.initialSeedTimestamp, ChangeUser = null, CreationDate = Seed.initialSeedTimestamp, CreationUser = null },
            };

            var groupRoles = new List<GroupRole>();

            groupRoles.AddRange(new GroupRole[] { // Gestor Master
                new GroupRole() { RoleId = (int)Roles.CreateQuotaTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreatePrivateFixedIncomeTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreatePublicFixedIncomeTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreateFuturesTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreateMarginTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreateContractedTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreateCurrencyTermTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreateSwapCETIPTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewQuotaMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewPrivateFixedIncomeMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewPublicFixedIncomeMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewFuturesMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewMarginMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewContractedMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewCurrencyTermMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewVariableIncomeMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ViewSwapCETIPMonitor, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.ApproveAccountManagerPendingTicket, GroupId = 1 },
                new GroupRole() { RoleId = (int)Roles.CreateSubordinateUser, GroupId = 1 },
                //new GroupRole() { RoleId = (int)Roles.ChangeDefaultLimitTime, GroupId = 1 }
            });

            groupRoles.AddRange(new GroupRole[] { // Adm. Fiduci
                new GroupRole() { RoleId = (int)Roles.ApproveAdmPendingTickets, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewQuotaMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewPrivateFixedIncomeMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewPublicFixedIncomeMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewFuturesMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewMarginMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewContractedMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewCurrencyTermMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewVariableIncomeMonitor, GroupId = 2 },
                new GroupRole() { RoleId = (int)Roles.ViewSwapCETIPMonitor, GroupId = 2 }
            });

            groupRoles.AddRange(new GroupRole[] { // Custódia
                new GroupRole() { RoleId = (int)Roles.CreateVariableIncomeTicket, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ApproveCustodyAwaitingSettlement, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ApproveCustodyActiveSettlement, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewQuotaMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewPrivateFixedIncomeMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewPublicFixedIncomeMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewFuturesMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewMarginMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewContractedMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewCurrencyTermMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewVariableIncomeMonitor, GroupId = 3 },
                new GroupRole() { RoleId = (int)Roles.ViewSwapCETIPMonitor, GroupId = 3 }
            });

            groupRoles.AddRange(new GroupRole[] { // Liquidação / Open
                new GroupRole() { RoleId = (int)Roles.ApproveOpenAwaitingSettlement, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ApproveOpenActiveSettlement, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewQuotaMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewPrivateFixedIncomeMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewPublicFixedIncomeMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewFuturesMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewMarginMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewContractedMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewCurrencyTermMonitor, GroupId = 4 },
                new GroupRole() { RoleId = (int)Roles.ViewVariableIncomeMonitor, GroupId = 4 }, 
                new GroupRole() { RoleId = (int)Roles.ViewSwapCETIPMonitor, GroupId = 4 }
            });

            groupRoles.AddRange(new GroupRole[] { // Middle
                new GroupRole() { RoleId = (int)Roles.ApproveMiddleAwaitingSettlement, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ApproveMiddleActiveSettlement, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewQuotaMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewPrivateFixedIncomeMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewPublicFixedIncomeMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewFuturesMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewMarginMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewContractedMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewCurrencyTermMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewVariableIncomeMonitor, GroupId = 5 },
                new GroupRole() { RoleId = (int)Roles.ViewSwapCETIPMonitor, GroupId = 5 }
            });

            groupRoles.AddRange(new GroupRole[] { // Adm
                new GroupRole() { RoleId = (int)Roles.CreateMasterUser, GroupId = 6 },
            });

            groupRoles.AddRange(new GroupRole[] { // Gestor Boleta
                new GroupRole() { RoleId = (int)Roles.CreateQuotaTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreatePrivateFixedIncomeTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreatePublicFixedIncomeTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreateFuturesTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreateMarginTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreateContractedTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreateCurrencyTermTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.CreateSwapCETIPTicket, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewQuotaMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewPrivateFixedIncomeMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewPublicFixedIncomeMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewFuturesMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewMarginMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewContractedMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewCurrencyTermMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewVariableIncomeMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ViewSwapCETIPMonitor, GroupId = 7 },
                new GroupRole() { RoleId = (int)Roles.ApproveAccountManagerPendingTicket, GroupId = 7 },
                //new GroupRole() { RoleId = (int)Roles.ChangeLimitTime, GroupId = 7 }
            });

            // Visualizar monitor - todos exceto admin
            groupRoles.Add(new GroupRole() { RoleId = (int)Roles.ViewApprovalHistory, GroupId = 1 });
            groupRoles.Add(new GroupRole() { RoleId = (int)Roles.ViewApprovalHistory, GroupId = 2 });
            groupRoles.Add(new GroupRole() { RoleId = (int)Roles.ViewApprovalHistory, GroupId = 3 });
            groupRoles.Add(new GroupRole() { RoleId = (int)Roles.ViewApprovalHistory, GroupId = 4 });
            groupRoles.Add(new GroupRole() { RoleId = (int)Roles.ViewApprovalHistory, GroupId = 5 });

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserPassword>().HasData(userPasswords);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<GroupRole>().HasData(groupRoles);
            modelBuilder.Entity<UserGroup>().HasData(usersGroups);
        }

    }
}