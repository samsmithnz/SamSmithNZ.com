CREATE TABLE [dbo].[problem_search_parameters] (
    [record_id]    UNIQUEIDENTIFIER NOT NULL,
    [search_text]  VARCHAR (100)    NULL,
    [last_updated] DATETIME         NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_problem_search_parameters]
    ON [dbo].[problem_search_parameters]([record_id] ASC);

