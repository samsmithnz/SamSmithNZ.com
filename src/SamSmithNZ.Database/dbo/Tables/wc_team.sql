CREATE TABLE [dbo].[wc_team] (
    [team_code]   INT     NOT NULL,
    [team_name]   VARCHAR (200) NULL,
    [flag_name]   VARCHAR (50) NULL,
    [region_code] INT     NULL,
    CONSTRAINT [PK_wc_team] PRIMARY KEY CLUSTERED ([team_code] ASC)
);

