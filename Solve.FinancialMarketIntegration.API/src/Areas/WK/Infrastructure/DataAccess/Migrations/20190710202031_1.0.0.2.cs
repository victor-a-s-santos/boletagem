using Microsoft.EntityFrameworkCore.Migrations;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Migrations
{
    public partial class _1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE 
                        A 
                    SET 
                        A.IsFinished = 1
                    FROM 
                        Workflow.Workflow AS A 
                    INNER JOIN
                        Workflow.StepResult AS B ON B.ID = A.CurrentStepId
                    WHERE
                        B.NextStepApprovedId IS NULL AND
                        B.NextStepRejectedId IS NULL


                    UPDATE 
                        A
                    SET
                        A.WorkflowEndDate = C.CreationDate
                    FROM
                        Ticket.Ticket AS A
                    INNER JOIN
                        Workflow.Workflow AS B ON B.Id = A.WorkflowId
                    INNER JOIN
                        Workflow.StepResult AS C ON C.Id = B.CurrentStepId
                    WHERE
                        C.NextStepApprovedId IS NULL AND
                        C.NextStepRejectedId IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
