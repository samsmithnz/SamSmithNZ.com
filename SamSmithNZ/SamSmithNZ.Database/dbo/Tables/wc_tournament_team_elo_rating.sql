CREATE TABLE [dbo].[wc_tournament_team_elo_rating] (
    [tournament_code] INT NULL,
    [team_code]       INT NULL,
    [elo_rating]   INT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_tournament_team_elo_rating]
    ON [dbo].[wc_tournament_team_elo_rating]([tournament_code] ASC);

