CREATE PROCEDURE [dbo].[spWC_GetTeamDetails]
	@team_code INT
AS

SELECT t.team_code, t.team_name, t.flag_name, ISNULL(te.fifa_ranking,0) AS fifa_ranking, 
te.coach_name, ISNULL(ct.flag_name,'') AS coach_nationality,
te.tournament_code, t2.name AS tournament_name
FROM wc_tournament_team_entry te
JOIN wc_team t ON te.team_code = t.team_code
JOIN wc_tournament t2 ON t2.tournament_code = te.tournament_code
LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
WHERE te.team_code = @team_code