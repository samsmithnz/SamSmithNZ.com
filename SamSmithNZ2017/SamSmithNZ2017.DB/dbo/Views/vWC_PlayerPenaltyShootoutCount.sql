CREATE VIEW [dbo].[vWC_PlayerPenaltyShootoutCount]
AS
SELECT t.tournament_code, COUNT(po.game_code) AS player_penalty_shootout_goal_count
FROM wc_tournament t
LEFT JOIN wc_game g ON g.tournament_code = t.tournament_code
LEFT JOIN wc_penalty_shootout po ON po.game_code = g.game_code AND scored = 1
GROUP BY t.tournament_code