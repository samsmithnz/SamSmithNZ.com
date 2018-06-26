CREATE PROCEDURE [dbo].[FB_SaveGroupDetails]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

	--Get the number of teams to set for each group
	DECLARE @TeamsFromEachGroupThatAdvance INT
	DECLARE @TeamsFromAllGroupsThatAdvance INT
	DECLARE @TotalNumberOfTeamsThatAdvanceFromStage INT
	DECLARE @GroupAdvancementDifference INT
	IF (@RoundNumber = 1)
	BEGIN
		SELECT @TeamsFromAllGroupsThatAdvance = (tf.r1_number_of_teams_from_group_that_advance * tf.r1_number_of_groups_in_round),
			@TotalNumberOfTeamsThatAdvanceFromStage = tf.r1_total_number_of_teams_that_advance,
			@TeamsFromEachGroupThatAdvance = tf.r1_number_of_teams_from_group_that_advance
		FROM wc_tournament t 
		JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
		WHERE t.tournament_code = @TournamentCode
	END
	ELSE IF (@RoundNumber = 2)
	BEGIN
		SELECT @TeamsFromAllGroupsThatAdvance = (tf.r2_number_of_teams_from_group_that_advance * tf.r2_number_of_groups_in_round),
			@TotalNumberOfTeamsThatAdvanceFromStage = tf.r2_total_number_of_teams_that_advance,
			@TeamsFromEachGroupThatAdvance = tf.r2_number_of_teams_from_group_that_advance
		FROM wc_tournament t 
		JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
		WHERE t.tournament_code = @TournamentCode
	END
	ELSE IF (@RoundNumber = 3)
	BEGIN
		SELECT @TeamsFromAllGroupsThatAdvance = (tf.r3_number_of_teams_from_group_that_advance * tf.r3_number_of_groups_in_round),
			@TotalNumberOfTeamsThatAdvanceFromStage = tf.r3_total_number_of_teams_that_advance,
			@TeamsFromEachGroupThatAdvance = tf.r3_number_of_teams_from_group_that_advance
		FROM wc_tournament t 
		JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
		WHERE t.tournament_code = @TournamentCode
	END

	IF (@TeamsFromAllGroupsThatAdvance <> @TotalNumberOfTeamsThatAdvanceFromStage)
	BEGIN
		SELECT @GroupAdvancementDifference = @TotalNumberOfTeamsThatAdvanceFromStage - @TeamsFromAllGroupsThatAdvance
	END

	CREATE TABLE #tmp_teams (team_code INT)

	--get all teams affected
	INSERT INTO #tmp_teams
	SELECT team_1_code
	FROM wc_game
	WHERE tournament_code = @TournamentCode
	AND round_number = @RoundNumber
	AND round_code = @RoundCode
	UNION
	SELECT team_2_code
	FROM wc_game
	WHERE tournament_code = @TournamentCode
	AND round_number = @RoundNumber
	AND round_code = @RoundCode

	--Clean up the group table
	DELETE FROM wc_group_stage
	WHERE tournament_code = @TournamentCode
	AND round_number = @RoundNumber
	AND round_code = @RoundCode

	--Build the Group Table
	DECLARE @team_code INT
	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT DISTINCT team_code
		FROM #tmp_teams 

	OPEN Cursor1

	--loop through all the teams, getting the record of each team, inserting in default placing.
	FETCH NEXT FROM Cursor1 INTO @team_code
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN

		--select @team_code
		INSERT INTO wc_group_stage
		SELECT @RoundNumber, @RoundCode, @TournamentCode, @team_code, 
			dbo.fnWC_GetTeamGamesPlayed(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --played
			dbo.fnWC_GetTeamWins(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --W
			dbo.fnWC_GetTeamDraws(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --D
			dbo.fnWC_GetTeamLosses(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --L
			dbo.fnWC_GetTeamGoalsFor(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --GF
			dbo.fnWC_GetTeamGoalsAgainst (@TournamentCode, @RoundNumber, @RoundCode, @team_code), --GA
			dbo.fnWC_GetTeamGoalsFor(@TournamentCode, @RoundNumber, @RoundCode, @team_code) - dbo.fnWC_GetTeamGoalsAgainst (@TournamentCode, @RoundNumber, @RoundCode, @team_code), --GD
			(dbo.fnWC_GetTeamWins(@TournamentCode, @RoundNumber, @RoundCode, @team_code) * dbo.fnWC_GetPointsForAWin(@TournamentCode)) + (dbo.fnWC_GetTeamDraws(@TournamentCode, @RoundNumber, @RoundCode, @team_code) * 1), --Pts
			0, 0

		--select * From wc_group_stage where tournament_code = 16

		FETCH NEXT FROM Cursor1 INTO @team_code
	END

	CLOSE Cursor1
	DEALLOCATE Cursor1

	--select * From wc_group_stage where tournament_code = @TournamentCode

	--Set the Group Ranking for each group
	DECLARE @group_ranking INT
	SELECT @group_ranking = 0

	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT gs.team_code
		FROM wc_group_stage gs
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		AND round_code = @RoundCode
		ORDER BY points DESC, goal_difference DESC, goals_for DESC

	OPEN Cursor1

	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @team_code
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		SELECT @group_ranking = @group_ranking + 1
	
		DECLARE @has_qualified_for_next_round bit
		IF (@group_ranking <= @TeamsFromEachGroupThatAdvance)
		BEGIN
			SELECT @has_qualified_for_next_round = 1
		END
		ELSE
		BEGIN
			SELECT @has_qualified_for_next_round = 0
		END

		UPDATE wc_group_stage
		SET group_ranking = @group_ranking, has_qualified_for_next_round = @has_qualified_for_next_round
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		AND round_code = @RoundCode
		AND team_code = @team_code

		FETCH NEXT FROM Cursor1 INTO @team_code
	END

	CLOSE Cursor1
	DEALLOCATE Cursor1

	/*
	SELECT t.team_name, gs.*
	FROM wc_group_stage gs
	JOIN wc_team t ON gs.team_code = t.team_code
	WHERE tournament_code = @TournamentCode
	AND round_number = @RoundNumber
	AND has_qualified_for_next_round = 0
	ORDER BY points DESC, goal_difference DESC, goals_for DESC
	*/

	IF (@TeamsFromAllGroupsThatAdvance = (SELECT COUNT(*) FROM wc_group_stage gs
											WHERE tournament_code = @TournamentCode
											AND round_number = @RoundNumber
											AND group_ranking <= @TeamsFromEachGroupThatAdvance))
	BEGIN
			UPDATE wc_group_stage
			SET has_qualified_for_next_round = 0
			WHERE tournament_code = @TournamentCode
			AND round_number = @RoundNumber
			AND team_code = @team_code
			AND group_ranking <= @TeamsFromEachGroupThatAdvance
	

		--Finally set any teams that have qualified for the next round in 3rd place positions
		SELECT @group_ranking = 0
		DECLARE Cursor1 CURSOR LOCAL FOR
			SELECT gs.team_code
			FROM wc_group_stage gs
			WHERE tournament_code = @TournamentCode
			AND round_number = @RoundNumber
			AND has_qualified_for_next_round = 0
			ORDER BY group_ranking, points DESC, goal_difference DESC, goals_for DESC

		OPEN Cursor1

		--loop through all the items
		FETCH NEXT FROM Cursor1 INTO @team_code
		WHILE (@@FETCH_STATUS <> -1 AND @group_ranking < @GroupAdvancementDifference)
		BEGIN
			SELECT @group_ranking = @group_ranking + 1
		
			UPDATE wc_group_stage
			SET has_qualified_for_next_round = 1
			WHERE tournament_code = @TournamentCode
			AND round_number = @RoundNumber
			AND team_code = @team_code

			FETCH NEXT FROM Cursor1 INTO @team_code
		END

		CLOSE Cursor1
		DEALLOCATE Cursor1
	END



	--Update the playoff's if the group is done.
	DECLARE @GroupRanking INT
	DECLARE @TeamCode INT

	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT gs.round_code, gs.group_ranking, gs.team_code
		FROM wc_group_stage gs
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		AND has_qualified_for_next_round = 1
		ORDER BY gs.round_code, gs.group_ranking

	OPEN Cursor1

	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @RoundCode, @GroupRanking, @TeamCode
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
	
		--Round of 16
	
		--Winners Group C	Match 50	Runners-up Group D
		IF (@RoundCode = 'C' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 50
		END	
		ELSE IF (@RoundCode = 'D' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 50
		END

		--Winners Group A	Match 49	Runners-up Group B
		ELSE IF (@RoundCode = 'A' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 49
		END	
		ELSE IF (@RoundCode = 'B' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 49
		END

		--Winners Group B	Match 51	Runners-up Group A
		ELSE IF (@RoundCode = 'B' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 51
		END	
		ELSE IF (@RoundCode = 'A' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 51
		END

		--Winners Group D	Match 52	Runners-up Group C
		ELSE IF (@RoundCode = 'D' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 52
		END	
		ELSE IF (@RoundCode = 'C' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 52
		END

		--Winners Group E	Match 53	Runners-up Group F
		ELSE IF (@RoundCode = 'E' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 53
		END	
		ELSE IF (@RoundCode = 'F' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 53
		END

		--Winners Group G	Match 54	Runners-up Group H
		ELSE IF (@RoundCode = 'G' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 54
		END	
		ELSE IF (@RoundCode = 'H' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 54
		END

		--Winners Group F	Match 55	Runners-up Group E
		ELSE IF (@RoundCode = 'F' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 55
		END	
		ELSE IF (@RoundCode = 'E' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 55
		END

		--Winners Group H	Match 56	Runners-up Group G
		ELSE IF (@RoundCode = 'H' AND @GroupRanking = 1) 
		BEGIN
			UPDATE g
			SET team_1_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 56
		END	
		ELSE IF (@RoundCode = 'G' AND @GroupRanking = 2)
		BEGIN
			UPDATE g
			SET team_2_code = @TeamCode
			FROM wc_game g
			WHERE g.tournament_code = @TournamentCode
			AND g.round_number = @RoundNumber + 1
			AND g.game_number = 56
		END
		

		FETCH NEXT FROM Cursor1 INTO @RoundCode, @GroupRanking, @TeamCode
	END

	CLOSE Cursor1
	DEALLOCATE Cursor1

	--
	/*
	SELECT * 
	FROM wc_group_stage
	WHERE tournament_code = @TournamentCode
	AND round_number = @RoundNumber
	AND round_code = @RoundCode*/

	--SELECT * 
	--FROM wc_tournament_format_round
	--WHERE format_round_code = @format_round_code
	--wc_tournament_format

	DROP TABLE #tmp_teams
END