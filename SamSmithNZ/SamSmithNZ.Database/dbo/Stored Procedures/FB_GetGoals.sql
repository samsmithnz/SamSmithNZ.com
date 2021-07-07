CREATE PROCEDURE [dbo].[FB_GetGoals]
	@GameCode INT = NULL
AS
SELECT g.goal_code AS GoalCode, 
	g.game_code AS GameCode,
	ISNULL(p.player_code,0) AS PlayerCode, 
	ISNULL(p.player_name,'Unknown') AS PlayerName, 
	ISNULL(g.goal_time,0) AS GoalTime, 
	ISNULL(g.injury_time,0) AS InjuryTime, 
	g.is_penalty AS IsPenalty, 
	g.is_own_goal AS IsOwnGoal
FROM wc_goal g
LEFT JOIN wc_player p ON g.player_code = p.player_code
WHERE (game_code = @GameCode OR @GameCode IS NULL)
ORDER BY g.goal_time, g.injury_time