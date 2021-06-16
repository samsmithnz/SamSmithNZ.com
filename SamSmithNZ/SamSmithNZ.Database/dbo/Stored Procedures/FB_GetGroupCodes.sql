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

	SELECT RoundCode, IsLastRound
	FROM #results
	ORDER BY RoundCode
END