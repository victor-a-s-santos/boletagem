IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627165810_1.0.0.2')
BEGIN
    UPDATE 
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


                        -- UPDATE 
                        --     A
                        -- SET
                        --     A.WorkflowEndDate = C.CreationDate
                        -- FROM
                        --     Ticket.Ticket AS A
                        -- INNER JOIN
                        --     Workflow.Workflow AS B ON B.Id = A.WorkflowId
                        -- INNER JOIN
                        --     Workflow.StepResult AS C ON C.Id = B.CurrentStepId
                        -- WHERE
                        --     C.NextStepApprovedId IS NULL AND
                        --     C.NextStepRejectedId IS NULL
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190627165810_1.0.0.2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190627165810_1.0.0.2', N'2.2.4-servicing-10062');
END;

GO

