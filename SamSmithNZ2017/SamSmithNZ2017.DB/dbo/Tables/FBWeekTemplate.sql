CREATE TABLE [dbo].[FBWeekTemplate] (
    [record_id]         UNIQUEIDENTIFIER NULL,
    [year_code]         SMALLINT         NULL,
    [week_code]         SMALLINT         NULL,
    [home_team_code]    SMALLINT         NULL,
    [away_team_code]    SMALLINT         NULL,
    [fav_team_code]     SMALLINT         NULL,
    [game_time]         SMALLDATETIME    NULL,
    [spread]            DECIMAL (18, 1)  NULL,
    [home_team_result]  SMALLINT         NULL,
    [away_team_result]  SMALLINT         NULL,
    [fav_team_won_game] SMALLINT         NULL
);


GO
CREATE NONCLUSTERED INDEX [ixFBWeekTemplateRecordID]
    ON [dbo].[FBWeekTemplate]([record_id] ASC);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBWeekTemplate]
    ON [dbo].[FBWeekTemplate]([year_code] ASC);

