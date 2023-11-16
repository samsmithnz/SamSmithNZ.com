CREATE PROCEDURE [dbo].[FB_GetPenaltyShootoutGoals]
	@GameCode INT
AS
SELECT ps.penalty_code AS PenaltyCode, 
	ps.game_code as GameCode,
	p.player_code AS PlayerCode, 
	p.player_name AS PlayerName, 
	ps.penalty_order AS PenaltyOrder, 
	ps.scored AS Scored
FROM wc_penalty_shootout ps
JOIN wc_player p ON ps.player_code = p.player_code
WHERE game_code = @GameCode
ORDER BY ps.penalty_order