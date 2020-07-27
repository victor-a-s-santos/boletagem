using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _1002WfStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Workflow.Status SET Name = 'Aguardando envio/recebimento da TED', ChangeDate = GETDATE(), ChangeUser = 'admin' WHERE Id = 10 AND Name = 'Aguardando Liquidação Middle'
                UPDATE Workflow.Status SET Name = 'Em Liquidação TED', ChangeDate = GETDATE(), ChangeUser = 'admin' WHERE Id = 11 AND Name = 'Em Liquidação Middle'
                UPDATE Workflow.Status SET Name = 'Cancelada TED', ChangeDate = GETDATE(), ChangeUser = 'admin' WHERE Id = 12 AND Name = 'Cancelada Middle'
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Workflow.Status SET Name = 'Aguardando Liquidação Middle' WHERE Id = 10
                UPDATE Workflow.Status SET Name = 'Em Liquidação Middle' WHERE Id = 11
                UPDATE Workflow.Status SET Name = 'Cancelada Middle' WHERE Id = 12
            ");
        }
    }
}
