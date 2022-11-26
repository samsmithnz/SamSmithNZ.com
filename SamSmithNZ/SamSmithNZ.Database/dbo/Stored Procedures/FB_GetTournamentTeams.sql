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
		ISNULL(te.current_elo_rating,0) AS CurrentEloRating,
		te.coach_name AS CoachName, 
		ISNULL(ct.flag_name,'') AS CoachNationalityFlagName,
		(SELECT COUNT(*) FROM wc_game g1 WHERE g1.tournament_code = te.tournament_code AND g1.team_1_code = t.team_code AND NOT g1.team_1_normal_time_score IS NULL) + 
		(SELECT COUNT(*) FROM wc_game g1 WHERE g1.tournament_code = te.tournament_code AND g1.team_2_code = t.team_code AND NOT g1.team_2_normal_time_score IS NULL) AS GamesCompleted
	FROM wc_tournament_team_entry te
	JOIN wc_team t ON te.team_code = t.team_code
	JOIN wc_region r ON t.region_code = r.region_code
	LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
	WHERE te.tournament_code = @TournamentCode
	ORDER BY t.team_name
END
GO