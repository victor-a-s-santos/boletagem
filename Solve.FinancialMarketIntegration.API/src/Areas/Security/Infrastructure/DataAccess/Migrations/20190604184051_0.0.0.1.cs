using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Migrations
{
    public partial class _0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    CreationUser = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ChangeUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    LastPasswordChangedDate = table.Column<DateTimeOffset>(nullable: true),
                    Email = table.Column<string>(maxLength: 70, nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    LockoutEndDateUtc = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<short>(nullable: false),
                    UserName = table.Column<string>(maxLength: 25, nullable: true),
                    UserDocument = table.Column<string>(nullable: true),
                    PasswordExpirationDate = table.Column<DateTimeOffset>(nullable: false),
                    Salt = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordResetHash = table.Column<string>(nullable: true),
                    LastAccessToken = table.Column<string>(maxLength: 250, nullable: true),
                    LastAccessDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupRole",
                schema: "Security",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRole", x => new { x.GroupId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_GroupRole_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Security",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Security",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPassword",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPassword_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Group",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3453), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3464), new TimeSpan(0, -3, 0, 0, 0)), null, "Gestores" },
                    { 2, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3483), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3487), new TimeSpan(0, -3, 0, 0, 0)), null, "Adm. Fiduci" },
                    { 3, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3493), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3498), new TimeSpan(0, -3, 0, 0, 0)), null, "Custodia" },
                    { 4, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3503), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3508), new TimeSpan(0, -3, 0, 0, 0)), null, "Liquidação (Open)" },
                    { 5, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3513), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3517), new TimeSpan(0, -3, 0, 0, 0)), null, "Middle Adm. Fiduci" },
                    { 6, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3575), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(3580), new TimeSpan(0, -3, 0, 0, 0)), null, "Administrator do Sistema" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
                values: new object[,]
                {
                    { 17, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4948), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4953), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar histórico de aprovação" },
                    { 18, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4957), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4962), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Cotas" },
                    { 19, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4966), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4970), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de RF - Título Privado" },
                    { 20, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4975), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4979), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de RF - Título Público" },
                    { 21, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4984), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4988), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Futuros" },
                    { 22, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4992), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4997), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Margem" },
                    { 25, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5019), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5023), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Renda Variável" },
                    { 24, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5010), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5014), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Termo de Moeda" },
                    { 27, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4940), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4944), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Pendente Aprovação do Gestor" },
                    { 26, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5027), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5032), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Swap-CETIP" },
                    { 28, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5036), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5041), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar Usuário Master" },
                    { 29, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5045), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5050), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar Usuário Subordinado" },
                    { 23, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5001), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(5006), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Compromissada de Compra" },
                    { 16, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4930), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4934), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Em Liquidação (Custódia)" },
                    { 15, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4921), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4926), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Aguardando Liquidação (Custódia)" },
                    { 14, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4912), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4917), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Em Liquidação (Middle ADM Fiduci)" },
                    { 1, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4775), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4785), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Cotas" },
                    { 2, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4803), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4808), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de RF - Título Privado" },
                    { 3, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4813), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4817), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de RF - Título Público" },
                    { 5, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4830), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4835), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Margem" },
                    { 6, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4839), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4843), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Compromissada de Compra" },
                    { 7, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4848), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4853), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Termo de Moeda" },
                    { 4, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4821), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4826), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Futuros" },
                    { 9, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4866), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4870), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Swap-CETIP" },
                    { 10, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4875), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4879), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Pendente de Aprovação pela ADM" },
                    { 11, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4886), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4890), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Aguardando Liquidação (Open)" },
                    { 12, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4895), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4899), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Em Liquidação (Open)" },
                    { 13, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4904), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4908), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Aguardando Liquidação (Middle ADM Fiduci)" },
                    { 8, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4857), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(4861), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Renda Variável" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Email", "EmailConfirmed", "LastAccessDate", "LastAccessToken", "LastPasswordChangedDate", "LockoutEnabled", "LockoutEndDateUtc", "Name", "PasswordExpirationDate", "PasswordHash", "PasswordResetHash", "PhoneNumber", "PhoneNumberConfirmed", "Salt", "UserDocument", "UserName" },
                values: new object[,]
                {
                    { 6, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1568), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1572), new TimeSpan(0, -3, 0, 0, 0)), null, "admin@admin.com.br", true, null, null, null, false, null, "Administrator", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1578), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTF==", null, "admin" },
                    { 1, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 894, DateTimeKind.Unspecified).AddTicks(6604), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 895, DateTimeKind.Unspecified).AddTicks(7502), new TimeSpan(0, -3, 0, 0, 0)), null, "estelle_ankunding36@hotmail.com", true, null, null, null, false, null, "Estelle Ankunding (Gestor)", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1060), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTA==", null, "gestor" },
                    { 2, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1431), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1453), new TimeSpan(0, -3, 0, 0, 0)), null, "dee_rippin@yahoo.com", true, null, null, null, false, null, "Dee Rippin (Adm Fiduci)", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1503), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTB==", null, "admin.fiduci" },
                    { 3, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1517), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1523), new TimeSpan(0, -3, 0, 0, 0)), null, "garrett_dickinson37@hotmail.com", true, null, null, null, false, null, "Garrett Dickinson (Custódia)", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1529), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTC==", null, "custodia" },
                    { 4, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1536), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1540), new TimeSpan(0, -3, 0, 0, 0)), null, "zula_stokes37@gmail.com", true, null, null, null, false, null, "Zula Stokes V (Open)", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1546), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTD==", null, "open" },
                    { 5, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1551), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1556), new TimeSpan(0, -3, 0, 0, 0)), null, "maia_spinka@gmail.com", true, null, null, null, false, null, "Maia Spinka (Middle ADM)", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1562), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTE==", null, "middle.adm" },
                    { 7, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1583), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1588), new TimeSpan(0, -3, 0, 0, 0)), null, "bart_larson@yahoo.com", true, null, null, null, false, null, "Ms. Bart Larson (Gestor)", new DateTimeOffset(new DateTime(2020, 6, 4, 15, 40, 50, 896, DateTimeKind.Unspecified).AddTicks(1593), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTG==", null, "gestor2" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "GroupRole",
                columns: new[] { "GroupId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 21 },
                    { 2, 21 },
                    { 3, 21 },
                    { 4, 21 },
                    { 1, 22 },
                    { 2, 22 },
                    { 3, 22 },
                    { 4, 22 },
                    { 5, 22 },
                    { 1, 23 },
                    { 2, 23 },
                    { 3, 23 },
                    { 4, 23 },
                    { 5, 23 },
                    { 1, 24 },
                    { 2, 24 },
                    { 3, 24 },
                    { 1, 29 },
                    { 6, 28 },
                    { 5, 26 },
                    { 4, 26 },
                    { 3, 26 },
                    { 2, 26 },
                    { 5, 20 },
                    { 1, 26 },
                    { 4, 25 },
                    { 3, 25 },
                    { 2, 25 },
                    { 1, 25 },
                    { 5, 24 },
                    { 4, 24 },
                    { 5, 25 },
                    { 4, 20 },
                    { 5, 21 },
                    { 2, 20 },
                    { 3, 15 },
                    { 5, 14 },
                    { 5, 13 },
                    { 3, 20 },
                    { 4, 11 },
                    { 2, 10 },
                    { 3, 16 },
                    { 1, 9 },
                    { 1, 7 },
                    { 1, 6 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 3, 8 },
                    { 1, 27 },
                    { 4, 12 },
                    { 2, 17 },
                    { 1, 20 },
                    { 1, 17 },
                    { 4, 19 },
                    { 3, 19 },
                    { 2, 19 },
                    { 1, 19 },
                    { 5, 18 },
                    { 5, 19 },
                    { 3, 18 },
                    { 2, 18 },
                    { 1, 18 },
                    { 5, 17 },
                    { 4, 17 },
                    { 3, 17 },
                    { 4, 18 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserGroup",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 6, 6 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserPassword",
                columns: new[] { "Id", "CreationDate", "PasswordHash", "UserId" },
                values: new object[,]
                {
                    { 6, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2947), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 6 },
                    { 5, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2945), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 5 },
                    { 2, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2922), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 2 },
                    { 3, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2940), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 3 },
                    { 1, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2139), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 1 },
                    { 4, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2943), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 4 },
                    { 7, new DateTime(2019, 6, 4, 15, 40, 50, 896, DateTimeKind.Local).AddTicks(2949), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_Name",
                schema: "Security",
                table: "Group",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupRole_RoleId",
                schema: "Security",
                table: "GroupRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                schema: "Security",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "Security",
                table: "User",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                schema: "Security",
                table: "UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPassword_UserId",
                schema: "Security",
                table: "UserPassword",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserGroup",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserPassword",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");
        }
    }
}
