CREATE TABLE [dbo].[tab_tuning] (
    [tuning_code] INT     NULL,
    [tuning_name] VARCHAR (50) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_tab_tuning]
    ON [dbo].[tab_tuning]([tuning_code] ASC);

