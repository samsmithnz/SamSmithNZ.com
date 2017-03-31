CREATE TABLE [dbo].[FBWeek] (
    [year_code]       SMALLINT         NULL,
    [player_code]     SMALLINT         NULL,
    [week_code]       SMALLINT         NULL,
    [record_id]       UNIQUEIDENTIFIER NULL,
    [fav_team_picked] SMALLINT         NULL,
    [won_pick]        SMALLINT         CONSTRAINT [DF_FBWeek_won_pick] DEFAULT ((0)) NULL,
    [last_updated]    DATETIME         NULL
);


GO
CREATE NONCLUSTERED INDEX [ixFBWeekYearWeekFavTeamPickedRecordId]
    ON [dbo].[FBWeek]([year_code] ASC, [week_code] ASC, [fav_team_picked] ASC, [record_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ixFBWeekPlayerYearWeek]
    ON [dbo].[FBWeek]([year_code] ASC, [week_code] ASC, [player_code] ASC);


GO
CREATE NONCLUSTERED INDEX [ixFBWeekPlayerRecordID]
    ON [dbo].[FBWeek]([player_code] ASC, [record_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ixFBWeekRecordID]
    ON [dbo].[FBWeek]([record_id] ASC);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBWeek]
    ON [dbo].[FBWeek]([year_code] ASC);

