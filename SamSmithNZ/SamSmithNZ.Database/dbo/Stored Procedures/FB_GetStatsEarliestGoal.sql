CREATE PROCEDURE [dbo].[FB_GetStatsEarliestGoal]
	@TournamentCode INT = NULL
AS
BEGIN
	SELECT top 10 * 
	FROM wc_goal gl
	JOIN wc_game ga ON gl.game_code = ga.game_code
	WHERE (@TournamentCode IS NULL OR ga.tournament_code = @TournamentCode)
	ORDER BY goal_time ASC
END
GO
