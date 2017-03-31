CREATE TABLE [dbo].[wc_player_game] (
    [game_code]       SMALLINT NOT NULL,
    [team_code]       SMALLINT NOT NULL,
    [player_code]     SMALLINT NOT NULL,
    [player_time_on]  SMALLINT NOT NULL,
    [player_time_off] SMALLINT NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_player_game]
    ON [dbo].[wc_player_game]([game_code] ASC);

