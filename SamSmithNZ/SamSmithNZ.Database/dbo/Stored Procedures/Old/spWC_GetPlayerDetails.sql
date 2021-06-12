
CREATE PROCEDURE [dbo].[spWC_GetPlayerDetails]
	@player_code INT
AS
SELECT p.player_code, p.player_name, p.number, p.position, p.date_of_birth, 
	COUNT(gl.goal_time) AS world_cup_goals
FROM wc_player p
RIGHT OUTER JOIN wc_goal gl ON gl.player_code = p.player_code
WHERE p.player_code = @player_code
GROUP BY p.player_code, p.player_name, p.number, p.position, p.date_of_birth