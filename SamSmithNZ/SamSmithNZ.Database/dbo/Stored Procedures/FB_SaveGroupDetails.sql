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
	DECLARE @FormatCode INT
	IF (@RoundNumber = 1)
	BEGIN
		SELECT @TeamsFromAllGroupsThatAdvance = (tf.r1_number_of_teams_from_group_that_advance * tf.r1_number_of_groups_in_round),
			@TotalNumberOfTeamsThatAdvanceFromStage = tf.r1_total_number_of_teams_that_advance,
			@TeamsFromEachGroupThatAdvance = tf.r1_number_of_teams_from_group_that_advance,
			@FormatCode = t.format_code
		FROM wc_tournament t 
		JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
		WHERE t.tournament_code = @TournamentCode
	END
	ELSE IF (@RoundNumber = 2)
	BEGIN
		SELECT @TeamsFromAllGroupsThatAdvance = (tf.r2_number_of_teams_from_group_that_advance * tf.r2_number_of_groups_in_round),
			@TotalNumberOfTeamsThatAdvanceFromStage = tf.r2_total_number_of_teams_that_advance,
			@TeamsFromEachGroupThatAdvance = tf.r2_number_of_teams_from_group_that_advance,
			@FormatCode = t.format_code
		FROM wc_tournament t 
		JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
		WHERE t.tournament_code = @TournamentCode
	END
	ELSE IF (@RoundNumber = 3)
	BEGIN
		SELECT @TeamsFromAllGroupsThatAdvance = (tf.r3_number_of_teams_from_group_that_advance * tf.r3_number_of_groups_in_round),
			@TotalNumberOfTeamsThatAdvanceFromStage = tf.r3_total_number_of_teams_that_advance,
			@TeamsFromEachGroupThatAdvance = tf.r3_number_of_teams_from_group_that_advance,
			@FormatCode = t.format_code
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
	
	--Setup any teams that have qualified for the next round in 3rd place positions
	IF (@TeamsFromAllGroupsThatAdvance < @TotalNumberOfTeamsThatAdvanceFromStage)
	BEGIN
		SELECT @team_code = gs.team_code
		FROM wc_group_stage gs
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		AND round_code = @RoundCode
		AND group_ranking = 3

		--Don't include a team here - just delete anyone from this group
		DELETE FROM wc_group_stage_third_placed_teams
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		AND round_code = @RoundCode

		--Insert this new team into the third placed teams
		INSERT INTO wc_group_stage_third_placed_teams
		SELECT round_number, round_code, tournament_code, team_code, 
			played, wins, draws, losses, goals_for, goals_against, goal_difference, 
			points, has_qualified_for_next_round, 0 
		FROM wc_group_stage
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		AND round_code = @RoundCode
		AND team_code = @team_code	

		SELECT @group_ranking = 0

		DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT gs.team_code
		FROM wc_group_stage_third_placed_teams gs
		WHERE tournament_code = @TournamentCode
		AND round_number = @RoundNumber
		ORDER BY points DESC, goal_difference DESC, goals_for DESC

		OPEN Cursor1

		--loop through all the items
		FETCH NEXT FROM Cursor1 INTO @team_code
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			SELECT @group_ranking = @group_ranking + 1
	
			IF (@group_ranking <= (@TotalNumberOfTeamsThatAdvanceFromStage - @TeamsFromAllGroupsThatAdvance))
			BEGIN
				SELECT @has_qualified_for_next_round = 1
			END
			ELSE
			BEGIN
				SELECT @has_qualified_for_next_round = 0
			END

			UPDATE wc_group_stage_third_placed_teams
			SET group_ranking = @group_ranking, has_qualified_for_next_round = @has_qualified_for_next_round
			WHERE tournament_code = @TournamentCode
			AND round_number = @RoundNumber
			AND team_code = @team_code

			FETCH NEXT FROM Cursor1 INTO @team_code
		END

		CLOSE Cursor1
		DEALLOCATE Cursor1

		--Now update the original groups
		UPDATE g
		SET g.has_qualified_for_next_round = t.has_qualified_for_next_round
		FROM wc_group_stage g
		JOIN wc_group_stage_third_placed_teams t ON g.tournament_code = t.tournament_code
			AND g.round_number = t.round_number
			AND g.team_code = t.team_code
		WHERE t.tournament_code = @TournamentCode
		AND t.round_number = @RoundNumber 

	END
	

	IF (@FormatCode = 1) --Current round is 8 groups, top 2 teams from each group goes through
	BEGIN
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
	END

	DROP TABLE #tmp_teams
END
GO
/*
exec FB_SaveGroupDetails 316, 1, 'A'
exec FB_SaveGroupDetails 316, 1, 'B'
exec FB_SaveGroupDetails 316, 1, 'C'
exec FB_SaveGroupDetails 316, 1, 'D'
exec FB_SaveGroupDetails 316, 1, 'E'
exec FB_SaveGroupDetails 316, 1, 'F'
select * From wc_group_stage_third_placed_teams order by group_ranking
*/