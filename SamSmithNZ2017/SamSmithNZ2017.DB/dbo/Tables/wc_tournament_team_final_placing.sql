CREATE TABLE [dbo].[wc_tournament_team_final_placing] (
    [tournament_code] SMALLINT NULL,
    [team_code]       SMALLINT NULL,
    [final_placing]   SMALLINT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_tournament_team_final_placing]
    ON [dbo].[wc_tournament_team_final_placing]([tournament_code] ASC);

