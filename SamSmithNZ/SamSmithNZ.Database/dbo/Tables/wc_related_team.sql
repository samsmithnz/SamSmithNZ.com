CREATE TABLE [dbo].[wc_related_team] (
    [team_code]            INT      NULL,
    [historical_team_code] INT      NULL,
    [year_team_changed]    INT      NULL,
    [notes]                VARCHAR (100) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_related_team]
    ON [dbo].[wc_related_team]([team_code] ASC);

