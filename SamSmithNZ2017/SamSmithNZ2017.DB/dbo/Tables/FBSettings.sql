CREATE TABLE [dbo].[FBSettings] (
    [current_week_code] SMALLINT NULL,
    [current_year_code] INT      NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBSettings]
    ON [dbo].[FBSettings]([current_week_code] ASC);

