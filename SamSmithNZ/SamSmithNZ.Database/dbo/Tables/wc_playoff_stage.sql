CREATE TABLE [dbo].[wc_playoff_stage] (
    [tournament_code]       INT NOT NULL,
    [round_number]          INT NOT NULL,
    [game_code]             INT NOT NULL,
    [team_1_winner_of_game] INT NULL,
    [team_2_winner_of_game] INT NULL,
    [team_1_loser_of_game]  INT NULL,
    [team_2_loser_of_game]  INT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_playoff_stage]
    ON [dbo].[wc_playoff_stage]([tournament_code] ASC);

