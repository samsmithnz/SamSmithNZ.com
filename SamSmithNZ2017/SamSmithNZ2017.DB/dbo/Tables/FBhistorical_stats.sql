CREATE TABLE [dbo].[FBhistorical_stats] (
    [year_code]    SMALLINT NOT NULL,
    [week_code]    SMALLINT NOT NULL,
    [fav_wins]     SMALLINT NULL,
    [home_wins]    SMALLINT NULL,
    [games_played] SMALLINT NULL,
    CONSTRAINT [PK_historical_stats] PRIMARY KEY CLUSTERED ([year_code] ASC, [week_code] ASC)
);

