CREATE PROCEDURE [dbo].[FB_GetTournamentTeams]
	@TournamentCode INT
AS
BEGIN
	SELECT 0 AS Placing, 
		t.team_code AS TeamCode, 
		t.team_name AS TeamName, 
		t.flag_name AS FlagName, 
		r.region_code AS RegionCode, 
		r.region_abbrev AS RegionName, 
		ISNULL(te.fifa_ranking,0) AS FifaRanking, 
		ISNULL(te.starting_elo_rating,0) AS StartingEloRating,
		te.coach_name AS CoachName, 
		ISNULL(ct.flag_name,'') AS CoachNationalityFlagName,
		ISNULL(e.elo_rating,0) AS EloRating
	FROM wc_tournament_team_entry te
	JOIN wc_team t ON te.team_code = t.team_code
	JOIN wc_region r ON t.region_code = r.region_code
	LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
	LEFT JOIN wc_tournament_team_elo_rating e ON e.tournament_code = te.tournament_code AND e.team_code = te.team_code
	WHERE te.tournament_code = @TournamentCode
	ORDER BY t.team_name
END