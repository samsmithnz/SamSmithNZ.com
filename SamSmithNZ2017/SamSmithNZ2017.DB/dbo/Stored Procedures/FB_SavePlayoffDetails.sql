CREATE PROCEDURE [dbo].[FB_SavePlayoffDetails]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10)
AS
SET NOCOUNT ON

DECLARE @number_of_teams_in_group INT

SELECT @number_of_teams_in_group = tfr.number_of_teams_in_group 
FROM wc_tournament t
JOIN wc_tournament_format tf ON t.format_code = tf.format_code
JOIN wc_tournament_format_round tfr ON tfr.format_round_code = tf.round_2_format_code
WHERE tournament_code = @TournamentCode

DECLARE @game_count INT
SELECT @game_count = COUNT(*) 
FROM wc_game
WHERE tournament_code = @TournamentCode
and round_number = @RoundNumber
and round_code = @RoundCode

--If the number of games doesn't audit correctly, add some new games
IF (@number_of_teams_in_group/2 <> @game_count)
BEGIN
	SELECT g.tournament_code,
		g.game_code,
		g.game_number,
		g.game_time,
		g.[location],
		g.round_code,
		g.round_number,
		g.team_1_code,
		g.team_1_extra_time_score,
		g.team_1_normal_time_score,
		g.team_1_penalties_score,
		g.team_2_code,
		g.team_2_extra_time_score,
		g.team_2_normal_time_score,
		g.team_2_penalties_score
	FROM wc_game g
	WHERE tournament_code = @TournamentCode
	and round_number = @RoundNumber
	and round_code = @RoundCode
END
ELSE
BEGIN
	Select 0
END

----Get the number of teams to set for each group
--DECLARE @TeamsFromEachGroupThatAdvance INT
--DECLARE @TeamsFromAllGroupsThatAdvance INT
--DECLARE @TotalNumberOfTeamsThatAdvanceFromStage INT
--DECLARE @GroupAdvancementDifference INT
--IF (@RoundNumber = 1)
--BEGIN
--	SELECT @TeamsFromAllGroupsThatAdvance = (tf.r1_number_of_teams_from_group_that_advance * tf.r1_number_of_groups_in_round),
--		@TotalNumberOfTeamsThatAdvanceFromStage = tf.r1_total_number_of_teams_that_advance,
--		@TeamsFromEachGroupThatAdvance = tf.r1_number_of_teams_from_group_that_advance
--	FROM wc_tournament t 
--	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
--	WHERE t.tournament_code = @TournamentCode
--END
--ELSE IF (@RoundNumber = 2)
--BEGIN
--	SELECT @TeamsFromAllGroupsThatAdvance = (tf.r2_number_of_teams_from_group_that_advance * tf.r2_number_of_groups_in_round),
--		@TotalNumberOfTeamsThatAdvanceFromStage = tf.r2_total_number_of_teams_that_advance,
--		@TeamsFromEachGroupThatAdvance = tf.r2_number_of_teams_from_group_that_advance
--	FROM wc_tournament t 
--	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
--	WHERE t.tournament_code = @TournamentCode
--END
--ELSE IF (@RoundNumber = 3)
--BEGIN
--	SELECT @TeamsFromAllGroupsThatAdvance = (tf.r3_number_of_teams_from_group_that_advance * tf.r3_number_of_groups_in_round),
--		@TotalNumberOfTeamsThatAdvanceFromStage = tf.r3_total_number_of_teams_that_advance,
--		@TeamsFromEachGroupThatAdvance = tf.r3_number_of_teams_from_group_that_advance
--	FROM wc_tournament t 
--	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
--	WHERE t.tournament_code = @TournamentCode
--END

--IF (@TeamsFromAllGroupsThatAdvance <> @TotalNumberOfTeamsThatAdvanceFromStage)
--BEGIN
--    SELECT @GroupAdvancementDifference = @TotalNumberOfTeamsThatAdvanceFromStage - @TeamsFromAllGroupsThatAdvance
--END

--CREATE TABLE #tmp_teams (team_code INT)

----get all teams affected
--INSERT INTO #tmp_teams
--SELECT team_1_code
--FROM wc_game
--WHERE tournament_code = @TournamentCode
--and round_number = @RoundNumber
--and round_code = @RoundCode
--UNION
--SELECT team_2_code
--FROM wc_game
--WHERE tournament_code = @TournamentCode
--and round_number = @RoundNumber
--and round_code = @RoundCode

----Clean up the group table
--DELETE FROM wc_group_stage
--WHERE tournament_code = @TournamentCode
--and round_number = @RoundNumber
--and round_code = @RoundCode

----Build the Group Table
--DECLARE @team_code INT
--DECLARE Cursor1 CURSOR LOCAL FOR
--	SELECT DISTINCT team_code
--	FROM #tmp_teams 

--OPEN Cursor1

----loop through all the teams, getting the record of each team, inserting in default placing.
--FETCH NEXT FROM Cursor1 INTO @team_code
--WHILE (@@FETCH_STATUS <> -1)
--BEGIN

--	--select @team_code
--	INSERT INTO wc_group_stage
--	SELECT @RoundNumber, @RoundCode, @TournamentCode, @team_code, 
--		dbo.fnWC_GetTeamGamesPlayed(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --played
--		dbo.fnWC_GetTeamWins(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --W
--		dbo.fnWC_GetTeamDraws(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --D
--		dbo.fnWC_GetTeamLosses(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --L
--		dbo.fnWC_GetTeamGoalsFor(@TournamentCode, @RoundNumber, @RoundCode, @team_code), --GF
--		dbo.fnWC_GetTeamGoalsAgainst (@TournamentCode, @RoundNumber, @RoundCode, @team_code), --GA
--		dbo.fnWC_GetTeamGoalsFor(@TournamentCode, @RoundNumber, @RoundCode, @team_code) - dbo.fnWC_GetTeamGoalsAgainst (@TournamentCode, @RoundNumber, @RoundCode, @team_code), --GD
--		(dbo.fnWC_GetTeamWins(@TournamentCode, @RoundNumber, @RoundCode, @team_code) * dbo.fnWC_GetPointsForAWin(@TournamentCode)) + (dbo.fnWC_GetTeamDraws(@TournamentCode, @RoundNumber, @RoundCode, @team_code) * 1), --Pts
--		0, 0

--	--select * From wc_group_stage where tournament_code = 16

--	FETCH NEXT FROM Cursor1 INTO @team_code
--END

--CLOSE Cursor1
--DEALLOCATE Cursor1

----select * From wc_group_stage where tournament_code = @TournamentCode

----Set the Group Ranking for each group
--DECLARE @group_ranking INT
--SELECT @group_ranking = 0

--DECLARE Cursor1 CURSOR LOCAL FOR
--	SELECT gs.team_code
--	FROM wc_group_stage gs
--	WHERE tournament_code = @TournamentCode
--	and round_number = @RoundNumber
--	and round_code = @RoundCode
--	ORDER BY points DESC, goal_difference DESC, goals_for DESC

--OPEN Cursor1

----loop through all the items
--FETCH NEXT FROM Cursor1 INTO @team_code
--WHILE (@@FETCH_STATUS <> -1)
--BEGIN
--	SELECT @group_ranking = @group_ranking + 1
	
--	DECLARE @has_qualified_for_next_round bit
--	IF (@group_ranking <= @TeamsFromEachGroupThatAdvance)
--	BEGIN
--		SELECT @has_qualified_for_next_round = 1
--	END
--	ELSE
--	BEGIN
--		SELECT @has_qualified_for_next_round = 0
--	END

--	UPDATE wc_group_stage
--	SET group_ranking = @group_ranking, has_qualified_for_next_round = @has_qualified_for_next_round
--	WHERE tournament_code = @TournamentCode
--	and round_number = @RoundNumber
--	and round_code = @RoundCode
--	and team_code = @team_code

--	FETCH NEXT FROM Cursor1 INTO @team_code
--END

--CLOSE Cursor1
--DEALLOCATE Cursor1

--/*
--SELECT t.team_name, gs.*
--FROM wc_group_stage gs
--JOIN wc_team t ON gs.team_code = t.team_code
--WHERE tournament_code = @TournamentCode
--and round_number = @RoundNumber
--and has_qualified_for_next_round = 0
--ORDER BY points DESC, goal_difference DESC, goals_for DESC
--*/

--IF (@TeamsFromAllGroupsThatAdvance = (SELECT COUNT(*) FROM wc_group_stage gs
--										WHERE tournament_code = @TournamentCode
--										and round_number = @RoundNumber
--										and group_ranking <= @TeamsFromEachGroupThatAdvance))
--BEGIN
--		UPDATE wc_group_stage
--		SET has_qualified_for_next_round = 0
--		WHERE tournament_code = @TournamentCode
--		and round_number = @RoundNumber
--		and team_code = @team_code
--		and group_ranking <= @TeamsFromEachGroupThatAdvance
	

--	--Finally set any teams that have qualified for the next round in 3rd place positions
--	SELECT @group_ranking = 0
--	DECLARE Cursor1 CURSOR LOCAL FOR
--		SELECT gs.team_code
--		FROM wc_group_stage gs
--		WHERE tournament_code = @TournamentCode
--		and round_number = @RoundNumber
--		and has_qualified_for_next_round = 0
--		ORDER BY group_ranking, points DESC, goal_difference DESC, goals_for DESC

--	OPEN Cursor1

--	--loop through all the items
--	FETCH NEXT FROM Cursor1 INTO @team_code
--	WHILE (@@FETCH_STATUS <> -1 and @group_ranking < @GroupAdvancementDifference)
--	BEGIN
--		SELECT @group_ranking = @group_ranking + 1
		
--		UPDATE wc_group_stage
--		SET has_qualified_for_next_round = 1
--		WHERE tournament_code = @TournamentCode
--		and round_number = @RoundNumber
--		and team_code = @team_code

--		FETCH NEXT FROM Cursor1 INTO @team_code
--	END

--	CLOSE Cursor1
--	DEALLOCATE Cursor1
--END

----
--/*
--SELECT * 
--FROM wc_group_stage
--WHERE tournament_code = @TournamentCode
--and round_number = @RoundNumber
--and round_code = @RoundCode*/

----SELECT * 
----FROM wc_tournament_format_round
----WHERE format_round_code = @format_round_code
----wc_tournament_format

--DROP TABLE #tmp_teams