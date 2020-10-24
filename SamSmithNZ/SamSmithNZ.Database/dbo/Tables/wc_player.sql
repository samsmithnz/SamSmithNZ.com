CREATE TABLE [dbo].[wc_player] (
    [player_code]       INT      NOT NULL,
    [team_code]         INT      NOT NULL,
    [tournament_code]   INT      NULL,
    [player_name]       NVARCHAR(200) NOT NULL,
    [number]            INT      NULL,
    [position]          VARCHAR (50)  NOT NULL,
    [date_of_birth]     DATETIME      NULL,
    [is_captain]        BIT           NOT NULL,
    [club_name]         NVARCHAR(200) NULL,
    [club_country_name] NVARCHAR(200) NULL,
    CONSTRAINT [PK_wc_player] PRIMARY KEY CLUSTERED ([player_code] ASC)
);

