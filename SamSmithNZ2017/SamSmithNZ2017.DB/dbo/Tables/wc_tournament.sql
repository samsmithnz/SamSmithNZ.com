CREATE TABLE [dbo].[wc_tournament] (
    [competition_code]    SMALLINT      NULL,
    [tournament_code]     SMALLINT      NOT NULL,
    [year]                SMALLINT      NOT NULL,
    [name]                VARCHAR (50)  NOT NULL,
    [format_code]         SMALLINT      NOT NULL,
    [host_team_code]      SMALLINT      NULL,
    [co_host_team_code]   SMALLINT      NULL,
    [notes]               TEXT          NULL,
    [logo_image]          VARCHAR (100) NULL,
    [qualification_image] VARCHAR (100) NULL,
    CONSTRAINT [PK_wc_tournament] PRIMARY KEY CLUSTERED ([tournament_code] ASC)
);

