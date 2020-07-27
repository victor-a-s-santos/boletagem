using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess
{
    public class Seed
    {
        private static int currentApprovalStep = 0;
        private static int currentApprovalRole = 0;

        private static DateTimeOffset initialSeedTimestamp = new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0));

        public static void Apply(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowStatus>().HasData(
                new WorkflowStatus() { Id = (int)WorkflowStatuses.PendingAdmApproval, Name = "Pendente de Aprovação pela ADM", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.CancelledByAdm, Name = "Cancelada pela ADM", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.WaitingSettlement, Name = "Aguardando Liquidação", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.InSettlement, Name = "Em Liquidação", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.Settled, Name = "Liquidada", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.CancelledBySettlement, Name = "Cancelada pela Liquidação", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.WaitingDetainerSettlement, Name = "Aguardando Liquidação Custódia", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.InDetainerSettlement, Name = "Em Liquidação Custódia", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.CancelledByDetainer, Name = "Cancelada pela Custódia", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.WaitingSettlementMiddle, Name = "Aguardando Liquidação Middle", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.InMiddleSettlement, Name = "Em Liquidação Middle", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.CancelledByMiddle, Name = "Cancelada Middle", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.WaitingVoiceRecord, Name = "Aguardando Registro do Voice", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.InVoiceRecord, Name = "Em Registro do Voice", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.CancelledByAccountManager, Name = "Cancelado pelo Gestor", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.Completed, Name = "Concluído", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowStatus() { Id = (int)WorkflowStatuses.PendingAccountManagerApproval, Name = "Pendente aprovação do Gestor", CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" }
            );

            modelBuilder.Entity<WorkflowApproval>().HasData(
                new WorkflowApproval { Id = (short)WorkflowApprovals.AccountManagers, Name = "Gestores" },
                new WorkflowApproval { Id = (short)WorkflowApprovals.AdminFiduci, Name = "Administração Fiduciária" },
                new WorkflowApproval { Id = (short)WorkflowApprovals.Open, Name = "Open" },
                new WorkflowApproval { Id = (short)WorkflowApprovals.Detainer, Name = "Custódia" },
                new WorkflowApproval { Id = (short)WorkflowApprovals.AdminMiddle, Name = "Middle Adim" }
            );

            GenerateDefaultWorkflows(modelBuilder, new WorkflowType[] {
                new WorkflowType() { Id = (int)WorkflowTypes.FundQuota, Name = "Fluxo de Boletas de Cotas - CETIP", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowType() { Id = (int)WorkflowTypes.PrivateFixedIncome, Name = "Fluxo de Renda Fixa Privada", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowType() { Id = (int)WorkflowTypes.PublicFixedIncome, Name = "Fluxo de Renda Fixa Pública", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowType() { Id = (int)WorkflowTypes.Contracted, Name = "Fluxo de Compromissada de Compra", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowType() { Id = (int)WorkflowTypes.SwapCetip, Name = "Fluxo de Swap CETIP", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowType() { Id = (int)WorkflowTypes.Margin, Name = "Fluxo de Margem", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
                new WorkflowType() { Id = (int)WorkflowTypes.CurrencyTerm, Name = "Fluxo de Termo de Moeda", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" },
            });

            GenerateCotasTEDWorkflow(modelBuilder);
            GenerateCotasCetipVoiceWorkflow(modelBuilder);
            GenerateFuturoWorkflow(modelBuilder);
            GenerateMargemRendaVariavel(modelBuilder);
            GenerateMargemDinheiro(modelBuilder);
            GenerateRendaVariavel(modelBuilder);
        }


        private static void GenerateDefaultWorkflows(ModelBuilder modelBuilder, IEnumerable<WorkflowType> workflows)
        {
            foreach (var workflow in workflows)
            {
                modelBuilder.Entity<WorkflowType>().HasData(workflow);

                modelBuilder.Entity<WorkflowApprovalStep>().HasData(
                new
                {
                    Id = currentApprovalStep + 6,
                    WorkflowTypeId = workflow.Id,
                    WorkflowStatusId = (short)WorkflowStatuses.CancelledBySettlement, // Cancelada pela Liquidação
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
               new
               {
                   Id = currentApprovalStep + 5,
                   WorkflowTypeId = workflow.Id,
                   WorkflowStatusId = (short)WorkflowStatuses.Settled, // Liquidada
                   IsFirstStep = false,
                   IsActive = true,
                   CreationDate = Seed.initialSeedTimestamp,
                   CreationUser = "admin"
               },
               new
               {
                   Id = currentApprovalStep + 3,
                   WorkflowTypeId = workflow.Id,
                   WorkflowStatusId = (short)WorkflowStatuses.CancelledByAdm, // Cancelada pela ADM
                   IsFirstStep = false,
                   IsActive = true,
                   CreationDate = Seed.initialSeedTimestamp,
                   CreationUser = "admin"
               },
               new
               {
                   Id = currentApprovalStep + 4,
                   WorkflowTypeId = workflow.Id,
                   WorkflowStatusId = (short)WorkflowStatuses.InSettlement, // Em Liquidação
                   NextStepApprovedId = currentApprovalStep + 5,
                   NextStepRejectedId = currentApprovalStep + 6,
                   IsFirstStep = false,
                   IsActive = true,
                   CreationDate = Seed.initialSeedTimestamp,
                   CreationUser = "admin"
               },
               new
               {
                   Id = currentApprovalStep + 2,
                   WorkflowTypeId = workflow.Id,
                   WorkflowStatusId = (short)WorkflowStatuses.WaitingSettlement, // Aguardando Liquidação
                   NextStepApprovedId = currentApprovalStep + 4,
                   NextStepRejectedId = currentApprovalStep + 6,
                   IsFirstStep = false,
                   IsActive = true,
                   CreationDate = Seed.initialSeedTimestamp,
                   CreationUser = "admin"
               },
               new
               {
                   Id = currentApprovalStep + 1,
                   WorkflowTypeId = workflow.Id,
                   WorkflowStatusId = (short)WorkflowStatuses.PendingAdmApproval, // Pendente de Aprovação pela ADM
                   NextStepApprovedId = currentApprovalStep + 2,
                   NextStepRejectedId = currentApprovalStep + 3,
                   IsFirstStep = true,
                   IsActive = true,
                   CreationDate = Seed.initialSeedTimestamp,
                   CreationUser = "admin"
               });

                modelBuilder.Entity<WorkflowApprovalRole>().HasData(new
                {
                    Id = currentApprovalRole + 1,
                    ApprovalId = (int)WorkflowApprovals.AdminFiduci, // Administração Fiduciária
                    WorkflowApprovalStepId = currentApprovalStep + 1,
                    RoleId = (int)Roles.ApproveAdmPendingTickets, 
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalRole + 2,
                    ApprovalId = (int)WorkflowApprovals.Open, // Open
                    WorkflowApprovalStepId = currentApprovalStep + 2,
                    RoleId = (int)Roles.ApproveOpenAwaitingSettlement,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalRole + 3,
                    ApprovalId = (int)WorkflowApprovals.Open, //Open
                    WorkflowApprovalStepId = currentApprovalStep + 4,
                    RoleId = (int)Roles.ApproveOpenActiveSettlement,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                });

                currentApprovalStep += 6;
                currentApprovalRole += 3;
            }
        }

        private static void GenerateCotasTEDWorkflow(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowType>().HasData(new WorkflowType() { Id = (int)WorkflowTypes.FundQuotaTED, Name = "Fluxo de Boletas de Cotas - TED", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" });

            modelBuilder.Entity<WorkflowApprovalStep>().HasData(
                            new
                            {
                                Id = currentApprovalStep + 6,
                                WorkflowTypeId = (int)WorkflowTypes.FundQuotaTED,
                                WorkflowStatusId = (short)WorkflowStatuses.CancelledBySettlement, // Cancelada pela Liquidação
                                IsFirstStep = false,
                                IsActive = true,
                                CreationDate = Seed.initialSeedTimestamp,
                                CreationUser = "admin"
                            },
                           new
                           {
                               Id = currentApprovalStep + 5,
                               WorkflowTypeId = (int)WorkflowTypes.FundQuotaTED,
                               WorkflowStatusId = (short)WorkflowStatuses.Settled, // Liquidada
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 3,
                               WorkflowTypeId = (int)WorkflowTypes.FundQuotaTED,
                               WorkflowStatusId = (short)WorkflowStatuses.CancelledByAdm, // Cancelada pela ADM
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 4,
                               WorkflowTypeId = (int)WorkflowTypes.FundQuotaTED,
                               WorkflowStatusId = (short)WorkflowStatuses.InMiddleSettlement, // Em Liquidação
                               NextStepApprovedId = currentApprovalStep + 5,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 2,
                               WorkflowTypeId = (int)WorkflowTypes.FundQuotaTED,
                               WorkflowStatusId = (short)WorkflowStatuses.WaitingSettlementMiddle, // Aguardando Liquidação
                               NextStepApprovedId = currentApprovalStep + 4,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 1,
                               WorkflowTypeId = (int)WorkflowTypes.FundQuotaTED,
                               WorkflowStatusId = (short)WorkflowStatuses.PendingAdmApproval, // Pendente de Aprovação pela ADM
                               NextStepApprovedId = currentApprovalStep + 2,
                               NextStepRejectedId = currentApprovalStep + 3,
                               IsFirstStep = true,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           });

            modelBuilder.Entity<WorkflowApprovalRole>().HasData(new
            {
                Id = currentApprovalRole + 1,
                ApprovalId = (int)WorkflowApprovals.AdminFiduci, // Administração Fiduciária
                WorkflowApprovalStepId = currentApprovalStep + 1,
                RoleId = (int)Roles.ApproveAdmPendingTickets,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 2,
                ApprovalId = (int)WorkflowApprovals.AdminMiddle, // Middle ADM Fiduci
                WorkflowApprovalStepId = currentApprovalStep + 2,
                RoleId = 13,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 3,
                ApprovalId = (int)WorkflowApprovals.AdminMiddle, // Middle ADM Fiduci
                WorkflowApprovalStepId = currentApprovalStep + 4,
                RoleId = 14,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            });

            currentApprovalStep += 6;
            currentApprovalRole += 3;
        }
        private static void GenerateCotasCetipVoiceWorkflow(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowType>().HasData(new WorkflowType() { Id = (int)WorkflowTypes.FundQuotaCetipVoice, Name = "Fluxo de Boletas de Cotas - CETIP Voice", IsActive = false, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" });

            var steps = new List<dynamic>(new[] {
                new
                {
                    Id = currentApprovalStep + 9,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.CancelledByDetainer, // Cancelada pela Liquidação,
                    NextStepApprovedId = default(int?),
                    NextStepRejectedId = default(int?),
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 6,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.CancelledBySettlement, // Cancelada pela Liquidação,
                    NextStepApprovedId = default(int?),
                    NextStepRejectedId = default(int?),
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 8,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.Settled, // Liquidada
                    NextStepApprovedId = default(int?),
                    NextStepRejectedId = default(int?),
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 3,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.CancelledByAdm, // Cancelada pela ADM
                    NextStepApprovedId = default(int?),
                    NextStepRejectedId = default(int?),
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 7,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.InSettlement, // Em Liquidação
                    NextStepApprovedId = (int?)currentApprovalStep + 8,
                    NextStepRejectedId = (int?)currentApprovalStep + 6,
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 5,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.WaitingSettlement, // Aguardando Liquidação
                    NextStepApprovedId = (int?)currentApprovalStep + 7,
                    NextStepRejectedId = (int?)currentApprovalStep + 6,
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 4,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.InVoiceRecord, // Em registro de Voice
                    NextStepApprovedId = (int?)currentApprovalStep + 5,
                    NextStepRejectedId = (int?)currentApprovalStep + 9,
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 2,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.WaitingVoiceRecord, // Aguardando registro do Voice
                    NextStepApprovedId = (int?)currentApprovalStep + 4,
                    NextStepRejectedId = (int?)currentApprovalStep + 9,
                    IsFirstStep = false,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalStep + 1,
                    WorkflowTypeId = (int)WorkflowTypes.FundQuotaCetipVoice,
                    WorkflowStatusId = (short)WorkflowStatuses.PendingAdmApproval, // Pendente de Aprovação pela ADM
                    NextStepApprovedId = (int?)currentApprovalStep + 2,
                    NextStepRejectedId = (int?)currentApprovalStep + 3,
                    IsFirstStep = true,
                    IsActive = true,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                }
            });

            modelBuilder.Entity<WorkflowApprovalStep>().HasData(steps);

            var roles = new List<dynamic>(new[] 
            {
                new
                {
                    Id = currentApprovalRole + 1,
                    ApprovalId = (int)WorkflowApprovals.AdminFiduci, // Administração Fiduciária
                    WorkflowApprovalStepId = currentApprovalStep + 1,
                    RoleId = (int)Roles.ApproveAdmPendingTickets,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalRole + 2,
                    ApprovalId = (int)WorkflowApprovals.Detainer, // Custódia
                    WorkflowApprovalStepId = currentApprovalStep + 2,
                    RoleId = (int)Roles.ApproveCustodyAwaitingSettlement,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalRole + 3,
                    ApprovalId = (int)WorkflowApprovals.Detainer, // Custódia
                    WorkflowApprovalStepId = currentApprovalStep + 4,
                    RoleId = (int)Roles.ApproveCustodyActiveSettlement,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalRole + 4,
                    ApprovalId = (int)WorkflowApprovals.Open, // Open
                    WorkflowApprovalStepId = currentApprovalStep + 5,
                    RoleId = (int)Roles.ApproveOpenAwaitingSettlement,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                },
                new
                {
                    Id = currentApprovalRole + 5,
                    ApprovalId = (int)WorkflowApprovals.Open, // Open
                    WorkflowApprovalStepId = currentApprovalStep + 7,
                    RoleId = (int)Roles.ApproveOpenActiveSettlement,
                    CreationDate = Seed.initialSeedTimestamp,
                    CreationUser = "admin"
                }
            });

            modelBuilder.Entity<WorkflowApprovalRole>().HasData(roles);

            currentApprovalStep += steps.Count;
            currentApprovalRole += roles.Count;
        }
        private static void GenerateFuturoWorkflow(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkflowType>().HasData(new WorkflowType() { Id = (int)WorkflowTypes.Futures, Name = "Fluxo de Futuros BMF", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" });

            modelBuilder.Entity<WorkflowApprovalStep>().HasData(
                            new
                            {
                                Id = currentApprovalStep + 6,
                                WorkflowTypeId = (int)WorkflowTypes.Futures,
                                WorkflowStatusId = (short)WorkflowStatuses.CancelledByDetainer, // Cancelada pela Custódia
                                IsFirstStep = false,
                                IsActive = true,
                                CreationDate = Seed.initialSeedTimestamp,
                                CreationUser = "admin"
                            },
                           new
                           {
                               Id = currentApprovalStep + 5,
                               WorkflowTypeId = (int)WorkflowTypes.Futures,
                               WorkflowStatusId = (short)WorkflowStatuses.Settled, // Liquidada
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 3,
                               WorkflowTypeId = (int)WorkflowTypes.Futures,
                               WorkflowStatusId = (short)WorkflowStatuses.CancelledByAdm, // Cancelada pela ADM
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 4,
                               WorkflowTypeId = (int)WorkflowTypes.Futures,
                               WorkflowStatusId = (short)WorkflowStatuses.InDetainerSettlement, // Em Liquidação Custódia
                               NextStepApprovedId = currentApprovalStep + 5,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 2,
                               WorkflowTypeId = (int)WorkflowTypes.Futures,
                               WorkflowStatusId = (short)WorkflowStatuses.WaitingDetainerSettlement, // Aguardando Liquidação Custódia
                               NextStepApprovedId = currentApprovalStep + 4,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 1,
                               WorkflowTypeId = (int)WorkflowTypes.Futures,
                               WorkflowStatusId = (short)WorkflowStatuses.PendingAdmApproval, // Pendente de Aprovação pela ADM
                               NextStepApprovedId = currentApprovalStep + 2,
                               NextStepRejectedId = currentApprovalStep + 3,
                               IsFirstStep = true,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           });

            modelBuilder.Entity<WorkflowApprovalRole>().HasData(new
            {
                Id = currentApprovalRole + 1,
                ApprovalId = (int)WorkflowApprovals.AdminFiduci, // Administração Fiduciária
                WorkflowApprovalStepId = currentApprovalStep + 1,
                RoleId = (int)Roles.ApproveAdmPendingTickets,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 2,
                ApprovalId = (int)WorkflowApprovals.Detainer, // Custódia
                WorkflowApprovalStepId = currentApprovalStep + 2,
                RoleId = (int)Roles.ApproveCustodyAwaitingSettlement,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 3,
                ApprovalId = (int)WorkflowApprovals.Detainer, // Custódia
                WorkflowApprovalStepId = currentApprovalStep + 4,
                RoleId = (int)Roles.ApproveCustodyActiveSettlement,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            });

            currentApprovalStep += 6;
            currentApprovalRole += 3;
        }
        private static void GenerateMargemRendaVariavel(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkflowType>().HasData(new WorkflowType() { Id = (int)WorkflowTypes.MarginVariableIncome, Name = "Fluxo de Margem Renda Variável", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" });

            modelBuilder.Entity<WorkflowApprovalStep>().HasData(
                            new
                            {
                                Id = currentApprovalStep + 6,
                                WorkflowTypeId = (int)WorkflowTypes.MarginVariableIncome,
                                WorkflowStatusId = (short)WorkflowStatuses.CancelledByDetainer, // Cancelada pela Custódia
                                IsFirstStep = false,
                                IsActive = true,
                                CreationDate = Seed.initialSeedTimestamp,
                                CreationUser = "admin"
                            },
                           new
                           {
                               Id = currentApprovalStep + 5,
                               WorkflowTypeId = (int)WorkflowTypes.MarginVariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.Settled, // Liquidada
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 3,
                               WorkflowTypeId = (int)WorkflowTypes.MarginVariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.CancelledByAdm, // Cancelada pela ADM
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 4,
                               WorkflowTypeId = (int)WorkflowTypes.MarginVariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.InDetainerSettlement, // Em Liquidação Custódia
                               NextStepApprovedId = currentApprovalStep + 5,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 2,
                               WorkflowTypeId = (int)WorkflowTypes.MarginVariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.WaitingDetainerSettlement, // Aguardando Liquidação Custódia
                               NextStepApprovedId = currentApprovalStep + 4,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 1,
                               WorkflowTypeId = (int)WorkflowTypes.MarginVariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.PendingAdmApproval, // Pendente de Aprovação pela ADM
                               NextStepApprovedId = currentApprovalStep + 2,
                               NextStepRejectedId = currentApprovalStep + 3,
                               IsFirstStep = true,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           });

            modelBuilder.Entity<WorkflowApprovalRole>().HasData(new
            {
                Id = currentApprovalRole + 1,
                ApprovalId = (int)WorkflowApprovals.AdminFiduci, // Administração Fiduciária
                WorkflowApprovalStepId = currentApprovalStep + 1,
                RoleId = (int)Roles.ApproveAdmPendingTickets,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 2,
                ApprovalId = (int)WorkflowApprovals.Detainer, // Custódia
                WorkflowApprovalStepId = currentApprovalStep + 2,
                RoleId = (int)Roles.ApproveCustodyAwaitingSettlement,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 3,
                ApprovalId = (int)WorkflowApprovals.Detainer, // Custódia
                WorkflowApprovalStepId = currentApprovalStep + 4,
                RoleId = (int)Roles.ApproveCustodyActiveSettlement,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            });

            currentApprovalStep += 6;
            currentApprovalRole += 3;
        }
        private static void GenerateMargemDinheiro(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkflowType>().HasData(new WorkflowType() { Id = (int)WorkflowTypes.MarginCurrency, Name = "Fluxo de Margem Dinheiro", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" });

            modelBuilder.Entity<WorkflowApprovalStep>().HasData(
                            new
                            {
                                Id = currentApprovalStep + 6,
                                WorkflowTypeId = (int)WorkflowTypes.MarginCurrency,
                                WorkflowStatusId = (short)WorkflowStatuses.CancelledByDetainer, // Cancelada pela Custódia
                                IsFirstStep = false,
                                IsActive = true,
                                CreationDate = Seed.initialSeedTimestamp,
                                CreationUser = "admin"
                            },
                           new
                           {
                               Id = currentApprovalStep + 5,
                               WorkflowTypeId = (int)WorkflowTypes.MarginCurrency,
                               WorkflowStatusId = (short)WorkflowStatuses.Settled, // Liquidada
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 3,
                               WorkflowTypeId = (int)WorkflowTypes.MarginCurrency,
                               WorkflowStatusId = (short)WorkflowStatuses.CancelledByAdm, // Cancelada pela ADM
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 4,
                               WorkflowTypeId = (int)WorkflowTypes.MarginCurrency,
                               WorkflowStatusId = (short)WorkflowStatuses.InDetainerSettlement, // Em Liquidação Custódia
                               NextStepApprovedId = currentApprovalStep + 5,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 2,
                               WorkflowTypeId = (int)WorkflowTypes.MarginCurrency,
                               WorkflowStatusId = (short)WorkflowStatuses.WaitingDetainerSettlement, // Aguardando Liquidação Custódia
                               NextStepApprovedId = currentApprovalStep + 4,
                               NextStepRejectedId = currentApprovalStep + 6,
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 1,
                               WorkflowTypeId = (int)WorkflowTypes.MarginCurrency,
                               WorkflowStatusId = (short)WorkflowStatuses.PendingAdmApproval, // Pendente de Aprovação pela ADM
                               NextStepApprovedId = currentApprovalStep + 2,
                               NextStepRejectedId = currentApprovalStep + 3,
                               IsFirstStep = true,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           });

            modelBuilder.Entity<WorkflowApprovalRole>().HasData(new
            {
                Id = currentApprovalRole + 1,
                ApprovalId = (int)WorkflowApprovals.AdminFiduci, // Administração Fiduciária
                WorkflowApprovalStepId = currentApprovalStep + 1,
                RoleId = (int)Roles.ApproveAdmPendingTickets,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 2,
                ApprovalId = (int)WorkflowApprovals.Detainer, // (Custódia)
                WorkflowApprovalStepId = currentApprovalStep + 2,
                RoleId = (int)Roles.ApproveCustodyAwaitingSettlement,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            },
            new
            {
                Id = currentApprovalRole + 3,
                ApprovalId = (int)WorkflowApprovals.Detainer, // (Custódia)
                WorkflowApprovalStepId = currentApprovalStep + 4,
                RoleId = (int)Roles.ApproveCustodyActiveSettlement,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            });

            currentApprovalStep += 6;
            currentApprovalRole += 3;
        }

        private static void GenerateRendaVariavel(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkflowType>().HasData(new WorkflowType() { Id = (int)WorkflowTypes.VariableIncome, Name = "Fluxo de Renda Variável", IsActive = true, CreationDate = Seed.initialSeedTimestamp, CreationUser = "admin" });

            modelBuilder.Entity<WorkflowApprovalStep>().HasData(
                            new
                            {
                                Id = currentApprovalStep + 3,
                                WorkflowTypeId = (int)WorkflowTypes.VariableIncome,
                                WorkflowStatusId = (short)WorkflowStatuses.CancelledByAccountManager, // Cancelada pelo Gestor
                                IsFirstStep = false,
                                IsActive = true,
                                CreationDate = Seed.initialSeedTimestamp,
                                CreationUser = "admin"
                            },
                           new
                           {
                               Id = currentApprovalStep + 2,
                               WorkflowTypeId = (int)WorkflowTypes.VariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.Completed, // Concluída
                               IsFirstStep = false,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           },
                           new
                           {
                               Id = currentApprovalStep + 1,
                               WorkflowTypeId = (int)WorkflowTypes.VariableIncome,
                               WorkflowStatusId = (short)WorkflowStatuses.PendingAccountManagerApproval, // Pendente de Aprovação pela ADM
                               NextStepApprovedId = currentApprovalStep + 2,
                               NextStepRejectedId = currentApprovalStep + 3,
                               IsFirstStep = true,
                               IsActive = true,
                               CreationDate = Seed.initialSeedTimestamp,
                               CreationUser = "admin"
                           });

            modelBuilder.Entity<WorkflowApprovalRole>().HasData(new
            {
                Id = currentApprovalRole + 1,
                ApprovalId = (int)WorkflowApprovals.AccountManagers, // Account Manager
                WorkflowApprovalStepId = currentApprovalStep + 1,
                RoleId = (int)Roles.ApproveAccountManagerPendingTicket,
                CreationDate = Seed.initialSeedTimestamp,
                CreationUser = "admin"
            });

            currentApprovalStep += 6;
            currentApprovalRole += 1;
        }
    }
}
