CREATE TABLE [dbo].[wc_game] (
    [game_code]                SMALLINT      NOT NULL,
    [tournament_code]          SMALLINT      NOT NULL,
    [game_number]              SMALLINT      NOT NULL,
    [game_time]                DATETIME      NULL,
    [round_number]             SMALLINT      NULL,
    [round_code]               VARCHAR (10)  NULL,
    [location]                 VARCHAR (100) NOT NULL,
    [team_1_code]              SMALLINT      NOT NULL,
    [team_2_code]              SMALLINT      NOT NULL,
    [team_1_normal_time_score] SMALLINT      NULL,
    [team_1_extra_time_score]  SMALLINT      NULL,
    [team_1_penalties_score]   SMALLINT      NULL,
    [team_2_normal_time_score] SMALLINT      NULL,
    [team_2_extra_time_score]  SMALLINT      NULL,
    [team_2_penalties_score]   SMALLINT      NULL,
    CONSTRAINT [PK_wc_game] PRIMARY KEY CLUSTERED ([game_code] ASC)
);

