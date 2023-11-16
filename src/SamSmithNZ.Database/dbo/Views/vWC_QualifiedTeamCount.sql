CREATE VIEW [dbo].[vWC_QualifiedTeamCount]
AS
SELECT t.tournament_code, COUNT(te.tournament_code) AS qualified_teams_count
FROM wc_tournament t
LEFT JOIN wc_tournament_team_entry te ON te.tournament_code = t.tournament_code
GROUP BY t.tournament_code