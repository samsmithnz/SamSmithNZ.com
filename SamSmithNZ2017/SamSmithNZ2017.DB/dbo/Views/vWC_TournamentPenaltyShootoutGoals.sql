CREATE VIEW [dbo].[vWC_TournamentPenaltyShootoutGoals]
AS
SELECT t.tournament_code,
sum(isnull(g.team_1_penalties_score,0)) + 
sum(isnull(g.team_2_penalties_score,0)) as total_penalty_shootout_goals
FROM wc_tournament t
LEFT OUTER JOIN wc_game g ON t.tournament_code = g.tournament_code
GROUP BY t.tournament_code