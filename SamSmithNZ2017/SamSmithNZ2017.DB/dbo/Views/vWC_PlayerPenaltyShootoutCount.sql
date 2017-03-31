CREATE VIEW [dbo].[vWC_PlayerPenaltyShootoutCount]
AS
SELECT t.tournament_code, count(po.game_code) as player_penalty_shootout_goal_count
FROM wc_tournament t
LEFT OUTER JOIN wc_game g ON g.tournament_code = t.tournament_code
LEFT OUTER JOIN wc_penalty_shootout po ON po.game_code = g.game_code AND scored = 1
GROUP BY t.tournament_code