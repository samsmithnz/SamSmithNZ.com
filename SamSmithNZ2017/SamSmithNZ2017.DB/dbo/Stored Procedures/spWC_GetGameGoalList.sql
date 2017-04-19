CREATE PROCEDURE [dbo].[spWC_GetGameGoalList]
	@game_code smallint
AS
SELECT g.goal_code, p.player_code, p.player_name, 
	isnull(g.goal_time,0) as goal_time, isnull(g.injury_time,0) as injury_time, 
	g.is_penalty, g.is_own_goal
FROM wc_goal g
JOIN wc_player p ON g.player_code = p.player_code
WHERE game_code = @game_code
ORDER BY g.goal_time, g.injury_time