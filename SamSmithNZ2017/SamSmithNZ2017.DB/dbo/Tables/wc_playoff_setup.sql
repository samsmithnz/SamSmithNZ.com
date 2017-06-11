CREATE TABLE [dbo].[wc_playoff_setup] (
    [tournament_code]                 INT     NOT NULL,
    [game_code]                       INT     NOT NULL,
    [team_1_round_code]               VARCHAR (10) NOT NULL,
    [team_1_round_position_advancing] INT     NOT NULL,
    [team_2_round_code]               VARCHAR (10) NOT NULL,
    [team_2_round_position_advancing] INT     NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_playoff_setup]
    ON [dbo].[wc_playoff_setup]([tournament_code] ASC);

