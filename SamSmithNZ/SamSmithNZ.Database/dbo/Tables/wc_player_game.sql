CREATE TABLE [dbo].[wc_player_game] (
    [game_code]       INT NOT NULL,
    [team_code]       INT NOT NULL,
    [player_code]     INT NOT NULL,
    [player_time_on]  INT NOT NULL,
    [player_time_off] INT NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_player_game]
    ON [dbo].[wc_player_game]([game_code] ASC);

