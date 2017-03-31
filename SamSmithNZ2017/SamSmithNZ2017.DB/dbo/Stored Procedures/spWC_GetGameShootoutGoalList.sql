CREATE PROCEDURE [dbo].[spWC_GetGameShootoutGoalList]
	@game_code smallint
AS
SELECT ps.penalty_code, p.player_code, p.player_name, 
	ps.penalty_order, ps.scored
FROM wc_penalty_shootout ps
INNER JOIN wc_player p ON ps.player_code = p.player_code
WHERE game_code = @game_code
ORDER BY ps.penalty_order