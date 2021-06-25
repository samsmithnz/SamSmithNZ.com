CREATE TABLE [dbo].[wc_tournament_format_playoff_setup] (
    [tournament_code]                   INT NOT NULL,
    [round_code]                      VARCHAR(10) NOT NULL,
    [game_number]                       INT NOT NULL,
    [team_1_prereq]                     VARCHAR(50) NULL,
    [team_2_prereq]                     VARCHAR(50) NULL,
    CONSTRAINT [PK_wc_tournament_format_playoff_setup] PRIMARY KEY CLUSTERED ([tournament_code], [round_code], [game_number])
);

