CREATE TABLE [dbo].[wc_tournament_team_entry] (
    [tournament_code]   SMALLINT      NOT NULL,
    [team_code]         SMALLINT      NOT NULL,
    [round_code]        VARCHAR (10)  NOT NULL,
    [fifa_ranking]      SMALLINT      NULL,
    [coach_name]        VARCHAR (200) NULL,
    [coach_nationality] VARCHAR (200) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_tournament_team_entry]
    ON [dbo].[wc_tournament_team_entry]([tournament_code] ASC);

