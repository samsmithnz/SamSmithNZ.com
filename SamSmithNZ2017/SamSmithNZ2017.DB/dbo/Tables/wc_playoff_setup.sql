CREATE TABLE [dbo].[wc_playoff_setup] (
    [tournament_code]                 SMALLINT     NOT NULL,
    [game_code]                       SMALLINT     NOT NULL,
    [team_1_round_code]               VARCHAR (10) NOT NULL,
    [team_1_round_position_advancing] SMALLINT     NOT NULL,
    [team_2_round_code]               VARCHAR (10) NOT NULL,
    [team_2_round_position_advancing] SMALLINT     NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_playoff_setup]
    ON [dbo].[wc_playoff_setup]([tournament_code] ASC);

