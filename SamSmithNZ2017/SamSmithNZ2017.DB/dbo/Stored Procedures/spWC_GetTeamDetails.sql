CREATE PROCEDURE [dbo].[spWC_GetTeamDetails]
	@team_code smallint
AS

SELECT t.team_code, t.team_name, t.flag_name, isnull(te.fifa_ranking,0) as fifa_ranking, 
te.coach_name, isnull(ct.flag_name,'') as coach_nationality,
te.tournament_code, t2.name as tournament_name
FROM wc_tournament_team_entry te
JOIN wc_team t ON te.team_code = t.team_code
JOIN wc_tournament t2 ON t2.tournament_code = te.tournament_code
LEFT OUTER JOIN wc_team ct ON ct.team_name = te.coach_nationality
WHERE te.team_code = @team_code