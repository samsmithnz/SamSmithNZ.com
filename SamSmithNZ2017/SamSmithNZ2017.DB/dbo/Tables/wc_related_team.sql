CREATE TABLE [dbo].[wc_related_team] (
    [team_code]            SMALLINT      NULL,
    [historical_team_code] SMALLINT      NULL,
    [year_team_changed]    SMALLINT      NULL,
    [notes]                VARCHAR (100) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_related_team]
    ON [dbo].[wc_related_team]([team_code] ASC);

