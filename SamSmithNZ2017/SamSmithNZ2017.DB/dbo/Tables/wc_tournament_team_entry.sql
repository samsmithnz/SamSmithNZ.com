CREATE TABLE [dbo].[wc_tournament_team_entry] (
    [tournament_code]   INT      NOT NULL,
    [team_code]         INT      NOT NULL,
    [round_code]        VARCHAR (10)  NOT NULL,
    [fifa_ranking]      INT      NULL,
    [coach_name]        VARCHAR (200) NULL,
    [coach_nationality] VARCHAR (200) NULL,
	[is_active]			BIT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_tournament_team_entry]
    ON [dbo].[wc_tournament_team_entry]([tournament_code] ASC);

