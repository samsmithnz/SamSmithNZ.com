CREATE VIEW [dbo].[vWC_PlayerGoalCount]
AS
SELECT t.tournament_code, count(go.game_code) as player_goal_count
FROM wc_tournament t
LEFT OUTER JOIN wc_game g ON g.tournament_code = t.tournament_code
LEFT OUTER JOIN wc_goal go ON go.game_code = g.game_code
GROUP BY t.tournament_code