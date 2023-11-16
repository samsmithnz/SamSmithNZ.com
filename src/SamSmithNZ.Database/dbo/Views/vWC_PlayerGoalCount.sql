CREATE VIEW [dbo].[vWC_PlayerGoalCount]
AS
SELECT t.tournament_code, COUNT(go.game_code) AS player_goal_count
FROM wc_tournament t
LEFT JOIN wc_game g ON g.tournament_code = t.tournament_code
LEFT JOIN wc_goal go ON go.game_code = g.game_code
GROUP BY t.tournament_code