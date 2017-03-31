CREATE TABLE [dbo].[TabStats_old] (
    [StatKey]   SMALLINT        NULL,
    [StatName]  VARCHAR (50)    NULL,
    [StatText]  VARCHAR (50)    NULL,
    [StatValue] DECIMAL (10, 2) NULL,
    [StatUnit]  VARCHAR (50)    NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_TabStats_old]
    ON [dbo].[TabStats_old]([StatKey] ASC);

