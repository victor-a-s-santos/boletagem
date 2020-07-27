using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Migrations
{
    public partial class _1008zbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

                migrationBuilder.InsertData(
                table: "Bank",
                schema: "Ticket",
                columns: new[] { "Id", "Code", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1,   "001", "Banco do Brasil S.A.", "BCO DO BRASIL S.A." },
                    { 73,  "604", "Banco Industrial do Brasil S.A.", "BCO INDUSTRIAL DO BRASIL S.A." },
                    { 72,  "600", "Banco Luso Brasileiro S.A.", "BCO LUSO BRASILEIRO S.A." },
                    { 71,  "505", "Banco Credit Suisse (Brasil) S.A.", "BCO CREDIT SUISSE S.A." },
                    { 70,  "487", "Deutsche Bank S.A. - Banco Alemão", "DEUTSCHE BANK S.A.BCO ALEMAO" },
                    { 69,  "477", "Citibank N.A.", "CITIBANK N.A." },
                    { 68,  "473", "Banco Caixa Geral - Brasil S.A.", "BCO CAIXA GERAL BRASIL S.A." },
                    { 67,  "464", "Banco Sumitomo Mitsui Brasileiro S.A.", "BCO SUMITOMO MITSUI BRASIL S.A." },
                    { 66,  "456", "Banco MUFG Brasil S.A.", "BCO MUFG BRASIL S.A." },
                    { 65,  "422", "Banco Safra S.A.", "BCO SAFRA S.A." },
                    { 64,  "412", "Banco Capital S. A.", "BCO CAPITAL S.A." },
                    { 63,  "394", "Banco Bradesco Financiamentos S.A.", "BCO BRADESCO FINANC. S.A." },
                    { 62,  "389", "Banco Mercantil do Brasil S.A.", "BCO MERCANTIL DO BRASIL S.A." },
                    { 61,  "376", "Banco J. P. Morgan S. A.", "BCO J.P. MORGAN S.A." },
                    { 60,  "366", "Banco Société Générale Brasil S.A.", "BCO SOCIETE GENERALE BRASIL" },
                    { 59,  "341", "Itaú Unibanco  S.A.", "ITAÚ UNIBANCO S.A." },
                    { 58,  "320", "China Construction Bank (Brasil) Banco Múltiplo S/A", "BCO CCB BRASIL S.A." },
                    { 57,  "318", "Banco BMG S.A.", "BCO BMG S.A." },
                    { 56,  "300", "Banco de la Nacion Argentina", "BCO LA NACION ARGENTINA" },
                    { 55,  "279", "Cooperativa de Crédito Rural de Primavera do Leste", "CCR DE PRIMAVERA DO LESTE" },
                    { 54,  "266", "Banco Cédula S.A.", "BCO CEDULA S.A." },
                    { 53,  "265", "Banco Fator S.A.", "BCO FATOR S.A." },
                    { 74,  "610", "Banco VR S.A.", "BCO VR S.A." },
                    { 52,  "254", "Parana Banco S. A.", "PARANA BCO S.A." },
                    { 75,  "611", "Banco Paulista S.A.", "BCO PAULISTA S.A." },
                    { 77,  "613", "Omni Banco S.A.", "OMNI BANCO S.A." },
                    { 98,  "755", "Bank of America Merrill Lynch Banco Múltiplo S.A.", "BOFA MERRILL LYNCH BM S.A." },
                    { 97,  "753", "Novo Banco Continental S.A. - Banco Múltiplo", "NOVO BCO CONTINENTAL S.A. - BM" },
                    { 96,  "752", "Banco BNP Paribas Brasil S.A.", "BCO BNP PARIBAS BRASIL S A" },
                    { 95,  "748", "Banco Cooperativo Sicredi S. A.", "BCO COOPERATIVO SICREDI S.A." },
                    { 94,  "747", "Banco Rabobank International Brasil S.A.", "BCO RABOBANK INTL BRASIL S.A." },
                    { 93,  "746", "Banco Modal S.A.", "BCO MODAL S.A." },
                    { 92,  "745", "Banco Citibank S.A.", "BCO CITIBANK S.A." },
                    { 91,  "743", "Banco Semear S.A.", "BANCO SEMEAR" },
                    { 90,  "741", "Banco Ribeirão Preto S.A.", "BCO RIBEIRAO PRETO S.A." },
                    { 89,  "739", "Banco Cetelem S.A.", "BCO CETELEM S.A." },
                    { 88,  "707", "Banco Daycoval S.A.", "BCO DAYCOVAL S.A" },
                    { 87,  "655", "Banco Votorantim S.A.", "BCO VOTORANTIM S.A." },
                    { 86,  "654", "Banco A. J. Renner S.A.", "BCO A.J. RENNER S.A." },
                    { 85,  "653", "Banco Indusval S. A.", "BCO INDUSVAL S.A." },
                    { 84,  "652", "Itaú Unibanco Holding S.A.", "ITAÚ UNIBANCO HOLDING S.A." },
                    { 83,  "643", "Banco Pine S.A.", "BCO PINE S.A." },
                    { 82,  "637", "Banco Sofisa S. A.", "BCO SOFISA S.A." },
                    { 81,  "634", "Banco Triângulo S.A.", "BCO TRIANGULO S.A." },
                    { 80,  "633", "Banco Rendimento S.A.", "BCO RENDIMENTO S.A." },
                    { 79,  "626", "Banco Ficsa S. A.", "BCO FICSA S.A." },
                    { 78,  "623", "Banco Pan S.A.", "BANCO PAN" },
                    { 76,  "612", "Banco Guanabara S.A.", "BCO GUANABARA S.A." },
                    { 51,  "249", "Banco Investcred Unibanco S.A.", "BANCO INVESTCRED UNIBANCO S.A." },
                    { 50,  "246", "Banco ABC Brasil S.A.", "BCO ABC BRASIL S.A." },
                    { 49,  "243", "Banco Máxima S.A.", "BCO MÁXIMA S.A." },
                    { 22,  "084", "Uniprime Norte do Paraná - Cooperativa de Crédito Ltda.", "UNIPRIME NORTE DO PARANÁ - CC" },
                    { 21,  "083", "Banco da China Brasil S.A.", "BCO DA CHINA BRASIL S.A." },
                    { 20,  "077", "Banco Inter S.A.", "BANCO INTER" },
                    { 19,  "074", "Banco J. Safra S.A.", "BCO. J.SAFRA S.A." },
                    { 18,  "070", "Banco de Brasília S.A.", "BRB - BCO DE BRASILIA S.A." },
                    { 17,  "069", "Banco Crefisa S.A.", "BCO CREFISA S.A." },
                    { 16,  "065", "Banco AndBank (Brasil) S.A.", "BCO ANDBANK S.A." },
                    { 15,  "063", "Banco Bradescard S.A.", "BANCO BRADESCARD" },
                    { 14,  "062", "Hipercard Banco Múltiplo S.A.", "HIPERCARD BM S.A." },
                    { 13,  "047", "Banco do Estado de Sergipe S.A.", "BCO DO EST. DE SE S.A." },
                    { 12,  "041", "Banco do Estado do Rio Grande do Sul S.A.", "BCO DO ESTADO DO RS S.A." },
                    { 11,  "037", "Banco do Estado do Pará S.A.", "BCO DO EST. DO PA S.A." },
                    { 10,  "036", "Banco Bradesco BBI S.A.", "BCO BBI S.A." },
                    { 9,   "033", "Banco Santander (Brasil) S. A.", "BCO SANTANDER (BRASIL) S.A." },
                    { 8,   "025", "Banco Alfa S.A.", "BCO ALFA S.A." },
                    { 7,   "021", "Banestes S.A. Banco do Estado do Espírito Santo", "BCO BANESTES S.A." },
                    { 6,   "018", "Banco Tricury S.A.", "BCO TRICURY S.A." },
                    { 5,   "017", "BNY Mellon Banco S.A.", "BNY MELLON BCO S.A." },
                    { 4,   "010", "Credicoamo Crédito Rural Cooperativa", "CREDICOAMO" },
                    { 3,   "004", "Banco do Nordeste do Brasil S.A.", "BCO DO NORDESTE DO BRASIL S.A." },
                    { 2,   "003", "Banco da Amazônia S.A.", "BCO DA AMAZONIA S.A." },
                    { 23,  "085", "Cooperativa Central de Crédito - Ailos", "COOP CENTRAL AILOS" },
                    { 24,  "089", "Cooperativa de Crédito Rural da Região da Mogiana", "CCR REG MOGIANA" },
                    { 25,  "094", "Banco Finaxis S.A.", "BANCO FINAXIS" },
                    { 26,  "097", "Cooperativa Central de Crédito Noroeste Brasileiro Ltda - CentralCredi", "CCC NOROESTE BRASILEIRO LTDA." },
                    { 48,  "241", "Banco Clássico S.A.", "BCO CLASSICO S.A." },
                    { 47,  "237", "Banco Bradesco S.A.", "BCO BRADESCO S.A." },
                    { 46,  "224", "Banco Fibra S.A.", "BCO FIBRA S.A." },
                    { 45,  "222", "Banco Credit Agrícole Brasil S.A.", "BCO CRÉDIT AGRICOLE BR S.A." },
                    { 44,  "218", "Banco BS2 S.A.", "BCO BS2 S.A." },
                    { 43,  "217", "Banco John Deere S.A.", "BANCO JOHN DEERE S.A." },
                    { 42,  "213", "Banco Arbi S.A.", "BCO ARBI S.A." },
                    { 41,  "212", "Banco Original S.A.", "BANCO ORIGINAL" },
                    { 40,  "208", "Banco BTG Pactual S.A.", "BANCO BTG PACTUAL S.A." },
                    { 39,  "204", "Banco Bradesco Cartões S.A.", "BCO BRADESCO CARTOES S.A." },
                    { 99,  "756", "Banco Cooperativo do Brasil S/A - Bancoob", "BANCOOB" },
                    { 38,  "136", "Confederação Nacional das Cooperativas Centrais Unicred Ltda – Unicred do Brasil", "CONF NAC COOP CENTRAIS UNICRED" },
                    { 36,  "125", "Brasil Plural S.A. Banco Múltiplo", "BRASIL PLURAL S.A. BCO." },
                    { 35,  "124", "Banco Woori Bank do Brasil S.A.", "BCO WOORI BANK DO BRASIL S.A." },
                    { 34,  "122", "Banco Bradesco BERJ S.A.", "BCO BRADESCO BERJ S.A." },
                    { 33,  "121", "Banco Agibank S.A.", "BCO AGIBANK S.A." },
                    { 32,  "120", "Banco Rodobens SA", "BCO RODOBENS S.A." },
                    { 31,  "114", "Central Cooperativa de Crédito no Estado do Espírito Santo - CECOOP", "CENTRAL COOPERATIVA DE CRÉDITO NO ESTADO DO ESPÍRITO SANTO" },
                    { 30,  "107", "Banco Bocom BBM S.A.", "BCO BOCOM BBM S.A." },
                    { 29,  "104", "Caixa Econômica Federal", "CAIXA ECONOMICA FEDERAL" },
                    { 28,  "099", "Uniprime Central – Central Interestadual de Cooperativas de Crédito Ltda.", "UNIPRIME CENTRAL CCC LTDA." },
                    { 27,  "098", "Credialiança Cooperativa de Crédito Rural", "CREDIALIANÇA CCR" },
                    { 37,  "133", "Confederação Nacional das Cooperativas Centrais de Crédito e Economia Familiar e", "CRESOL CONFEDERAÇÃO" },
                    { 100, "757", "Banco Keb Hana do Brasil S. A.", "BCO KEB HANA DO BRASIL S.A." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank",
                schema: "Ticket");
        }
    }
}
