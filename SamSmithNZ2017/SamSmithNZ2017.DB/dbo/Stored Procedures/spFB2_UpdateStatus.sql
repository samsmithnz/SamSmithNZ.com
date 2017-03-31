CREATE PROCEDURE [dbo].[spFB2_UpdateStatus]
	@year_code smallint,
	@week_code smallint,
	@donut_cost money
AS
set nocount on

--First update the weeks money
DECLARE @year_end_amount money, @weekly_amount money

CREATE TABLE #tmp_count (count smallint)

--Get all players that made at least one pick this week
INSERT INTO #tmp_count
SELECT count(*)
FROM FBWeek w
INNER JOIN FBPlayer p ON w.player_code = p.player_code and w.year_code = p.year_code
WHERE w.year_code = @year_code and w.week_code = @week_code
and p.is_celebrity = 0
GROUP BY w.player_code
HAVING sum(w.fav_team_picked) >= 0

--The weekly amount available. It's the total number of players * $5
SELECT @weekly_amount = count(*) * 5 FROM #tmp_count

DROP TABLE #tmp_count

--calculate the year end amount (the weekly amount * 15%)
SELECT @year_end_amount = (@weekly_amount * 0.15) 

--Get the net amount for the weekly win (minus year end and donut costs)
SELECT @weekly_amount = @weekly_amount - @year_end_amount - @donut_cost
--SELECT @weekly_amount, @year_end_amount


--Update the money amounts in the FBStatus table
DECLARE @status_count smallint
SELECT @status_count = count(*) 
FROM FBStatus
WHERE year_code = @year_code and week_code = @week_code

IF (@status_count > 0)
BEGIN
	UPDATE FBStatus
	SET year_end_amount = @year_end_amount, weekly_amount = @weekly_amount, donut_cost = @donut_cost
	WHERE year_code = @year_code and week_code = @week_code
END
ELSE
BEGIN
	INSERT INTO FBStatus
	SELECT @year_code, @week_code, @year_end_amount, @weekly_amount, @donut_cost
END

--Update the Player Accounting
-- a. delete all accounting transactions of type "entryfee" for this week and year
DELETE FROM FBAccountingTransaction
WHERE year_code = @year_code
and week_code = @week_code
and transaction_type_code = 1 --Entry fee

-- b. add -5 tranaction of type "entryfee" for all players for this week and year
INSERT INTO FBAccountingTransaction
SELECT @year_code, @week_code, w.player_code, -5, 1, GETDATE()
FROM FBWeek w
INNER JOIN FBPlayer p ON w.player_code = p.player_code and w.year_code = p.year_code
WHERE w.year_code = @year_code and w.week_code = @week_code
and p.is_celebrity = 0
GROUP BY w.player_code
HAVING sum(w.fav_team_picked) >= 0

--Get all the games that have started
DECLARE @games_played smallint

SELECT @games_played = count(*)
FROM FBWeekTemplate
WHERE year_code = @year_code and week_code = @week_code
and game_time < getdate()

--Now update the Summary
DECLARE @player_code smallint

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT player_code
	FROM FBWeek
	WHERE year_code = @year_code and week_code = @week_code 
	--and player_code = 18

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @player_code
WHILE (@@FETCH_STATUS <> -1)
BEGIN
	DECLARE @pcount smallint

	--First check to see if we need to add a player record for this week
	SELECT @pcount = count(*)
	FROM FBSummary
	WHERE player_code = @player_code and year_code = @year_code and week_code = @week_code

	IF (@pcount = 0)
	BEGIN
		INSERT INTO FBSummary
		VALUES (@year_code, @player_code, @week_code, 0, 0, 0, 0, 0, 0, 0, 0, getdate())
	END
	
	DECLARE @games_this_week smallint
	SELECT @games_this_week = count(*)
	FROM FBWeek w
	INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
	WHERE wt.year_code = @year_code 
	and wt.week_code = @week_code
	and w.fav_team_picked >= 0
	and player_code = @player_code
	
	IF (@games_this_week = 0)
	BEGIN
		UPDATE FBWeek
		SET won_pick = -1
		WHERE year_code = @year_code 
		and week_code = @week_code
		and player_code = @player_code
	END

	--Now get to the meat of the calculation
	DECLARE @bln_weekstarted smallint
	DECLARE @current_week_code smallint
	DECLARE @weeks_played smallint
	DECLARE @wins_this_week smallint
	DECLARE @YTD_wins smallint
	DECLARE @YTD_losses smallint
	DECLARE @YTD_win_percentage decimal(18,2)

	--DECLARE @total_games_played smallint
	--SELECT @total_games_played = count(*)
	--FROM FBWeek w
	--INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
	--WHERE w.player_code= @player_code and w.week_code <= @week_code --and w.fav_team_picked >= 0
	--	and wt.year_code = @year_code and wt.week_code <= @week_code
	--	and wt.game_time < getdate()


	--Get the real number of games - if a player plays just one game, then that week counts
	CREATE TABLE #tmp_games_played_in_week (week_code smallint, thecount smallint)	
	INSERT INTO #tmp_games_played_in_week
	SELECT w.week_code, count(*)
	FROM FBWeek w
	WHERE w.year_code = @year_code 
		and w.player_code = @player_code 
		and w.fav_team_picked >= 0
	GROUP BY w.week_code 	
	DECLARE @total_games_played smallint
	SELECT @total_games_played = count(*)
	FROM FBWeek w
	INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
	WHERE wt.year_code = @year_code
		and w.week_code <= @week_code 
		and wt.week_code <= @week_code
		and w.player_code = @player_code 
		and wt.game_time < getdate()
		--and w.fav_team_picked >= 0
		and w.week_code in (SELECT week_code FROM #tmp_games_played_in_week)
	DROP TABLE #tmp_games_played_in_week

	SELECT @bln_weekstarted = isnull(count(*),0) 
	FROM FBWeekTemplate
	WHERE year_code = @year_code and week_code = @week_code and game_time < getdate()

	CREATE TABLE #tmp_week (week_code smallint)	
	INSERT INTO #tmp_week
	SELECT distinct week_code
	FROM FBWeek 
	WHERE player_code = @player_code and year_code = @year_code and week_code <= @week_code and fav_team_picked >= 0	
	SELECT @weeks_played = count(*) FROM #tmp_week

	--Get the number of records returned from the last statement
	/*IF (@bln_weekstarted = 0)
	BEGIN 
		SELECT @weeks_played = @weeks_played - 1
	END*/

	SELECT @YTD_wins = isnull(sum(won_pick),0)
	FROM FBWeek w
	INNER JOIN #tmp_week tw on w.week_code = tw.week_code
	WHERE w.player_code = @player_code 
	and w.fav_team_picked >= 0 
	and w.year_code = @year_code 
	and w.week_code <= @week_code 
	and w.won_pick >= 0

	SELECT @YTD_losses = isnull(count(won_pick),0)
	FROM FBWeek w 
	INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
	INNER JOIN #tmp_week tw on w.week_code = tw.week_code
	WHERE w.player_code = @player_code and w.fav_team_picked >= 0 
		and w.week_code <= @week_code 
		and w.won_pick = 0
		and wt.year_code = @year_code 
		and wt.week_code <= @week_code
		and wt.game_time < getdate()

	--Add games not picked/missed to the losses - but only for weeks that were played (using #tmp_week)
	SELECT @YTD_losses = @YTD_losses + isnull(count(won_pick),0)
	FROM FBWeek w 
	INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
	INNER JOIN #tmp_week tw on w.week_code = tw.week_code
	WHERE w.player_code = @player_code 
		and w.fav_team_picked = -1
		and w.week_code <= @week_code 
		and wt.year_code = @year_code 
		and wt.week_code <= @week_code
		and wt.game_time < getdate()
		
	--select @YTD_losses

	SELECT @wins_this_week = isnull(sum(won_pick),0)
	FROM FBWeek 
	WHERE player_code = @player_code 
	and year_code = @year_code 
	and week_code = @week_code 
	and won_pick > 0 
	and fav_team_picked >= 0

	IF (@total_games_played = 0)
	BEGIN
		SELECT @YTD_win_percentage = 0
	END
	ELSE
	BEGIN
		SELECT @YTD_win_percentage = (convert(decimal(18,4),@YTD_wins)/(convert(decimal(18,4),@total_games_played))) * 100
	END

	UPDATE FBSummary
	SET weeks_played = @weeks_played, wins_this_week = @wins_this_week, YTD_wins = @YTD_wins, YTD_losses = @YTD_losses, YTD_win_percentage = @YTD_win_percentage, last_updated = getdate()
	WHERE player_code = @player_code and year_code = @year_code and week_code = @week_code

	DROP TABLE #tmp_week

	FETCH NEXT FROM Cursor1 INTO @player_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

DECLARE @first_game_time smalldatetime
SELECT @first_game_time = game_time
FROM FBWeekTemplate
WHERE year_code = @year_code and week_code = @week_code
ORDER BY game_time desc
--select @first_game_time, getdate()
--Only process the money if the games have begun...
IF (@first_game_time <= getdate())
BEGIN
	DECLARE @winning_win_count smallint, @winner_count smallint
	DECLARE @winning_amount money
	
	SELECT @winning_win_count = max(wins_this_week)
	FROM FBSummary s
	INNER JOIN FBPlayer p ON s.year_code = p.year_code and s.player_code = p.player_code
	WHERE s.year_code = @year_code and s.week_code = @week_code
	and p.is_celebrity = 0
	
	SELECT @winner_count = count(*)
	FROM FBSummary
	WHERE wins_this_week = @winning_win_count
		and year_code = @year_code and week_code = @week_code
	
	if (@winner_count > 0)
	BEGIN
		SELECT @winning_amount = (weekly_amount/@winner_count)
		FROM FBStatus
		WHERE year_code = @year_code and week_code = @week_code
	END

	if (@winning_amount < 0)
	BEGIN
		SELECT @winning_amount = 0
	END
	
	UPDATE FBSummary
	SET weekly_dollars_won = 0, ytd_dollars_won = 0, last_updated = getdate()
	WHERE year_code = @year_code and week_code = @week_code
	
	UPDATE FBSummary
	SET weekly_dollars_won = @winning_amount, last_updated = getdate()
	FROM FBSummary s
	INNER JOIN FBPlayer p ON s.year_code = p.year_code and s.player_code = p.player_code
	WHERE wins_this_week = @winning_win_count 
		and s.year_code = @year_code 
		and s.week_code = @week_code
		and p.is_celebrity = 0
END
ELSE
BEGIN
	UPDATE FBSummary
	SET weekly_dollars_won = 0, ytd_dollars_won = 0, last_updated = getdate()--ytd_dollars_won + weekly_dollars_won
	FROM FBSummary s
	INNER JOIN FBPlayer p ON s.year_code = p.year_code and s.player_code = p.player_code
	WHERE s.year_code = @year_code 
		and s.week_code = @week_code
		and p.is_celebrity = 0
END

--Update the money amounts
DELETE FROM FBMoney 
WHERE year_code = @year_code and week_code = @week_code

INSERT INTO FBMoney
SELECT @year_code, @week_code, player_code, weekly_dollars_won
FROM FBSummary
WHERE year_code = @year_code and week_code = @week_code and weekly_dollars_won > 0

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT player_code
	FROM FBWeek
	WHERE year_code = @year_code and week_code = @week_code 

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @player_code
WHILE (@@FETCH_STATUS <> -1)
BEGIN
	DECLARE @ytd_dollars_won decimal(10,2)
/*
	SELECT @ytd_dollars_won = isnull(SUM(m.ytd_total),0)
	FROM FBSummary s
	INNER JOIN fbMoney m ON s.player_code = m.player_code
	WHERE m.week_code <= @week_code and 
		s.week_code <= @week_code and 
		m.player_code = @player_code
*/

	SELECT @ytd_dollars_won = isnull(sum(ytd_total),0)
	FROM FBMoney
	WHERE year_code = @year_code and week_code <= @week_code and player_code = @player_code

	--SELECT @ytd_dollars_won

	UPDATE FBSummary
	SET ytd_dollars_won = @ytd_dollars_won, last_updated = getdate()
	WHERE week_code = @week_code and player_code = @player_code and @ytd_dollars_won > 0

	FETCH NEXT FROM Cursor1 INTO @player_code
END

CLOSE Cursor1
DEALLOCATE Cursor1


--Ranking calculations
DECLARE @count smallint, @rank smallint SELECT @count = 0, @rank = 0
DECLARE @decRankCompare decimal(18,2)

DECLARE @max_weeks_played smallint
SELECT @max_weeks_played = max(weeks_played) 
FROM FBSummary
WHERE year_code = @year_code and week_code = @week_code

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT s.player_code, s.YTD_win_percentage
	FROM FBSummary s
	INNER JOIN FBPlayer p ON s.player_code = p.player_code
	WHERE s.year_code = @year_code and p.year_code = @year_code and week_code = @week_code 
	ORDER BY case when weeks_played >= (@max_weeks_played-1) then 0 else 1 end, --To filter out people who have missed 2 or more games.
			YTD_win_percentage DESC, wins_this_week DESC, weeks_played DESC, YTD_wins DESC, player_name

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @player_code, @YTD_win_percentage
WHILE (@@FETCH_STATUS <> -1)
BEGIN

	IF (@count = 0)
	BEGIN
		SELECT @count = 1, @rank = 1
		SELECT @decRankCompare = @YTD_win_percentage
        /*If intOrder = 0 Then
            decRankCompare = CType(DataBinder.Eval(e.Item.DataItem, "YTD_win_percentage"), Decimal)
        ElseIf intOrder = 1 Then
            decRankCompare = CType(DataBinder.Eval(e.Item.DataItem, "wins_this_week"), Decimal)
        End If*/
	END
	ELSE
	BEGIN
		IF (@decRankCompare <> @YTD_win_percentage)
		BEGIN
			SELECT @decRankCompare = @YTD_win_percentage
			SELECT @rank = @count
		END
	END

	UPDATE FBSummary
	SET ranking = @rank, last_updated = getdate()
	WHERE player_code = @player_code and year_code = @year_code and week_code = @week_code

	SELECT @count = @count + 1

	FETCH NEXT FROM Cursor1 INTO @player_code, @YTD_win_percentage
END

CLOSE Cursor1
DEALLOCATE Cursor1