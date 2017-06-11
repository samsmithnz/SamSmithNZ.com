CREATE PROCEDURE [dbo].[FB_GetGroupCodes]
	@TournamentCode INT,
	@RoundNumber INT
AS
BEGIN
	SELECT DISTINCT gs.round_code AS RoundCode
	FROM wc_group_stage gs 
	WHERE gs.tournament_code = @TournamentCode
	AND gs.round_number = @RoundNumber
	ORDER BY round_code
END