CREATE PROCEDURE [dbo].[spWC_ProcessGroupDetails]
	@tournament_code smallint,
	@round_number smallint,
	@round_code varchar(10)
AS
SET NOCOUNT ON

--Get the number of teams to set for each group
DECLARE @TeamsFromEachGroupThatAdvance smallint
DECLARE @TeamsFromAllGroupsThatAdvance smallint
DECLARE @TotalNumberOfTeamsThatAdvanceFromStage smallint
DECLARE @GroupAdvancementDifference smallint
IF (@round_number = 1)
BEGIN
	SELECT @TeamsFromAllGroupsThatAdvance = (tf.r1_number_of_teams_from_group_that_advance * tf.r1_number_of_groups_in_round),
		@TotalNumberOfTeamsThatAdvanceFromStage = tf.r1_total_number_of_teams_that_advance,
		@TeamsFromEachGroupThatAdvance = tf.r1_number_of_teams_from_group_that_advance
	FROM wc_tournament t 
	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
	WHERE t.tournament_code = @tournament_code
END
ELSE IF (@round_number = 2)
BEGIN
	SELECT @TeamsFromAllGroupsThatAdvance = (tf.r2_number_of_teams_from_group_that_advance * tf.r2_number_of_groups_in_round),
		@TotalNumberOfTeamsThatAdvanceFromStage = tf.r2_total_number_of_teams_that_advance,
		@TeamsFromEachGroupThatAdvance = tf.r2_number_of_teams_from_group_that_advance
	FROM wc_tournament t 
	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
	WHERE t.tournament_code = @tournament_code
END
ELSE IF (@round_number = 3)
BEGIN
	SELECT @TeamsFromAllGroupsThatAdvance = (tf.r3_number_of_teams_from_group_that_advance * tf.r3_number_of_groups_in_round),
		@TotalNumberOfTeamsThatAdvanceFromStage = tf.r3_total_number_of_teams_that_advance,
		@TeamsFromEachGroupThatAdvance = tf.r3_number_of_teams_from_group_that_advance
	FROM wc_tournament t 
	JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
	WHERE t.tournament_code = @tournament_code
END

IF (@TeamsFromAllGroupsThatAdvance <> @TotalNumberOfTeamsThatAdvanceFromStage)
BEGIN
    SELECT @GroupAdvancementDifference = @TotalNumberOfTeamsThatAdvanceFromStage - @TeamsFromAllGroupsThatAdvance
END

CREATE TABLE #tmp_teams (team_code smallint)

--get all teams affected
INSERT INTO #tmp_teams
SELECT team_1_code
FROM wc_game
WHERE tournament_code = @tournament_code
and round_number = @round_number
and round_code = @round_code
UNION
SELECT team_2_code
FROM wc_game
WHERE tournament_code = @tournament_code
and round_number = @round_number
and round_code = @round_code

--Clean up the group table
DELETE FROM wc_group_stage
WHERE tournament_code = @tournament_code
and round_number = @round_number
and round_code = @round_code

--Build the Group Table
DECLARE @team_code smallint
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
	SELECT @round_number, @round_code, @tournament_code, @team_code, 
		dbo.fnWC_GetTeamGamesPlayed(@tournament_code, @round_number, @round_code, @team_code), --played
		dbo.fnWC_GetTeamWins(@tournament_code, @round_number, @round_code, @team_code), --W
		dbo.fnWC_GetTeamDraws(@tournament_code, @round_number, @round_code, @team_code), --D
		dbo.fnWC_GetTeamLosses(@tournament_code, @round_number, @round_code, @team_code), --L
		dbo.fnWC_GetTeamGoalsFor(@tournament_code, @round_number, @round_code, @team_code), --GF
		dbo.fnWC_GetTeamGoalsAgainst (@tournament_code, @round_number, @round_code, @team_code), --GA
		dbo.fnWC_GetTeamGoalsFor(@tournament_code, @round_number, @round_code, @team_code) - dbo.fnWC_GetTeamGoalsAgainst (@tournament_code, @round_number, @round_code, @team_code), --GD
		(dbo.fnWC_GetTeamWins(@tournament_code, @round_number, @round_code, @team_code) * dbo.fnWC_GetPointsForAWin(@tournament_code)) + (dbo.fnWC_GetTeamDraws(@tournament_code, @round_number, @round_code, @team_code) * 1), --Pts
		0, 0

	--select * From wc_group_stage where tournament_code = 16

	FETCH NEXT FROM Cursor1 INTO @team_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

--select * From wc_group_stage where tournament_code = @tournament_code

--Set the Group Ranking for each group
DECLARE @group_ranking smallint
SELECT @group_ranking = 0

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT gs.team_code
	FROM wc_group_stage gs
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
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
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_code = @team_code

	FETCH NEXT FROM Cursor1 INTO @team_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

/*
SELECT t.team_name, gs.*
FROM wc_group_stage gs
JOIN wc_team t ON gs.team_code = t.team_code
WHERE tournament_code = @tournament_code
and round_number = @round_number
and has_qualified_for_next_round = 0
ORDER BY points DESC, goal_difference DESC, goals_for DESC
*/

IF (@TeamsFromAllGroupsThatAdvance = (SELECT COUNT(*) FROM wc_group_stage gs
										WHERE tournament_code = @tournament_code
										and round_number = @round_number
										and group_ranking <= @TeamsFromEachGroupThatAdvance))
BEGIN
		UPDATE wc_group_stage
		SET has_qualified_for_next_round = 0
		WHERE tournament_code = @tournament_code
		and round_number = @round_number
		and team_code = @team_code
		and group_ranking <= @TeamsFromEachGroupThatAdvance
	

	--Finally set any teams that have qualified for the next round in 3rd place positions
	SELECT @group_ranking = 0
	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT gs.team_code
		FROM wc_group_stage gs
		WHERE tournament_code = @tournament_code
		and round_number = @round_number
		and has_qualified_for_next_round = 0
		ORDER BY group_ranking, points DESC, goal_difference DESC, goals_for DESC

	OPEN Cursor1

	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @team_code
	WHILE (@@FETCH_STATUS <> -1 and @group_ranking < @GroupAdvancementDifference)
	BEGIN
		SELECT @group_ranking = @group_ranking + 1
		
		UPDATE wc_group_stage
		SET has_qualified_for_next_round = 1
		WHERE tournament_code = @tournament_code
		and round_number = @round_number
		and team_code = @team_code

		FETCH NEXT FROM Cursor1 INTO @team_code
	END

	CLOSE Cursor1
	DEALLOCATE Cursor1
END

--
/*
SELECT * 
FROM wc_group_stage
WHERE tournament_code = @tournament_code
and round_number = @round_number
and round_code = @round_code*/

--SELECT * 
--FROM wc_tournament_format_round
--WHERE format_round_code = @format_round_code
--wc_tournament_format

DROP TABLE #tmp_teams