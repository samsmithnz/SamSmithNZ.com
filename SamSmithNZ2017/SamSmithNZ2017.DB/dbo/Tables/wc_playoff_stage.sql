CREATE TABLE [dbo].[wc_playoff_stage] (
    [tournament_code]       SMALLINT NOT NULL,
    [round_number]          SMALLINT NOT NULL,
    [game_code]             SMALLINT NOT NULL,
    [team_1_winner_of_game] SMALLINT NULL,
    [team_2_winner_of_game] SMALLINT NULL,
    [team_1_loser_of_game]  SMALLINT NULL,
    [team_2_loser_of_game]  SMALLINT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_playoff_stage]
    ON [dbo].[wc_playoff_stage]([tournament_code] ASC);

