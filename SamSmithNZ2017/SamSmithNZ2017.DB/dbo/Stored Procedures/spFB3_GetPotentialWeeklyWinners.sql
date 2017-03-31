CREATE PROCEDURE [dbo].[spFB3_GetPotentialWeeklyWinners]
	@year_code smallint,
	@week_code smallint
AS
SET NOCOUNT ON

/*
--For strongly typed datasets
SELECT convert(varchar(4),null) as code,
	convert(varchar(8000),null) as [text],
	convert(smallint,null) as wins_this_week
*/
DECLARE @maxwins smallint
DECLARE @number_of_games_left smallint

CREATE TABLE #tmp_games_left (record_id uniqueidentifier)
CREATE TABLE #tmp_results_base (player_code smallint, wins_this_week smallint)
CREATE TABLE #tmp_results_fav (player_code smallint, wins_this_week smallint)
CREATE TABLE #tmp_results_underdog (player_code smallint, wins_this_week smallint)
CREATE TABLE #tmp_player_picks (player_code smallint, record_id uniqueidentifier, fav_team_picked bit)

--Get the number of games left
INSERT INTO #tmp_games_left
SELECT record_id
FROM FBWeekTemplate
WHERE year_code = @year_code
and week_code = @week_code
--and game_time >= dateadd(hh,-3,getdate())--'2007/11/4 8:00 pm'--
and fav_team_won_game = -1
order by game_time desc

--Get the top number of wins, and minus off the number of games, to get the maximum number of wins for people in contention.
SELECT TOP 1 @maxwins = (wins_this_week - (SELECT count(*) FROM #tmp_games_left))
FROM FBSummary
WHERE year_code = @year_code
and week_code = @week_code
ORDER BY wins_this_week desc

INSERT INTO #tmp_results_base
SELECT player_code, wins_this_week
FROM FBSummary
WHERE year_code = @year_code
and week_code = @week_code
and wins_this_week >= @maxwins
ORDER BY wins_this_week desc

--Get a list of everyone in contention into two tables, a fav table and under table.
INSERT INTO #tmp_results_fav
SELECT player_code, wins_this_week
FROM FBSummary
WHERE year_code = @year_code
and week_code = @week_code
and wins_this_week >= @maxwins
ORDER BY wins_this_week desc

INSERT INTO #tmp_results_underdog
SELECT * FROM #tmp_results_fav

--Get the players Picks
INSERT INTO #tmp_player_picks (player_code, record_id, fav_team_picked)
SELECT rf.player_code, gl.record_id, fb.fav_team_picked
FROM #tmp_games_left gl, #tmp_results_fav rf, FBWeek fb 
WHERE fb.year_code = @year_code
and fb.week_code = @week_code
and rf.player_code = fb.player_code
and fb.record_id = gl.record_id

DECLARE @record_id uniqueidentifier

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT record_id
	FROM #tmp_games_left

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @record_id
WHILE (@@FETCH_STATUS <> -1)
BEGIN
	
	UPDATE #tmp_results_fav
	SET wins_this_week = wins_this_week + 1
	FROM #tmp_player_picks pp
	INNER JOIN #tmp_results_fav rf ON pp.player_code = rf.player_code
	WHERE pp.fav_team_picked = 1

	UPDATE #tmp_results_underdog
	SET wins_this_week = wins_this_week + 1
	FROM #tmp_player_picks pp
	INNER JOIN #tmp_results_underdog rf ON pp.player_code = rf.player_code
	WHERE pp.fav_team_picked = 0

	FETCH NEXT FROM Cursor1 INTO @record_id
END

CLOSE Cursor1
DEALLOCATE Cursor1
/*
SELECT 1, fb.record_id, fb.home_team_code, fb.away_team_code, fb.fav_team_code, fb.spread
FROM #tmp_games_left gl
INNER JOIN FBWeekTemplate fb ON fb.record_id = gl.record_id

SELECT 2, * 
FROM #tmp_player_picks

SELECT 3, * 
FROM #tmp_results_fav
ORDER BY wins_this_week desc

SELECT 4, * 
FROM #tmp_results_underdog
ORDER BY wins_this_week desc
*/
--select * from #tmp_results_fav
--select * from #tmp_results_underdog
DECLARE @fav_team_name varchar(100), @underdog_team_name varchar(100), @spread decimal(4,0)
DECLARE @fav_wins smallint, @fav_wins_count smallint
DECLARE @underdog_wins smallint, @underdog_wins_count smallint


--If the result of the game has the home team as the favorite
IF (exists (SELECT 1 FROM #tmp_games_left fl INNER JOIN FBWeekTemplate wt ON fl.record_id = wt.record_id WHERE home_team_code = fav_team_code))
BEGIN
	SELECT @fav_team_name = ht.team_name, @underdog_team_name = at.team_name, @spread = wt.spread
	FROM #tmp_games_left fl
	INNER JOIN FBWeekTemplate wt ON fl.record_id = wt.record_id
	INNER JOIN FBTeam ht on wt.home_team_code = ht.team_code
	INNER JOIN FBTeam at on wt.away_team_code = at.team_code
END
ELSE --The fav team is away
BEGIN
	SELECT @fav_team_name = at.team_name, @underdog_team_name = ht.team_name, @spread = wt.spread
	FROM #tmp_games_left fl
	INNER JOIN FBWeekTemplate wt ON fl.record_id = wt.record_id
	INNER JOIN FBTeam ht on wt.home_team_code = ht.team_code
	INNER JOIN FBTeam at on wt.away_team_code = at.team_code
END

SELECT @fav_wins = max(wins_this_week) FROM #tmp_results_fav
SELECT @fav_wins_count = count(*) FROM #tmp_results_fav WHERE wins_this_week = @fav_wins
SELECT @underdog_wins = max(wins_this_week) FROM #tmp_results_underdog
SELECT @underdog_wins_count = count(*) FROM #tmp_results_underdog WHERE wins_this_week = @underdog_wins

SELECT @number_of_games_left = count(*) FROM #tmp_games_left

--More than 2 games left
IF (@number_of_games_left > 2)
BEGIN
	SELECT 0 as code, 'There are too many games left to calculate all the possibilities' as [text], 0 as wins_this_week
END

--Exactly 2 games left
IF (@number_of_games_left = 2)
BEGIN
	--SELECT * FROM #tmp_results_fav order by wins_this_week desc
	--SELECT * FROM #tmp_results_underdog order by wins_this_week desc

	CREATE TABLE #tmp_result_multiple_games (player_code smallint, pick1 char(1), pick2 char(1))

	--select * from #tmp_player_picks
	DECLARE @player_code smallint
	DECLARE @fav_team_picked bit
	DECLARE @game_time datetime

	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT DISTINCT player_code, fav_team_picked, game_time
		FROM #tmp_player_picks pp
		INNER JOIN FBWeekTemplate wt ON wt.record_id = pp.record_id
		--WHERE player_code = 14
		ORDER BY wt.game_time
	
	OPEN Cursor1
	
	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @player_code, @fav_team_picked, @game_time
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN

		IF (exists (SELECT * FROM #tmp_result_multiple_games WHERE player_code = @player_code))
		BEGIN
			UPDATE #tmp_result_multiple_games
			SET pick2 = (case when @fav_team_picked = 1 then 'A' else 'B' end)
			WHERE player_code = @player_code
		END
		ELSE
		BEGIN
			INSERT INTO #tmp_result_multiple_games
			SELECT @player_code, case when @fav_team_picked = 1 then 'A' else 'B' end, null
		END		

		FETCH NEXT FROM Cursor1 INTO @player_code, @fav_team_picked, @game_time
	END
	
	CLOSE Cursor1
	DEALLOCATE Cursor1	
/*
	SELECT p.player_name, mg.* 
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
*/
	--AA
	SELECT 'AA' as code, player_name + ' - ' + convert(varchar(3),wins_this_week+2) + ' wins' as [text], wins_this_week+2 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'A'
	UNION
	SELECT 'AA', player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins', wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'B'
	UNION
	SELECT 'AA', player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins', wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'A'
	UNION
	SELECT 'AA', player_name + ' - ' + convert(varchar(3),wins_this_week) + ' wins', wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'B'

	--AB
	UNION
	SELECT 'AB' as code, player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins' as [text], wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'A'
	UNION
	SELECT 'AB', player_name + ' - ' + convert(varchar(3),wins_this_week+2) + ' wins', wins_this_week+2 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'B'
	UNION
	SELECT 'AB', player_name + ' - ' + convert(varchar(3),wins_this_week) + ' wins', wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'A'
	UNION
	SELECT 'AB', player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins', wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'B'

	--BA
	UNION
	SELECT 'BA' as code, player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins' as [text], wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'A'
	UNION
	SELECT 'BA', player_name + ' - ' + convert(varchar(3),wins_this_week) + ' wins', wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'B'
	UNION
	SELECT 'BA', player_name + ' - ' + convert(varchar(3),wins_this_week+2) + ' wins', wins_this_week+2 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'A'
	UNION
	SELECT 'BA', player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins', wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'B'

	--BB
	UNION
	SELECT 'BB' as code, player_name + ' - ' + convert(varchar(3),wins_this_week) + ' wins' as [text], wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'A'
	UNION
	SELECT 'BB', player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins', wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'A' and pick2 = 'B'
	UNION
	SELECT 'BB', player_name + ' - ' + convert(varchar(3),wins_this_week+1) + ' wins', wins_this_week+1 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'A'
	UNION
	SELECT 'BB', player_name + ' - ' + convert(varchar(3),wins_this_week+2) + ' wins', wins_this_week+2 as wins_this_week
	FROM #tmp_result_multiple_games mg
	INNER JOIN FBPlayer p ON mg.player_code = p.player_code and p.year_code = @year_code
	INNER JOIN #tmp_results_base rb ON p.player_code = rb.player_code
	WHERE pick1 = 'B' and pick2 = 'B'
	ORDER BY code, wins_this_week desc

	DROP TABLE #tmp_result_multiple_games

END
 
--Exactly 1 game left
IF (@number_of_games_left = 1)
BEGIN
	SELECT '1' as code, 
		'If the favorite (' 
		+ @fav_team_name + ') wins by ' 
		+ convert(varchar(5),@spread * -1) + ' or more, then ' 
		+ convert(varchar(5),@fav_wins_count) 
		+ case when @fav_wins_count = 1 then ' person wins with ' else ' people win with ' end
		+ convert(varchar(5),@fav_wins) 
		+ case when @fav_wins_count = 1 then ' wins. ' else ' wins each.' end as [text], 
		0 as wins_this_week

	UNION
	SELECT '2', 
		player_name, 0 as wins_this_week
	FROM #tmp_results_fav rf
	INNER JOIN FBPlayer p ON rf.player_code = p.player_code
	WHERE p.year_code = @year_code
	and wins_this_week = @fav_wins

	UNION
	SELECT '3', 
		'If the underdog (' 
		+ @underdog_team_name + ') wins, or the favorite (' 
		+ @fav_team_name + ') doesn''t win by '
		+ convert(varchar(5),@spread * -1) + ' or more, then ' 
		+ convert(varchar(5),@underdog_wins_count) 
		+ case when @underdog_wins_count = 1 then ' person wins with ' else ' people win with ' end
		+ convert(varchar(5),@underdog_wins) 
		+ case when @underdog_wins_count = 1 then ' wins.' else ' wins each.' end as text, 
		0 as wins_this_week

	UNION	
	SELECT '4', 
		player_name, 0 as wins_this_week
	FROM #tmp_results_underdog rf
	INNER JOIN FBPlayer p ON rf.player_code = p.player_code
	WHERE p.year_code = @year_code
	and wins_this_week = @underdog_wins

	ORDER BY code
END

IF (@number_of_games_left = 0)
BEGIN
	SELECT 0 as code, 'This week is complete and there are no games left to predict' as [text], 0 as wins_this_week
END

DROP TABLE #tmp_games_left
DROP TABLE #tmp_results_base
DROP TABLE #tmp_results_fav 
DROP TABLE #tmp_results_underdog