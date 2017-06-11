CREATE TABLE [dbo].[wc_tournament] (
    [competition_code]    INT      NULL,
    [tournament_code]     INT      NOT NULL,
    [year]                INT      NOT NULL,
    [name]                VARCHAR (50)  NOT NULL,
    [format_code]         INT      NOT NULL,
    [host_team_code]      INT      NULL,
    [co_host_team_code]   INT      NULL,
    [notes]               TEXT          NULL,
    [logo_image]          VARCHAR (100) NULL,
    [qualification_image] VARCHAR (100) NULL,
    CONSTRAINT [PK_wc_tournament] PRIMARY KEY CLUSTERED ([tournament_code] ASC)
);

