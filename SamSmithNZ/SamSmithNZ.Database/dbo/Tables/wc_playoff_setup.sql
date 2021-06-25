CREATE TABLE [dbo].[wc_playoff_setup] (
    [tournament_code]             INT     NOT NULL,
    [round_code]                  VARCHAR(10)     NOT NULL,
    [game_number]                 INT     NOT NULL,
    [team_1_prereq]               VARCHAR (50) NULL,
    [team_2_prereq]               VARCHAR (50) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_playoff_setup]
    ON [dbo].[wc_playoff_setup]([tournament_code], [round_code], [game_number]);