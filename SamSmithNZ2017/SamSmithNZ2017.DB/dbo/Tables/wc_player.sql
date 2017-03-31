CREATE TABLE [dbo].[wc_player] (
    [player_code]       SMALLINT      NOT NULL,
    [team_code]         SMALLINT      NOT NULL,
    [tournament_code]   SMALLINT      NULL,
    [player_name]       VARCHAR (200) NOT NULL,
    [number]            SMALLINT      NOT NULL,
    [position]          VARCHAR (50)  NOT NULL,
    [date_of_birth]     DATETIME      NULL,
    [is_captain]        BIT           NOT NULL,
    [club_name]         VARCHAR (200) NULL,
    [club_country_name] VARCHAR (200) NULL,
    CONSTRAINT [PK_wc_player] PRIMARY KEY CLUSTERED ([player_code] ASC)
);

