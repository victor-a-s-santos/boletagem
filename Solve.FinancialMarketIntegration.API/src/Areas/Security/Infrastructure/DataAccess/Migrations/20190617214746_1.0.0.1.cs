using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Migrations
{
    public partial class _1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Security",
                table: "Group",
                columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
                values: new object[,]
                {
                     //{ 1, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1150), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1170), new TimeSpan(0, -3, 0, 0, 0)), null, "Gestores Master" },
                     //{ 2, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1190), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1190), new TimeSpan(0, -3, 0, 0, 0)), null, "Adm. Fiduci" },
                     //{ 3, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1200), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1200), new TimeSpan(0, -3, 0, 0, 0)), null, "Custodia" },
                     //{ 4, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1210), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1210), new TimeSpan(0, -3, 0, 0, 0)), null, "Liquidação (Open)" },
                     //{ 5, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1220), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1220), new TimeSpan(0, -3, 0, 0, 0)), null, "Middle Adm. Fiduci" },
                     //{ 6, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1230), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1230), new TimeSpan(0, -3, 0, 0, 0)), null, "Administrator do Sistema" },
                     { 7, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1240), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(1240), new TimeSpan(0, -3, 0, 0, 0)), null, "Gestores Boleta" }
                });

            // migrationBuilder.InsertData(
            //     schema: "Security",
            //     table: "Role",
            //     columns: new[] { "Id", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Name" },
            //     values: new object[,]
            //     {
            //         { 17, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4560), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4570), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar histórico de aprovação" },
            //         { 18, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4570), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4580), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Cotas" },
            //         { 19, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4580), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4580), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de RF - Título Privado" },
            //         { 20, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4590), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4590), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de RF - Título Público" },
            //         { 21, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4600), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4600), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Futuros" },
            //         { 22, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4610), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4610), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Margem" },
            //         { 25, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4640), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4640), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Renda Variável" },
            //         { 24, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4630), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4630), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Termo de Moeda" },
            //         { 27, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4550), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4560), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Pendente Aprovação do Gestor" },
            //         { 26, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4650), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4650), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Swap-CETIP" },
            //         { 28, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4660), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4660), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar Usuário Master" },
            //         { 29, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4810), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4820), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar Usuário Subordinado" },
            //         { 23, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, -3, 0, 0, 0)), null, "Visualizar monitor de Compromissada de Compra" },
            //         { 16, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4540), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4550), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Em Liquidação (Custódia)" },
            //         { 15, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4530), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4540), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Aguardando Liquidação (Custódia)" },
            //         { 14, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4520), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4530), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Em Liquidação (Middle ADM Fiduci)" },
            //         { 1, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4360), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4380), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Cotas" },
            //         { 2, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4400), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4410), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de RF - Título Privado" },
            //         { 3, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4420), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4420), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de RF - Título Público" },
            //         { 5, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Margem" },
            //         { 6, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Compromissada de Compra" },
            //         { 7, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Termo de Moeda" },
            //         { 4, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4430), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4430), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Futuros" },
            //         { 9, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Swap-CETIP" },
            //         { 10, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4490), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Pendente de Aprovação pela ADM" },
            //         { 11, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4490), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4500), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Aguardando Liquidação (Open)" },
            //         { 12, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4500), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4510), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Em Liquidação (Open)" },
            //         { 13, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4510), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4520), new TimeSpan(0, -3, 0, 0, 0)), null, "Aprovar boletas Aguardando Liquidação (Middle ADM Fiduci)" },
            //         { 8, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, -3, 0, 0, 0)), null, "Criar boletas de Renda Variável" }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Security",
            //     table: "User",
            //     columns: new[] { "Id", "AccessFailedCount", "Active", "ChangeDate", "ChangeUser", "CreationDate", "CreationUser", "Email", "EmailConfirmed", "LastAccessDate", "LastAccessToken", "LastPasswordChangedDate", "LockoutEnabled", "LockoutEndDateUtc", "Name", "PasswordExpirationDate", "PasswordHash", "PasswordResetHash", "PhoneNumber", "PhoneNumberConfirmed", "Salt", "UserDocument", "UserName" },
            //     values: new object[,]
            //     {
            //         { 6, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7740), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7740), new TimeSpan(0, -3, 0, 0, 0)), null, "admin@admin.com.br", true, null, null, null, false, null, "Administrator", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7750), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTF==", null, "admin" },
            //         { 1, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 736, DateTimeKind.Unspecified).AddTicks(2800), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(1000), new TimeSpan(0, -3, 0, 0, 0)), null, "estelle_ankunding36@hotmail.com", true, null, null, null, false, null, "Estelle Ankunding (Gestor)", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(6980), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTA==", null, "gestor" },
            //         { 2, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7590), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7620), new TimeSpan(0, -3, 0, 0, 0)), null, "dee_rippin@yahoo.com", true, null, null, null, false, null, "Dee Rippin (Adm Fiduci)", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7670), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTB==", null, "admin.fiduci" },
            //         { 3, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7690), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7690), new TimeSpan(0, -3, 0, 0, 0)), null, "garrett_dickinson37@hotmail.com", true, null, null, null, false, null, "Garrett Dickinson (Custódia)", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7700), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTC==", null, "custodia" },
            //         { 4, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7700), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7710), new TimeSpan(0, -3, 0, 0, 0)), null, "zula_stokes37@gmail.com", true, null, null, null, false, null, "Zula Stokes V (Open)", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7720), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTD==", null, "open" },
            //         { 5, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7720), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7730), new TimeSpan(0, -3, 0, 0, 0)), null, "maia_spinka@gmail.com", true, null, null, null, false, null, "Maia Spinka (Middle ADM)", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7730), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTE==", null, "middle.adm" },
            //         { 7, (short)0, true, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7760), new TimeSpan(0, -3, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7760), new TimeSpan(0, -3, 0, 0, 0)), null, "bart_larson@yahoo.com", true, null, null, null, false, null, "Ms. Bart Larson (Gestor)", new DateTimeOffset(new DateTime(2020, 6, 17, 18, 47, 45, 747, DateTimeKind.Unspecified).AddTicks(7770), new TimeSpan(0, -3, 0, 0, 0)), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", null, null, true, "IHxey3JXmJa0gWCXqUxvTG==", null, "gestor2" }
            //     });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "GroupRole",
                columns: new[] { "GroupId", "RoleId" },
                values: new object[,]
                {
                     //{ 1, 1 },
                     //{ 4, 23 },
                     //{ 3, 23 },
                     //{ 2, 23 },
                     //{ 1, 23 },
                     { 7, 22 },
                     //{ 5, 22 },
                     //{ 4, 22 },
                     //{ 3, 22 },
                     //{ 5, 23 },
                     //{ 2, 22 },
                     { 7, 21 },
                     //{ 5, 21 },
                     //{ 4, 21 },
                     //{ 3, 21 },
                     //{ 1, 21 },
                     { 7, 20 },
                     //{ 5, 20 },
                     //{ 4, 20 },
                     //{ 1, 22 },
                     //{ 3, 20 },
                     { 7, 23 },
                     //{ 2, 24 },
                     //{ 1, 29 },
                     //{ 6, 28 },
                     { 7, 26 },
                     //{ 5, 26 },
                     //{ 4, 26 },
                     //{ 3, 26 },
                     //{ 2, 26 },
                     //{ 1, 26 },
                     //{ 1, 24 },
                     { 7, 25 },
                     //{ 4, 25 },
                     //{ 3, 25 },
                     //{ 2, 25 },
                     //{ 1, 25 },
                     { 7, 24 },
                     //{ 5, 24 },
                     //{ 4, 24 },
                     //{ 3, 24 },
                     //{ 5, 25 },
                     //{ 2, 20 },
                     //{ 2, 21 },
                     { 7, 19 },
                     //{ 4, 12 },
                     //{ 4, 11 },
                     //{ 2, 10 },
                     { 7, 9 },
                     //{ 1, 20 },
                     //{ 3, 8 },
                     { 7, 7 },
                     //{ 1, 7 },
                     { 7, 6 },
                     //{ 1, 6 },
                     { 7, 5 },
                     //{ 1, 5 },
                     { 7, 4 },
                     //{ 1, 4 },
                     { 7, 3 },
                     //{ 1, 3 },
                     { 7, 2 },
                     //{ 1, 2 },
                     { 7, 1 },
                     //{ 5, 13 },
                     //{ 5, 14 },
                     //{ 1, 9 },
                     //{ 3, 16 },
                     //{ 5, 19 },
                     //{ 4, 19 },
                     //{ 3, 19 },
                     //{ 3, 15 },
                     //{ 1, 19 },
                     { 7, 18 },
                     //{ 5, 18 },
                     //{ 4, 18 },
                     //{ 3, 18 },
                     //{ 2, 19 },
                     //{ 1, 18 },
                     //{ 5, 17 },
                     //{ 4, 17 },
                     //{ 3, 17 },
                     //{ 2, 17 },
                     //{ 1, 17 },
                     { 7, 27 },
                     //{ 1, 27 },
                     //{ 2, 18 }
                });

            // migrationBuilder.InsertData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     columns: new[] { "GroupId", "UserId" },
            //     values: new object[,]
            //     {
            //         { 6, 6 },
            //         { 5, 5 },
            //         { 4, 4 },
            //         { 3, 3 },
            //         { 2, 2 },
            //         { 1, 1 },
            //         { 1, 7 }
            //     });

            // migrationBuilder.InsertData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     columns: new[] { "Id", "CreationDate", "PasswordHash", "UserId" },
            //     values: new object[,]
            //     {
            //         { 3, new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Local).AddTicks(190), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 3 },
            //         { 1, new DateTime(2019, 6, 17, 18, 47, 45, 747, DateTimeKind.Local).AddTicks(8720), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 1 },
            //         { 4, new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Local).AddTicks(190), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 4 },
            //         { 5, new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Local).AddTicks(200), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 5 },
            //         { 6, new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Local).AddTicks(200), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 6 },
            //         { 2, new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Local).AddTicks(170), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 2 },
            //         { 7, new DateTime(2019, 6, 17, 18, 47, 45, 748, DateTimeKind.Local).AddTicks(200), "IpfLSPx0mU8z1kXnSrwuZ62rjA7ebAxzirzc7S5JtgU=", 7 }
            //     });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 1 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 2 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 3 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 4 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 5 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 6 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 7 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 9 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 17 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 18 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 19 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 20 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 21 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 22 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 23 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 24 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 25 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 26 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 27 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 1, 29 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 10 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 17 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 18 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 19 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 20 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 21 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 22 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 23 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 24 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 25 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 2, 26 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 8 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 15 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 16 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 17 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 18 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 19 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 20 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 21 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 22 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 23 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 24 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 25 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 3, 26 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 11 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 12 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 17 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 18 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 19 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 20 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 21 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 22 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 23 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 24 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 25 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 4, 26 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 13 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 14 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 17 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 18 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 19 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 20 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 21 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 22 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 23 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 24 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 25 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 5, 26 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "GroupRole",
            //     keyColumns: new[] { "GroupId", "RoleId" },
            //     keyValues: new object[] { 6, 28 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 18 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 19 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 20 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 21 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 22 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 23 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 24 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 25 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 26 });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "GroupRole",
                keyColumns: new[] { "GroupId", "RoleId" },
                keyValues: new object[] { 7, 27 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 1, 1 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 1, 7 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 2, 2 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 3, 3 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 4, 4 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 5, 5 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserGroup",
            //     keyColumns: new[] { "GroupId", "UserId" },
            //     keyValues: new object[] { 6, 6 });

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 6);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "UserPassword",
            //     keyColumn: "Id",
            //     keyValue: 7);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Group",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Group",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Group",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Group",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Group",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Group",
            //     keyColumn: "Id",
            //     keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Group",
                keyColumn: "Id",
                keyValue: 7);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 6);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 7);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 8);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 9);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 10);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 11);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 12);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 13);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 14);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 15);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 16);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 17);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 18);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 19);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 20);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 21);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 22);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 23);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 24);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 25);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 26);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 27);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 28);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "Role",
            //     keyColumn: "Id",
            //     keyValue: 29);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 1);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 2);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 3);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 4);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 5);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 6);

            // migrationBuilder.DeleteData(
            //     schema: "Security",
            //     table: "User",
            //     keyColumn: "Id",
            //     keyValue: 7);
        }
    }
}
