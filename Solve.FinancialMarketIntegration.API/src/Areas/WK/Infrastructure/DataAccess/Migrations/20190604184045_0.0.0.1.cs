using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Workflow");

            migrationBuilder.CreateTable(
                name: "Approval",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalStep",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    NextStepApprovedId = table.Column<int>(nullable: true),
                    NextStepRejectedId = table.Column<int>(nullable: true),
                    WorkflowStatusId = table.Column<short>(nullable: true),
                    IsFirstStep = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    WorkflowTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalStep_ApprovalStep_NextStepApprovedId",
                        column: x => x.NextStepApprovedId,
                        principalSchema: "Workflow",
                        principalTable: "ApprovalStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalStep_ApprovalStep_NextStepRejectedId",
                        column: x => x.NextStepRejectedId,
                        principalSchema: "Workflow",
                        principalTable: "ApprovalStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalStep_Status_WorkflowStatusId",
                        column: x => x.WorkflowStatusId,
                        principalSchema: "Workflow",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalStep_Type_WorkflowTypeId",
                        column: x => x.WorkflowTypeId,
                        principalSchema: "Workflow",
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRole",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    ApprovalId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    WorkflowApprovalStepId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRole_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Workflow",
                        principalTable: "Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalRole_ApprovalStep_WorkflowApprovalStepId",
                        column: x => x.WorkflowApprovalStepId,
                        principalSchema: "Workflow",
                        principalTable: "ApprovalStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalStepResult",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    ApprovalId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    WorkflowStepResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStepResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalStepResult_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Workflow",
                        principalTable: "Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StepResult",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    NextStepApprovedId = table.Column<int>(nullable: true),
                    NextStepRejectedId = table.Column<int>(nullable: true),
                    WorkflowStatusId = table.Column<short>(nullable: true),
                    StepOriginId = table.Column<int>(nullable: false),
                    IsFirstStep = table.Column<bool>(nullable: false),
                    WorkflowId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepResult_StepResult_NextStepApprovedId",
                        column: x => x.NextStepApprovedId,
                        principalSchema: "Workflow",
                        principalTable: "StepResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StepResult_StepResult_NextStepRejectedId",
                        column: x => x.NextStepRejectedId,
                        principalSchema: "Workflow",
                        principalTable: "StepResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StepResult_ApprovalStep_StepOriginId",
                        column: x => x.StepOriginId,
                        principalSchema: "Workflow",
                        principalTable: "ApprovalStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepResult_Status_WorkflowStatusId",
                        column: x => x.WorkflowStatusId,
                        principalSchema: "Workflow",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workflow",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    CurrentStepId = table.Column<int>(nullable: true),
                    IsFinished = table.Column<bool>(nullable: false),
                    WorkflowTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workflow_StepResult_CurrentStepId",
                        column: x => x.CurrentStepId,
                        principalSchema: "Workflow",
                        principalTable: "StepResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workflow_Type_WorkflowTypeId",
                        column: x => x.WorkflowTypeId,
                        principalSchema: "Workflow",
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "Approval",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
                values: new object[,]
                {
                    { 1, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Gestores" },
                    { 2, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Administração Fiduciária" },
                    { 3, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Open" },
                    { 4, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Custódia" },
                    { 5, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Middle Adim" }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "Status",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
                values: new object[,]
                {
                    { (short)17, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Pendente aprovação do Gestor" },
                    { (short)16, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Concluído" },
                    { (short)15, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelado pelo Gestor" },
                    { (short)14, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Registro do Voice" },
                    { (short)12, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada Middle" },
                    { (short)11, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Liquidação Middle" },
                    { (short)10, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Liquidação Middle" },
                    { (short)9, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada pela Custódia" },
                    { (short)13, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Registro do Voice" },
                    { (short)7, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Liquidação Custódia" },
                    { (short)6, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada pela Liquidação" },
                    { (short)5, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Liquidada" },
                    { (short)4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Liquidação" },
                    { (short)3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Aguardando Liquidação" },
                    { (short)2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Cancelada pela ADM" },
                    { (short)1, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 685, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Pendente de Aprovação pela ADM" },
                    { (short)8, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 686, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", "Em Liquidação Custódia" }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "Type",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "Name" },
                values: new object[,]
                {
                    { 11, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Margem Renda Variável" },
                    { 10, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Futuros BMF" },
                    { 9, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Boletas de Cotas - CETIP Voice" },
                    { 8, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Boletas de Cotas - TED" },
                    { 7, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Termo de Moeda" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Fixa Pública" },
                    { 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Swap CETIP" },
                    { 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Compromissada de Compra" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Fixa Privada" },
                    { 1, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Boletas de Cotas - CETIP" },
                    { 12, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Margem Dinheiro" },
                    { 6, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 687, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Margem" },
                    { 13, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, "Fluxo de Renda Variável" }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 6, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 1 },
                    { 48, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 8 },
                    { 47, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 8 },
                    { 45, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 8 },
                    { 57, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 9 },
                    { 54, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 9 },
                    { 56, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 9 },
                    { 51, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 9 },
                    { 39, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 7 },
                    { 63, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 10 },
                    { 60, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 10 },
                    { 69, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 11 },
                    { 68, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 11 },
                    { 66, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 11 },
                    { 75, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)9, 12 },
                    { 74, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 12 },
                    { 72, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 12 },
                    { 62, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 10 },
                    { 78, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)15, 13 },
                    { 41, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 7 },
                    { 33, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 6 },
                    { 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 1 },
                    { 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 1 },
                    { 12, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 2 },
                    { 11, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 2 },
                    { 9, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 2 },
                    { 18, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 3 },
                    { 17, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 3 },
                    { 42, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 7 },
                    { 15, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 3 },
                    { 23, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 4 },
                    { 21, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 4 },
                    { 30, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 5 },
                    { 29, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 5 },
                    { 27, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)2, 5 },
                    { 36, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 6 },
                    { 35, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)5, 6 },
                    { 24, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)6, 4 },
                    { 77, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, null, null, (short)16, 13 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 5, 6, (short)4, 1 },
                    { 10, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 11, 12, (short)4, 2 },
                    { 16, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 17, 18, (short)4, 3 },
                    { 22, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 23, 24, (short)4, 4 },
                    { 28, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 29, 30, (short)4, 5 },
                    { 34, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 35, 36, (short)4, 6 },
                    { 40, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 41, 42, (short)4, 7 },
                    { 46, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 47, 48, (short)11, 8 },
                    { 55, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 56, 54, (short)4, 9 },
                    { 61, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 62, 63, (short)8, 10 },
                    { 67, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 68, 69, (short)8, 11 },
                    { 73, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 74, 75, (short)8, 12 },
                    { 76, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 77, 78, (short)17, 13 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[,]
                {
                    { 3, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 4 },
                    { 38, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 73 },
                    { 35, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 67 },
                    { 32, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 61 },
                    { 29, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 55 },
                    { 24, 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 14, 46 },
                    { 18, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 34 },
                    { 15, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 28 },
                    { 21, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 40 },
                    { 12, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 22 },
                    { 9, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 16 },
                    { 6, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 12, 10 },
                    { 39, 1, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 27, 76 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 26, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 28, 30, (short)3, 5 },
                    { 32, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 34, 36, (short)3, 6 },
                    { 71, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 73, 75, (short)7, 12 },
                    { 38, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 40, 42, (short)3, 7 },
                    { 14, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 16, 18, (short)3, 3 },
                    { 44, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 46, 48, (short)10, 8 },
                    { 53, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 55, 54, (short)3, 9 },
                    { 8, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 10, 12, (short)3, 2 },
                    { 59, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 61, 63, (short)7, 10 },
                    { 65, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 67, 69, (short)7, 11 },
                    { 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 4, 6, (short)3, 1 },
                    { 20, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 22, 24, (short)3, 4 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[,]
                {
                    { 2, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 2 },
                    { 5, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 8 },
                    { 34, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 65 },
                    { 8, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 14 },
                    { 11, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 20 },
                    { 31, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 59 },
                    { 14, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 26 },
                    { 17, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 32 },
                    { 37, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 71 },
                    { 20, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 38 },
                    { 28, 3, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 11, 53 },
                    { 23, 5, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 13, 44 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 64, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 65, 66, (short)1, 11 },
                    { 58, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 59, 60, (short)1, 10 },
                    { 52, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 53, 57, (short)14, 9 },
                    { 31, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 32, 33, (short)1, 6 },
                    { 37, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 38, 39, (short)1, 7 },
                    { 25, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 26, 27, (short)1, 5 },
                    { 19, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 20, 21, (short)1, 4 },
                    { 13, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 14, 15, (short)1, 3 },
                    { 7, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 8, 9, (short)1, 2 },
                    { 1, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 2, 3, (short)1, 1 },
                    { 43, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 44, 45, (short)1, 8 },
                    { 70, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 71, 72, (short)1, 12 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[,]
                {
                    { 1, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 688, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 1 },
                    { 4, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 7 },
                    { 7, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 13 },
                    { 10, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 19 },
                    { 13, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 25 },
                    { 16, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 31 },
                    { 19, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 37 },
                    { 22, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 689, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 43 },
                    { 27, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 16, 52 },
                    { 30, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 692, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 58 },
                    { 33, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 693, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 64 },
                    { 36, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 694, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 70 }
                });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[] { 50, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, false, 52, 57, (short)13, 9 });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[] { 26, 4, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 15, 50 });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalStep",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "IsActive", "IsFirstStep", "NextStepApprovedId", "NextStepRejectedId", "WorkflowStatusId", "WorkflowTypeId" },
                values: new object[] { 49, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", true, true, 50, 51, (short)1, 9 });

            migrationBuilder.InsertData(
                schema: "Workflow",
                table: "ApprovalRole",
                columns: new[] { "Id", "ApprovalId", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "RoleId", "WorkflowApprovalStepId" },
                values: new object[] { 25, 2, null, null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 44, 691, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin", 10, 49 });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRole_ApprovalId",
                schema: "Workflow",
                table: "ApprovalRole",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRole_WorkflowApprovalStepId",
                schema: "Workflow",
                table: "ApprovalRole",
                column: "WorkflowApprovalStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStep_NextStepApprovedId",
                schema: "Workflow",
                table: "ApprovalStep",
                column: "NextStepApprovedId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStep_NextStepRejectedId",
                schema: "Workflow",
                table: "ApprovalStep",
                column: "NextStepRejectedId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStep_WorkflowStatusId",
                schema: "Workflow",
                table: "ApprovalStep",
                column: "WorkflowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStep_WorkflowTypeId",
                schema: "Workflow",
                table: "ApprovalStep",
                column: "WorkflowTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStepResult_ApprovalId",
                schema: "Workflow",
                table: "ApprovalStepResult",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStepResult_WorkflowStepResultId",
                schema: "Workflow",
                table: "ApprovalStepResult",
                column: "WorkflowStepResultId");

            migrationBuilder.CreateIndex(
                name: "IX_StepResult_NextStepApprovedId",
                schema: "Workflow",
                table: "StepResult",
                column: "NextStepApprovedId");

            migrationBuilder.CreateIndex(
                name: "IX_StepResult_NextStepRejectedId",
                schema: "Workflow",
                table: "StepResult",
                column: "NextStepRejectedId");

            migrationBuilder.CreateIndex(
                name: "IX_StepResult_StepOriginId",
                schema: "Workflow",
                table: "StepResult",
                column: "StepOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_StepResult_WorkflowId",
                schema: "Workflow",
                table: "StepResult",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_StepResult_WorkflowStatusId",
                schema: "Workflow",
                table: "StepResult",
                column: "WorkflowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_CurrentStepId",
                schema: "Workflow",
                table: "Workflow",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_WorkflowTypeId",
                schema: "Workflow",
                table: "Workflow",
                column: "WorkflowTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalStepResult_StepResult_WorkflowStepResultId",
                schema: "Workflow",
                table: "ApprovalStepResult",
                column: "WorkflowStepResultId",
                principalSchema: "Workflow",
                principalTable: "StepResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StepResult_Workflow_WorkflowId",
                schema: "Workflow",
                table: "StepResult",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StepResult_ApprovalStep_StepOriginId",
                schema: "Workflow",
                table: "StepResult");

            migrationBuilder.DropForeignKey(
                name: "FK_StepResult_Status_WorkflowStatusId",
                schema: "Workflow",
                table: "StepResult");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_Type_WorkflowTypeId",
                schema: "Workflow",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_StepResult_CurrentStepId",
                schema: "Workflow",
                table: "Workflow");

            migrationBuilder.DropTable(
                name: "ApprovalRole",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "ApprovalStepResult",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Approval",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "ApprovalStep",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Type",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "StepResult",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Workflow",
                schema: "Workflow");
        }
    }
}
