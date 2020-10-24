CREATE PROCEDURE [dbo].[spWC_AddOddsForOldTournament]
	@tournament_code INT
AS
	SET NOCOUNT ON

	DELETE FROM wc_odds
	WHERE tournament_code = @tournament_code

	--Set game dates.
	DECLARE @StartDate datetime
	DECLARE @EndDate datetime
	SELECT @StartDate = CONVERT(datetime,cast(MIN(game_time) AS date)), --Strip out the time
		@EndDate = CONVERT(datetime,cast(MAX(game_time) AS date)) + 1 --Strip out the time
	FROM wc_game
	WHERE tournament_code = @tournament_code
	DECLARE @Days INT = DATEDIFF(DAY,@StartDate,@EndDate)+1

	--Get all teams
	CREATE TABLE #tmp_teams (team_name VARCHAR(200), team_code INT)

	INSERT INTO #tmp_teams
	SELECT DISTINCT t.team_name, t.team_code
	FROM wc_tournament_team_entry tte
	JOIN wc_team t ON tte.team_code = t.team_code
	WHERE tournament_code = @tournament_code

	--add 1/[num of teams] AS day before first game date.
	INSERT INTO wc_odds
	SELECT team_name, @StartDate - 1, 
		CONVERT(decimal(18,4),1)/CONVERT(decimal(18,4),(SELECT COUNT(*) FROM #tmp_teams)), 0,0,0,0,1, @tournament_code
	FROM #tmp_teams

	-----------------------------------------------------------
	-- Delete the temporary table if it exists
	-----------------------------------------------------------
	IF OBJECT_ID('tempdb..#DateRange') IS NOT NULL 
	BEGIN
		DROP TABLE #DateRange ;
	END

	;WITH AllDays
    AS (SELECT @StartDate AS [Date], 1 AS [level]
        UNION ALL
        SELECT DATEADD(DAY, 1, [Date]), [level] + 1
        FROM AllDays
        WHERE [Date] < @EndDate)
	SELECT [Date], [level]
	INTO #DateRange
	FROM AllDays OPTION (MAXRECURSION 0)

	-------------------------------------------------------------
	---- Create the #DateRange Table, with one record per day
	---- Included an Integer representation YYYYMMDD which
	---- is often used to store date fields in warehouses.
	-------------------------------------------------------------
	--SELECT  DATEADD(DAY, I, @StartDate) AS CalendarDate ,CONVERT(INT,CONVERT(VARCHAR, DATEADD(DAY, I, @StartDate),112)) AS CalendarDateInt
	--INTO #DateRange       
	--FROM    (
	--		  SELECT ROW_NUMBER () OVER (ORDER BY A.object_id) - 1  AS I 
	--		  FROM sys.all_columns A
	--		) AS T1
	--WHERE   DATEADD(DAY, I, @StartDate) <= @EndDate ;

	------------------------------------------------------------------
	-- Validate the data in the #DateRange table
	------------------------------------------------------------------
	--SELECT  @StartDate, @EndDate, @Days ;
	--SELECT MIN(CalendarDate) AS StartDate
	--	  ,MAX(CalendarDate) AS EndDate
	--	  ,COUNT(*) AS Days
	--FROM   #DateRange;          

	------------------------------------------------------------------
	-- Loop through #DateRange, starting with the most recent date.
	------------------------------------------------------------------
	DECLARE @Counter INT
	SELECT @Counter = 0
	WHILE (SELECT COUNT(*) FROM #DateRange) > 0
	BEGIN
		SELECT @Counter = @Counter + 1
		DECLARE @Calendar_Date DATE;
		DECLARE @Calendar_Date_INT INT;
		SELECT TOP 1 @Calendar_Date=CalendarDate
					,@Calendar_Date_INT=CalendarDateInt
		FROM #DateRange 
		ORDER BY CalendarDate;
   
		------------------------------------------------------
		--PUT CODE BELOW HERE---------------------------------
		------------------------------------------------------
	   
		SELECT 'Starting new calendar loop: ', 
			@Calendar_Date, 
			(SELECT COUNT(*) 
				from wc_odds where tournament_code = @tournament_code), 
			'games found:', 
			(SELECT DISTINCT COUNT(*)
				FROM wc_game
				WHERE tournament_code = @tournament_code
				and CONVERT(datetime,cast(game_time AS date)) = CONVERT(datetime,@Calendar_Date))

		----For each game, update the % for the next day.
		DECLARE @game_code INT
		DECLARE @game_date datetime
		DECLARE @round_number INT
		DECLARE @round_code VARCHAR(10)
		DECLARE @team_1_name VARCHAR(200)
		DECLARE @team_2_name VARCHAR(200)
		DECLARE @team_1_previous_day_prob decimal(18,4)
		DECLARE @team_2_previous_day_prob decimal(18,4)
		DECLARE @team_1_score INT
		DECLARE @team_2_score INT

		DECLARE Cursor1 CURSOR LOCAL FOR
			SELECT DISTINCT game_code
			FROM wc_game
			WHERE tournament_code = @tournament_code
			and CONVERT(datetime,cast(game_time AS date)) + 1 = CONVERT(datetime,@Calendar_Date)

		OPEN Cursor1

		--loop through all the items
		FETCH NEXT FROM Cursor1 INTO @game_code
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN

			--1. Get the game details
			SELECT @game_date = CONVERT(datetime,cast(g.game_time AS date)) + 1,
				@round_number = g.round_number,
				@round_code = g.round_code,
				@team_1_name = t1.team_name,
				@team_2_name = t2.team_name,
				@team_1_previous_day_prob = o1.odds_probability,
				@team_2_previous_day_prob = o2.odds_probability,
				@team_1_score = ISNULL(team_1_normal_time_score,0) + ISNULL(team_1_extra_time_score,0)+ISNULL(team_1_penalties_score,0),
				@team_2_score = ISNULL(team_2_normal_time_score,0) + ISNULL(team_2_extra_time_score,0)+ISNULL(team_2_penalties_score,0)
			FROM wc_game g
			JOIN wc_team t1 ON g.team_1_code = t1.team_code
			JOIN wc_team t2 ON g.team_2_code = t2.team_code
			JOIN wc_odds o1 ON t1.team_name = o1.team_name and o1.tournament_code = g.tournament_code
			JOIN wc_odds o2 ON t2.team_name = o2.team_name and o2.tournament_code = g.tournament_code
			WHERE g.game_code = @game_code
			AND g.tournament_code = @tournament_code

			--2. Process who won
			DECLARE @team_1_won bit
			DECLARE @team_2_won bit
			IF (@team_1_score > @team_2_score)
			BEGIN
				SELECT @team_1_won = 1, @team_2_won = 0
			END
			ELSE IF (@team_1_score < @team_2_score)
			BEGIN
				SELECT @team_1_won = 0, @team_2_won = 1
			END
			ELSE IF (@team_1_score = @team_2_score)
			BEGIN
				SELECT @team_1_won = 0, @team_2_won = 0
			END

			--SELECT CONVERT(VARCHAR(12),@team_1_name), @team_1_won, CONVERT(VARCHAR(12),@team_2_name), @team_2_won
			--SELECT * 
			--FROM wc_odds 
			--WHERE tournament_code = @tournament_code 
			--and (team_name = @team_1_name
			--or team_name = @team_2_name)
			--SELECT @game_date, @game_date - 1
			--SELECT odds_probability 
			--				FROM wc_odds o
			--				WHERE o.tournament_code = @tournament_code 
			--				and o.team_name = @team_1_name
			--				and o.odds_date = @game_date - 1
			--SELECT odds_probability 
			--	FROM wc_odds o
			--	WHERE o.tournament_code = @tournament_code 
			--	and o.team_name = @team_2_name
			--	and o.odds_date = @game_date - 1

			--3. Now process the rounds
			IF (@round_number = 1)
			BEGIN
				--Win +1pt
				--Loss -1pt
				--Draw 0pt
				IF (@team_1_won = 1)
				BEGIN
				
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_1_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_1_name
							and o.odds_date = @game_date - 1) + 0.0075, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_1_name
					
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_2_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_2_name
							and o.odds_date = @game_date - 1) - 0.005, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_2_name
				END
				ELSE IF (@team_2_won = 1)
				BEGIN
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_1_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_1_name
							and o.odds_date = @game_date - 1) - 0.005, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_1_name
					
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_2_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_2_name
							and o.odds_date = @game_date - 1) + 0.0075, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_2_name
				END
				ELSE IF (@team_1_won = 0 and @team_2_won = 0)
				BEGIN
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_1_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_1_name
							and o.odds_date = @game_date - 1), 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_1_name
					
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_2_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_2_name
							and o.odds_date = @game_date - 1), 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_2_name
				END
				

				--Process end of round calculations
				--Win +3pt
				--2nd Place +1pt
				--1st+2nd normalized to 4/32 (~12%)
				--3rd+4th = 0pt

				IF (exists(SELECT 1
				FROM wc_game g
				WHERE g.tournament_code = @tournament_code
				and g.game_code = 48
				and 0 = (SELECT COUNT(*) 
								FROM wc_game g2 
								WHERE g2.tournament_code = @tournament_code
								and round_number = @round_number
								and round_code = @round_code
								and game_time > CONVERT(Datetime,@game_date) + 1)))
				BEGIN
					SELECT 'Games in group ' + @round_code + ' are complete' 
				END

				----Store the last game of the round
				--DECLARE @first_day_after_Round_1 datetime
				--SELECT top 1 @first_day_after_Round_1 = CONVERT(datetime,cast(MAX(game_time) AS date)) + 1
				--FROM wc_game 
				--WHERE tournament_code = 19 and round_number = 1

				
				--IF (@calendar_date = @first_day_after_Round_1)
				--BEGIN
				--	SELECT 'Round 1 is over', @calendar_date, @first_day_after_Round_1
				--END
			END
			ELSE IF (@round_number = 2)
			BEGIN
				DECLARE @first_date_of_round_2 datetime
				SELECT @first_date_of_round_2 = CONVERT(datetime,cast(MIN(g.game_time) AS date))
				FROM wc_game g
				--JOIN wc_team t1 ON g.team_1_code = t1.team_code
				--JOIN wc_team t2 ON g.team_2_code = t2.team_code
				WHERE g.tournament_code = @tournament_code
				and g.round_number = 2

				--SELECT 'round 2 start:', @first_date_of_round_2

				UPDATE wc_odds
				set odds_probability = 0
				WHERE tournament_code = @tournament_code
				and team_name not in (SELECT t1.team_name
										FROM wc_game g
										JOIN wc_team t1 ON g.team_1_code = t1.team_code
										WHERE g.tournament_code = @tournament_code
										and g.round_number = 2
										and g.game_time >= @first_date_of_round_2)
				and team_name not in (SELECT t2.team_name
										FROM wc_game g
										JOIN wc_team t2 ON g.team_2_code = t2.team_code
										WHERE g.tournament_code = @tournament_code
										and g.round_number = 2
										and g.game_time >= @first_date_of_round_2)
				and odds_date >= @first_date_of_round_2

				IF (@team_1_won = 1)
				BEGIN
				
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_1_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_1_name
							and o.odds_date = @game_date - 1) + CASE WHEN @round_code = '16' THEN CONVERT(decimal(18,4),1)/16 WHEN @round_code = 'QF' THEN CONVERT(decimal(18,4),1)/8 WHEN @round_code = 'SF' THEN CONVERT(decimal(18,4),1)/4 WHEN @round_code = 'FF' THEN 1/2 ELSE 0 END, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_1_name
					
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_2_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_2_name
							and o.odds_date = @game_date - 1) * 0, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_2_name

					
					IF (@round_code = 'FF' and @team_1_won = 1)
					BEGIN
						UPDATE wc_odds
						SET odds_probability = 1
						WHERE tournament_code = @tournament_code
						and odds_date = @game_date
						and team_name = @team_1_name
					END
				END
				ELSE IF (@team_2_won = 1)
				BEGIN
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_1_name
					
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_1_name
							and o.odds_date = @game_date - 1) * 0, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_1_name
					
					--SELECT @game_date,* FROM wc_odds WHERE tournament_code = @tournament_code and team_name = @team_2_name
					
					--SELECT 'round', @round_code, CASE WHEN @round_code = '16' THEN CONVERT(decimal(18,4),1)/16 WHEN @round_code = 'QF' THEN CONVERT(decimal(18,4),1)/8 WHEN @round_code = 'SF' THEN CONVERT(decimal(18,4),1)/4 WHEN @round_code = 'FF' THEN CONVERT(decimal(18,4),1)/2 ELSE 0 END
					INSERT INTO wc_odds
					SELECT t.team_name, @game_date, 
						(SELECT top 1 odds_probability 
							FROM wc_odds o
							WHERE o.tournament_code = @tournament_code 
							and o.team_name = @team_2_name
							and o.odds_date = @game_date - 1) + CASE WHEN @round_code = '16' THEN CONVERT(decimal(18,4),1)/16 WHEN @round_code = 'QF' THEN CONVERT(decimal(18,4),1)/8 WHEN @round_code = 'SF' THEN CONVERT(decimal(18,4),1)/4 WHEN @round_code = 'FF' THEN CONVERT(decimal(18,4),1)/2 ELSE 0 END, 
						0,0,0,0,1,@tournament_code
					FROM #tmp_teams t
					WHERE t.team_name = @team_2_name

					IF (@round_code = 'FF' and @team_2_won = 1)
					BEGIN
						UPDATE wc_odds
						SET odds_probability = 1
						WHERE tournament_code = @tournament_code
						and odds_date = @game_date
						and team_name = @team_2_name
					END
				END



			END

			FETCH NEXT FROM Cursor1 INTO @game_code
		END

		CLOSE Cursor1
		DEALLOCATE Cursor1

		DECLARE @missing_gamedate DATETIME
		SELECT @missing_gamedate = CONVERT(datetime,@Calendar_Date)
		SELECT 'Fill in days', @missing_gamedate
		
		--SELECT o3.odds_probability 
		--		FROM wc_odds o3
		--		WHERE o3.tournament_code = @tournament_code 
		--		and o3.odds_date = @missing_gamedate - 1

		--Fill in the missing days
		INSERT INTO wc_odds
		SELECT DISTINCT o.team_name, @missing_gamedate, 
			(SELECT top 1 o3.odds_probability 
				FROM wc_odds o3
				WHERE o3.tournament_code = @tournament_code 
				and o3.team_name = o.team_name
				and o3.odds_date = @missing_gamedate - 1), 
			0,0,0,0,1,@tournament_code					
		FROM wc_odds o
		WHERE o.tournament_code = @tournament_code 
		and o.team_name not in (SELECT o2.team_name
								FROM wc_odds o2
								WHERE o2.tournament_code = @tournament_code 
								and o2.odds_date = @missing_gamedate)

		SELECT @Counter AS counter, 32 + (@Counter*32) AS total, COUNT(*) AS actual_total
		FROM wc_odds
		WHERE tournament_code = @tournament_code

		--select * from wc_odds
		--WHERE tournament_code = @tournament_code
		--order by team_name, odds_date

	   ------------------------------------------------------
	   --PUT CODE ABOVE HERE---------------------------------
	   ------------------------------------------------------
	   --SELECT @Calendar_Date AS Calendar_Date ;       
   
	   DELETE FROM #DateRange 
	   WHERE CalendarDate=(SELECT MIN(CalendarDate) FROM #DateRange);

	END;

	

	--Get all teams and add 1/[num of teams] AS day before first game date.
	--For each game, update the % for the next day.
	--Fill in rest days with data from the day before

	DROP TABLE #tmp_teams