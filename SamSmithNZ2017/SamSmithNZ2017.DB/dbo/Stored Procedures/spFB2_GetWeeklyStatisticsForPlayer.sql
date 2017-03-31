CREATE PROCEDURE [dbo].[spFB2_GetWeeklyStatisticsForPlayer]
	@year_code smallint,
	@player_code smallint
AS

SET NOCOUNT ON

/*
SELECT CONVERT(smallint,null) as week_code, 
	CONVERT(smallint,null) as wins, 
	CONVERT(decimal(5,2),null) as week_percent, 
	CONVERT(smallint,null) as ranking, 
	CONVERT(smallint,null) as previous_ranking,
	CONVERT(smallint,null) as games_played 
*/

CREATE TABLE #tmp_weekly_summary (week_code smallint, wins smallint, week_percent decimal(5,2), ranking smallint, previous_ranking smallint, games_played smallint)

DECLARE @week_code smallint, @ranking smallint, @previous_ranking smallint

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT wt.week_code, s.ranking, isnull(s2.ranking,0) as previous_ranking
	FROM fbweektemplate wt
	INNER JOIN FBSummary s ON wt.week_code = s.week_code and s.year_code = wt.year_code and s.player_code = @player_code
	LEFT JOIN FBSummary s2 ON s2.week_code = s.week_code-1 and s2.year_code = wt.year_code and s2.player_code = @player_code 
	WHERE wt.year_code = @year_code
	GROUP BY wt.week_code, s.ranking, s2.ranking
	ORDER BY wt.week_code

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @week_code, @ranking, @previous_ranking
WHILE (@@FETCH_STATUS <> -1)
BEGIN
	DECLARE @wins smallint, @week_percent decimal(5,2), @fav_wins smallint, @games_played smallint
	
	--Get all wins for this player for this week
	SELECT @wins = count(*)
	FROM fbweek
	WHERE year_code = @year_code 
		and week_code = @week_code
		and player_code = @player_code
		and won_pick = 1

	SELECT @games_played = count(*)
	FROM fbweektemplate
	WHERE year_code = @year_code and week_code = @week_code and game_time < getdate()

	IF (@games_played = 0)
	BEGIN
		SELECT @week_percent = 0
	END
	ELSE
	BEGIN
		SELECT @week_percent = convert(decimal(5,2),convert(decimal(6,4),@wins)/ convert(decimal(6,4),@games_played) * 100)
	END

	INSERT INTO #tmp_weekly_summary
	VALUES (@week_code, @wins, @week_percent, @ranking, @previous_ranking, @games_played)

	FETCH NEXT FROM Cursor1 INTO @week_code, @ranking, @previous_ranking
END

CLOSE Cursor1
DEALLOCATE Cursor1

SELECT * 
FROM #tmp_weekly_summary
ORDER BY week_code

DROP TABLE #tmp_weekly_summary