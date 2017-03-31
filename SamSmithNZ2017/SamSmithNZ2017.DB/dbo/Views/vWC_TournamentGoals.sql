CREATE VIEW [dbo].[vWC_TournamentGoals]
AS
SELECT t.tournament_code,
sum(isnull(g.team_1_normal_time_score,0)) + sum(isnull(g.team_1_extra_time_score,0)) + 
sum(isnull(g.team_2_normal_time_score,0)) + sum(isnull(g.team_2_extra_time_score,0)) as total_goals
FROM wc_tournament t
LEFT OUTER JOIN wc_game g ON t.tournament_code = g.tournament_code
GROUP BY t.tournament_code