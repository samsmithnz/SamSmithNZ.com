CREATE PROCEDURE [dbo].[spFB_UpdateStatus]
	@year_code smallint,
	@week_code smallint,
	@donut_cost money
AS
set nocount on

--First update the weeks money
DECLARE @year_end_amount money, @weekly_amount money

CREATE TABLE #tmp_count (count smallint)

INSERT INTO #tmp_count
SELECT count(*)
FROM fbweek w
INNER JOIN fbplayer p ON w.player_code = p.player_code and w.year_code = p.year_code
WHERE w.year_code = @year_code and w.week_code = @week_code
and p.is_celebrity = 0
GROUP BY w.player_code
HAVING sum(w.fav_team_picked) >= 0

SELECT @weekly_amount = count(*) * 5 FROM #tmp_count

DROP TABLE #tmp_count

--calculate the year end amount
SELECT @year_end_amount = (@weekly_amount * 0.15) 
--Get the total amount for the weekly win.
SELECT @weekly_amount = @weekly_amount - @year_end_amount - @donut_cost
--SELECT @weekly_amount, @year_end_amount

DECLARE @status_count smallint

SELECT @status_count = count(*) 
FROM fbStatus
WHERE year_code = @year_code and week_code = @week_code

IF (@status_count > 0)
BEGIN
	UPDATE fbStatus
	SET year_end_amount = @year_end_amount, weekly_amount = @weekly_amount, donut_cost = @donut_cost
	WHERE year_code = @year_code and week_code = @week_code
END
ELSE
BEGIN
	INSERT INTO fbStatus
	SELECT @year_code, @week_code, @year_end_amount, @weekly_amount, @donut_cost
END

--Get all the games that have started
DECLARE @games_played smallint

SELECT @games_played = count(*)
FROM fbweektemplate
WHERE year_code = @year_code and week_code = @week_code
and game_time < getdate()

--Now update the Summary
DECLARE @player_code smallint

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT player_code
	FROM FBweek
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

	DECLARE @bln_weekstarted smallint
	DECLARE @current_week_code smallint
	DECLARE @weeks_played smallint
	DECLARE @wins_this_week smallint
	DECLARE @YTD_wins smallint
	DECLARE @YTD_losses smallint
	DECLARE @YTD_win_percentage decimal(18,2)

	DECLARE @total_games_played smallint
	SELECT @total_games_played = count(*)
	FROM fbweek w
	INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
	WHERE w.player_code= @player_code and w.week_code <= @week_code and w.fav_team_picked >= 0
		and wt.year_code = @year_code and wt.week_code <= @week_code
		and wt.game_time < getdate()

	SELECT @bln_weekstarted = isnull(count(*),0) 
	FROM fbweektemplate
	WHERE year_code = @year_code and week_code = @week_code and game_time < getdate()

	CREATE TABLE #tmp_week (week_code smallint)
	
	INSERT INTO #tmp_week
	SELECT distinct week_code
	FROM fbweek 
	WHERE player_code = @player_code and year_code = @year_code and week_code <= @week_code and fav_team_picked >= 0
	
	SELECT @weeks_played = count(*) FROM #tmp_week

	DROP TABLE #tmp_week

	--Get the number of records returned from the last statement
	/*IF (@bln_weekstarted = 0)
	BEGIN 
		SELECT @weeks_played = @weeks_played - 1
	END*/

	SELECT @YTD_wins = isnull(sum(won_pick),0)
	FROM fbweek 
	WHERE player_code = @player_code and fav_team_picked >= 0 and year_code = @year_code and week_code <= @week_code and won_pick >= 0

	SELECT @YTD_losses = isnull(count(won_pick),0)
	FROM fbweek w 
	INNER JOIN fbweektemplate wt ON w.record_id = wt.record_id
	WHERE w.player_code = @player_code and w.fav_team_picked >= 0 
		and w.week_code <= @week_code and w.won_pick = 0
		and wt.year_code = @year_code and wt.week_code <= @week_code 		and wt.game_time < getdate()

	--select @YTD_losses

	SELECT @wins_this_week = isnull(sum(won_pick),0)
	FROM fbweek 
	WHERE player_code = @player_code and year_code = @year_code and week_code = @week_code and won_pick > 0 and fav_team_picked >= 0

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

	FETCH NEXT FROM Cursor1 INTO @player_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

DECLARE @first_game_time smalldatetime
SELECT @first_game_time = game_time
FROM fbweektemplate
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
	INNER JOIN fbplayer p ON s.year_code = p.year_code and s.player_code = p.player_code
	WHERE s.year_code = @year_code and s.week_code = @week_code
	and p.is_celebrity = 0
	
	SELECT @winner_count = count(*)
	FROM FBSummary
	WHERE wins_this_week = @winning_win_count
		and year_code = @year_code and week_code = @week_code
	
	if (@winner_count > 0)
	BEGIN
		SELECT @winning_amount = (weekly_amount/@winner_count)
		FROM fbstatus
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
	INNER JOIN fbplayer p ON s.year_code = p.year_code and s.player_code = p.player_code
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
	INNER JOIN fbplayer p ON s.year_code = p.year_code and s.player_code = p.player_code
	WHERE s.year_code = @year_code 
		and s.week_code = @week_code
		and p.is_celebrity = 0
END

--Update the money amounts
DELETE FROM fbmoney 
WHERE year_code = @year_code and week_code = @week_code

INSERT INTO fbmoney
SELECT @year_code, @week_code, player_code, weekly_dollars_won
FROM fbsummary
WHERE year_code = @year_code and week_code = @week_code and weekly_dollars_won > 0

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT player_code
	FROM FBweek
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
	FROM fbmoney
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
DECLARE @count smallint, @rank smallint
SELECT @count = 0, @rank = 0
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