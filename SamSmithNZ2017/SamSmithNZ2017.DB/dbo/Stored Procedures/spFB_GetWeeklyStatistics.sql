CREATE PROCEDURE [dbo].[spFB_GetWeeklyStatistics]
	@year_code smallint,
	@max_week_code smallint
AS

CREATE TABLE #tmp_weekly_summary (week_code smallint, home_wins smallint, fav_wins smallint, games_played smallint)

DECLARE @week_code smallint

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT week_code
	FROM fbweektemplate
	WHERE year_code = @year_code
		and week_code <= @max_week_code

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @week_code
WHILE (@@FETCH_STATUS <> -1)
BEGIN
	DECLARE @fav_home_wins smallint, @under_home_wins smallint, @fav_wins smallint, @games_played smallint
	--Get all home wins where the fav was the home team
	SELECT @fav_home_wins = count(wt.week_code)
	FROM fbweektemplate wt
	WHERE (wt.home_team_code = wt.fav_team_code and wt.fav_team_won_game = 1)
	and wt.year_code = @year_code and wt.week_code = @week_code

	--Get all home wins where the underdog was the home team
	SELECT @under_home_wins = count(wt.week_code)
	FROM fbweektemplate wt
	WHERE (wt.home_team_code <> wt.fav_team_code and wt.fav_team_won_game = 0)
	and wt.year_code = @year_code and wt.week_code = @week_code

	--Get all fav Wins
	SELECT @fav_wins = count(wt.week_code) 
	FROM fbweektemplate wt
	WHERE wt.fav_team_won_game = 1 
	and wt.year_code = @year_code and wt.week_code = @week_code

	SELECT @games_played = count(*)
	FROM fbweektemplate
	WHERE year_code = @year_code and week_code = @week_code and game_time < getdate()

	INSERT INTO #tmp_weekly_summary
	VALUES (@week_code, (@fav_home_wins + @under_home_wins), @fav_wins, @games_played)

	FETCH NEXT FROM Cursor1 INTO @week_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

SELECT * 
FROM #tmp_weekly_summary
ORDER BY week_code

DROP TABLE #tmp_weekly_summary