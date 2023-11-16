CREATE TABLE [dbo].[wc_tournament_team_chance_to_win] (
    [tournament_code] INT NULL,
    [team_code]       INT NULL,
    [chance_to_win]   DECIMAL(6,4) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_tournament_team_chance_to_win]
    ON [dbo].[wc_tournament_team_chance_to_win]([tournament_code] ASC);

