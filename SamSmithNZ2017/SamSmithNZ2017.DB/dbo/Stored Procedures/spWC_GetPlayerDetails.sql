
CREATE PROCEDURE [dbo].[spWC_GetPlayerDetails]
	@player_code smallint
AS
SELECT p.player_code, p.player_name, p.number, p.position, p.date_of_birth, 
	count(go.goal_time) as world_cup_goals
FROM wc_player p
RIGHT OUTER JOIN wc_goal go ON go.player_code = p.player_code
WHERE p.player_code = @player_code
GROUP BY p.player_code, p.player_name, p.number, p.position, p.date_of_birth