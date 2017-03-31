CREATE TABLE [dbo].[FBSummary] (
    [year_code]          SMALLINT        NULL,
    [player_code]        SMALLINT        NULL,
    [week_code]          SMALLINT        NULL,
    [weeks_played]       SMALLINT        NULL,
    [wins_this_week]     SMALLINT        NULL,
    [YTD_wins]           SMALLINT        NULL,
    [YTD_losses]         SMALLINT        NULL,
    [YTD_win_percentage] DECIMAL (18, 2) NULL,
    [weekly_dollars_won] MONEY           NULL,
    [ytd_dollars_won]    MONEY           NULL,
    [ranking]            SMALLINT        NULL,
    [last_updated]       DATETIME        NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBSummary]
    ON [dbo].[FBSummary]([year_code] ASC);

