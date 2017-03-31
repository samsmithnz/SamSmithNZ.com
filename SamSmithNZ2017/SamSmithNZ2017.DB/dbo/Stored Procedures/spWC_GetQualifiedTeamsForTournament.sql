CREATE PROCEDURE [dbo].[spWC_GetQualifiedTeamsForTournament]
	@tournament_code smallint
AS
SELECT t.team_code, t.team_name, t.flag_name, 
	r.region_code, r.region_abbrev as region_name, 
	isnull(te.fifa_ranking,0) as fifa_ranking, 
	te.coach_name, isnull(ct.flag_name,'') as coach_nationality
FROM wc_tournament_team_entry te
INNER JOIN wc_team t ON te.team_code = t.team_code
INNER JOIN wc_region r ON t.region_code = r.region_code
LEFT OUTER JOIN wc_team ct ON ct.team_name = te.coach_nationality
WHERE te.tournament_code = @tournament_code
ORDER BY t.team_name