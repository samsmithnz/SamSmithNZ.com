CREATE PROCEDURE [dbo].[FB_GetMigratePlayoffs]
	@TournamentCode INT
AS
BEGIN
	SELECT s.tournament_code AS TournamentCode,
		round_code AS RoundCode,
		game_number as GameNumber,
		team_1_prereq as Team1Prereq,
		team_2_prereq as Team2Prereq
	FROM wc_tournament_format_playoff_setup s
	WHERE s.tournament_code = @TournamentCode
	ORDER BY game_number
END
GO