CREATE PROCEDURE [dbo].[FB_GetStatsGoalDistribution]
	@TournamentCode INT = NULL
AS
BEGIN
	SELECT goal_time, injury_time,
		CASE WHEN injury_time IS NOT NULL THEN CONVERT(VARCHAR(10), goal_time) + ' +' + CONVERT(VARCHAR(10), injury_time) ELSE CONVERT(VARCHAR(10), goal_time) END AS goal_and_injury_time,
		COUNT(*)
	FROM wc_goal gl
	JOIN wc_game ga ON gl.game_code = ga.game_code
	WHERE (@TournamentCode IS NULL OR ga.tournament_code = @TournamentCode)
	GROUP BY goal_time, injury_time,
		CASE WHEN injury_time IS NOT NULL THEN CONVERT(VARCHAR(10), goal_time) + ' +' + CONVERT(VARCHAR(10), injury_time) ELSE CONVERT(VARCHAR(10), goal_time) END
	ORDER BY goal_time, injury_time ASC
END
GO
