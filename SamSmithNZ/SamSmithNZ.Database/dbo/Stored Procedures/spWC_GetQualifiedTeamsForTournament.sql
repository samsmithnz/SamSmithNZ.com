CREATE PROCEDURE [dbo].[spWC_GetQualifiedTeamsForTournament]
	@tournament_code INT
AS
SELECT t.team_code, t.team_name, t.flag_name, 
	r.region_code, r.region_abbrev AS region_name, 
	ISNULL(te.fifa_ranking,0) AS fifa_ranking, 
	te.coach_name, ISNULL(ct.flag_name,'') AS coach_nationality
FROM wc_tournament_team_entry te
JOIN wc_team t ON te.team_code = t.team_code
JOIN wc_region r ON t.region_code = r.region_code
LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
WHERE te.tournament_code = @tournament_code
ORDER BY t.team_name