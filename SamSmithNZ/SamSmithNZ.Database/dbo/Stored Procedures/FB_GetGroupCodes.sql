CREATE PROCEDURE [dbo].[FB_GetGroupCodes]
	@TournamentCode INT,
	@RoundNumber INT
AS
BEGIN

	DECLARE @IsLastRound BIT
	DECLARE @RoundFormatCode INT

	IF (@RoundNumber = 1)
	BEGIN
		SELECT @RoundFormatCode = tf.round_2_format_code
		FROM wc_tournament t
		JOIN wc_tournament_format tf ON t.format_code = tf.format_code 
		WHERE tournament_code = @TournamentCode
	END
	ELSE IF (@RoundNumber = 2)
	BEGIN
		SELECT @RoundFormatCode = tf.round_3_format_code 
		FROM wc_tournament t
		JOIN wc_tournament_format tf ON t.format_code = tf.format_code 
		WHERE tournament_code = @TournamentCode

		IF (@RoundFormatCode = 0)
		BEGIN
			SELECT @IsLastRound = 1
		END
	END
	
	IF (@RoundNumber = 3 OR @RoundFormatCode = 0)
	BEGIN
		SELECT @IsLastRound = 1
	END
	ELSE
	BEGIN
		SELECT @IsLastRound = 0
	END

	DECLARE @ThirdPlaceTeams INT
	SELECT @ThirdPlaceTeams = COUNT(*) 
	FROM wc_group_stage_third_placed_teams
	WHERE tournament_code = @TournamentCode
	AND round_number = @RoundNumber

	CREATE TABLE #results (RoundCode VARCHAR(10), IsLastRound BIT)

	INSERT INTO #results
	SELECT DISTINCT gs.round_code AS RoundCode, 
		@IsLastRound AS IsLastRound
	FROM wc_group_stage gs 
	WHERE gs.tournament_code = @TournamentCode
	AND gs.round_number = @RoundNumber	

	IF (@ThirdPlaceTeams > 0)
	BEGIN
		INSERT INTO #results
		SELECT 'z3' AS RoundCode, --z to sort last, 3 to indicate it's the third placed teams
			@IsLastRound AS IsLastRound
	END

	DECLARE @NumberOfGroupsInRound INT
	SELECT @NumberOfGroupsInRound = r.number_of_groups_in_round 
	FROM wc_tournament t
	JOIN wc_tournament_format tf ON t.format_code = tf.format_code
	JOIN wc_tournament_format_round r ON tf.round_1_format_code = r.format_round_code 
	WHERE t.tournament_code = @TournamentCode

	IF ((SELECT COUNT(*) FROM #results) < @NumberOfGroupsInRound)
	BEGIN
		--Loop to dynamically build the group codes.
		DECLARE @counter INT
		SELECT @counter = 0 + ISNULL((SELECT COUNT(*) FROM #results),0)

		WHILE (@counter < @NumberOfGroupsInRound)
		BEGIN  
			INSERT INTO #results
			SELECT CHAR(65 + @Counter), 0
			SELECT @counter = @counter + 1
	
			IF (@counter >= @NumberOfGroupsInRound)
			BEGIN
				BREAK  
			END
			ELSE
			BEGIN
				CONTINUE  
			END
		END 
	END

	SELECT RoundCode, IsLastRound
	FROM #results
	ORDER BY RoundCode
END
GO