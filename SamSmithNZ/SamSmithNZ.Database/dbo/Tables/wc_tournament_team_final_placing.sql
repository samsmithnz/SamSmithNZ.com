CREATE TABLE [dbo].[wc_tournament_team_final_placing] (
    [tournament_code] INT NULL,
    [team_code]       INT NULL,
    [final_placing]   INT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_tournament_team_final_placing]
    ON [dbo].[wc_tournament_team_final_placing]([tournament_code] ASC);

