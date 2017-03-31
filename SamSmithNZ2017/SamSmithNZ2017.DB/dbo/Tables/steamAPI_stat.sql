CREATE TABLE [dbo].[steamAPI_stat] (
    [batch_id]     UNIQUEIDENTIFIER NULL,
    [api_url]      VARCHAR (1000)   NULL,
    [last_updated] DATETIME         NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_steamAPI_stat]
    ON [dbo].[steamAPI_stat]([batch_id] ASC);

