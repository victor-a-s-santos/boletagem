using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "Approval",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
            //     values: new object[,]
            //     {
            //         { 1, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Gestores" },
            //         { 2, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Administração Fiduciária" },
            //         { 3, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Open" },
            //         { 4, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Custódia" },
            //         { 5, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Middle Adim" }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "Status",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
            //     values: new object[,]
            //     {
            //         { (short)17, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(400), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Pendente aprovação do Gestor" },
            //         { (short)16, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Concluído" },
            //         { (short)15, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelado pelo Gestor" },
            //         { (short)14, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(380), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Registro do Voice" },
            //         { (short)12, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(380), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada Middle" },
            //         { (short)11, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(370), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Liquidação Middle" },
            //         { (short)10, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(360), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Liquidação Middle" },
            //         { (short)9, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(360), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada pela Custódia" },
            //         { (short)13, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(380), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Registro do Voice" },
            //         { (short)7, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(350), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Liquidação Custódia" },
            //         { (short)6, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(350), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada pela Liquidação" },
            //         { (short)5, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(340), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Liquidada" },
            //         { (short)4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(330), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Liquidação" },
            //         { (short)3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(330), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Liquidação" },
            //         { (short)2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(300), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada pela ADM" },
            //         { (short)1, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 242, DateTimeKind.Unspecified).AddTicks(2390), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Pendente de Aprovação pela ADM" },
            //         { (short)8, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 251, DateTimeKind.Unspecified).AddTicks(360), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Liquidação Custódia" }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "Type",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "Name" },
            //     values: new object[,]
            //     {
            //         { 11, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5600), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Margem Renda Variável" },
            //         { 10, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2320), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Futuros BMF" },
            //         { 9, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(5700), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Boletas de Cotas - CETIP Voice" },
            //         { 8, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5580), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Boletas de Cotas - TED" },
            //         { 7, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(4280), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Termo de Moeda" },
            //         { 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(4260), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Fixa Pública" },
            //         { 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(4270), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Swap CETIP" },
            //         { 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(4270), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Compromissada de Compra" },
            //         { 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(4240), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Fixa Privada" },
            //         { 1, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(2980), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Boletas de Cotas - CETIP" },
            //         { 12, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6740), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Margem Dinheiro" },
            //         { 6, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 252, DateTimeKind.Unspecified).AddTicks(4280), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Margem" },
            //         { 13, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 262, DateTimeKind.Unspecified).AddTicks(4690), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Variável" }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
            //     values: new object[,]
            //     {
            //         { 6, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(1470), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 1 },
            //         { 48, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5620), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 8 },
            //         { 47, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5630), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 8 },
            //         { 45, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5630), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 8 },
            //         { 57, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(5800), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 9 },
            //         { 54, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7710), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 9 },
            //         { 56, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7750), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 9 },
            //         { 51, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7750), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 9 },
            //         { 39, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5760), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 7 },
            //         { 63, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2510), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 10 },
            //         { 60, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2520), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 10 },
            //         { 69, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5700), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 11 },
            //         { 68, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5710), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 11 },
            //         { 66, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5710), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 11 },
            //         { 75, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6820), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 12 },
            //         { 74, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6820), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 12 },
            //         { 72, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6830), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 12 },
            //         { 62, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2510), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 10 },
            //         { 78, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 262, DateTimeKind.Unspecified).AddTicks(4750), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)15, 13 },
            //         { 41, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5760), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 7 },
            //         { 33, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5680), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 6 },
            //         { 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(2610), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 1 },
            //         { 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(2630), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 1 },
            //         { 12, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5330), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 2 },
            //         { 11, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5340), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 2 },
            //         { 9, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5340), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 2 },
            //         { 18, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5430), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 3 },
            //         { 17, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5430), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 3 },
            //         { 42, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5760), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 7 },
            //         { 15, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5440), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 3 },
            //         { 23, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5520), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 4 },
            //         { 21, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5520), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 4 },
            //         { 30, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5590), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 5 },
            //         { 29, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5600), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 5 },
            //         { 27, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5600), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 5 },
            //         { 36, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5670), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 6 },
            //         { 35, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5680), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 6 },
            //         { 24, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5510), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 4 },
            //         { 77, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 262, DateTimeKind.Unspecified).AddTicks(4750), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)16, 13 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
            //     values: new object[,]
            //     {
            //         { 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(2640), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 5, 6, (short)4, 1 },
            //         { 10, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5350), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 11, 12, (short)4, 2 },
            //         { 16, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5440), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 17, 18, (short)4, 3 },
            //         { 22, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5530), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 23, 24, (short)4, 4 },
            //         { 28, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5610), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 29, 30, (short)4, 5 },
            //         { 34, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5690), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 35, 36, (short)4, 6 },
            //         { 40, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5770), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 41, 42, (short)4, 7 },
            //         { 46, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5640), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 47, 48, (short)11, 8 },
            //         { 55, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7830), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 56, 54, (short)4, 9 },
            //         { 61, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2520), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 62, 63, (short)8, 10 },
            //         { 67, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5720), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 68, 69, (short)8, 11 },
            //         { 73, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6830), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 74, 75, (short)8, 12 },
            //         { 76, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 262, DateTimeKind.Unspecified).AddTicks(4760), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 77, 78, (short)17, 13 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
            //     values: new object[,]
            //     {
            //         { 3, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5030), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 4 },
            //         { 38, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6900), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 73 },
            //         { 35, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5960), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 67 },
            //         { 32, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2810), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 61 },
            //         { 29, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(8610), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 55 },
            //         { 24, 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5690), new TimeSpan(0, -3, 0, 0, 0)), "admin", 14, 46 },
            //         { 18, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5730), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 34 },
            //         { 15, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5650), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 28 },
            //         { 21, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5850), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 40 },
            //         { 12, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5570), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 22 },
            //         { 9, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5480), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 16 },
            //         { 6, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5400), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 10 },
            //         { 39, 1, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 262, DateTimeKind.Unspecified).AddTicks(4790), new TimeSpan(0, -3, 0, 0, 0)), "admin", 27, 76 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
            //     values: new object[,]
            //     {
            //         { 26, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5610), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 28, 30, (short)3, 5 },
            //         { 32, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5690), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 34, 36, (short)3, 6 },
            //         { 71, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6840), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 73, 75, (short)7, 12 },
            //         { 38, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5820), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 40, 42, (short)3, 7 },
            //         { 14, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5450), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 16, 18, (short)3, 3 },
            //         { 44, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5640), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 46, 48, (short)10, 8 },
            //         { 53, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7840), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 55, 54, (short)3, 9 },
            //         { 8, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5350), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 10, 12, (short)3, 2 },
            //         { 59, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2530), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 61, 63, (short)7, 10 },
            //         { 65, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5720), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 67, 69, (short)7, 11 },
            //         { 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(3680), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 4, 6, (short)3, 1 },
            //         { 20, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5530), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 22, 24, (short)3, 4 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
            //     values: new object[,]
            //     {
            //         { 2, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5010), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 2 },
            //         { 5, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 8 },
            //         { 34, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5960), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 65 },
            //         { 8, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5480), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 14 },
            //         { 11, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5560), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 20 },
            //         { 31, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2800), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 59 },
            //         { 14, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5640), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 26 },
            //         { 17, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5720), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 32 },
            //         { 37, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6900), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 71 },
            //         { 20, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5850), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 38 },
            //         { 28, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(8610), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 53 },
            //         { 23, 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5680), new TimeSpan(0, -3, 0, 0, 0)), "admin", 13, 44 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
            //     values: new object[,]
            //     {
            //         { 64, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5730), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 65, 66, (short)1, 11 },
            //         { 58, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2530), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 59, 60, (short)1, 10 },
            //         { 52, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7840), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 53, 57, (short)14, 9 },
            //         { 31, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5700), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 32, 33, (short)1, 6 },
            //         { 37, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5820), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 38, 39, (short)1, 7 },
            //         { 25, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5620), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 26, 27, (short)1, 5 },
            //         { 19, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5540), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 20, 21, (short)1, 4 },
            //         { 13, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5450), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 14, 15, (short)1, 3 },
            //         { 7, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5360), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 8, 9, (short)1, 2 },
            //         { 1, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(3700), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 2, 3, (short)1, 1 },
            //         { 43, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5640), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 44, 45, (short)1, 8 },
            //         { 70, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6840), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 71, 72, (short)1, 12 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
            //     values: new object[,]
            //     {
            //         { 1, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(4080), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 1 },
            //         { 4, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 7 },
            //         { 7, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5480), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 13 },
            //         { 10, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5560), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 19 },
            //         { 13, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5640), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 25 },
            //         { 16, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5720), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 31 },
            //         { 19, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 254, DateTimeKind.Unspecified).AddTicks(5840), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 37 },
            //         { 22, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 255, DateTimeKind.Unspecified).AddTicks(5680), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 43 },
            //         { 27, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(8600), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 52 },
            //         { 30, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 259, DateTimeKind.Unspecified).AddTicks(2800), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 58 },
            //         { 33, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 260, DateTimeKind.Unspecified).AddTicks(5800), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 64 },
            //         { 36, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 261, DateTimeKind.Unspecified).AddTicks(6890), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 70 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
            //     values: new object[] { 50, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7850), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 52, 57, (short)13, 9 });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
            //     values: new object[] { 26, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(8600), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 50 });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
            //     values: new object[] { 49, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(7850), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 50, 51, (short)1, 9 });

            // migrationBuilder.InsertData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
            //     values: new object[] { 25, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 48, 15, 257, DateTimeKind.Unspecified).AddTicks(8590), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 49 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 6);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 7);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 8);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 9);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 10);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 11);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 12);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 13);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 14);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 15);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 16);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 17);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 18);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 19);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 20);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 21);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 22);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 23);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 24);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 25);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 26);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 27);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 28);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 29);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 30);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 31);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 32);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 33);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 34);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 35);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 36);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 37);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 38);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalRole",
            //     keyColumn: "Id",
            //     keyValue: 39);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)12);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Approval",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Approval",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Approval",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Approval",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Approval",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 7);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 13);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 19);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 25);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 31);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 37);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 43);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 49);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 58);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 64);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 70);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 76);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 8);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 9);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 14);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 15);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 20);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 21);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 26);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 27);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 32);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 33);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 38);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 39);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 44);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 45);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 50);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 51);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 59);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 60);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 65);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 66);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 71);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 72);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 77);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 78);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)1);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)17);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 10);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 16);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 22);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 28);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 34);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 40);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 46);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 52);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 61);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 67);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 73);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)2);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)7);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)10);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)13);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)15);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)16);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 13);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 6);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 11);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 12);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 17);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 18);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 23);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 24);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 29);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 30);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 35);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 36);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 41);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 42);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 47);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 48);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 53);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 57);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 62);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 63);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 68);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 69);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 74);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 75);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)8);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)11);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)14);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 55);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)3);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)9);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 6);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 7);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 8);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 10);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 11);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 12);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 54);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "ApprovalStep",
            //     keyColumn: "Id",
            //     keyValue: 56);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)4);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)5);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Status",
            //     keyColumn: "Id",
            //     keyValue: (short)6);

            // migrationBuilder.DeleteData(
            //     schema: "Workflow",
            //     table: "Type",
            //     keyColumn: "Id",
            //     keyValue: 9);
        }
    }
}
