CREATE PROCEDURE [dbo].[FB_GetMigratePlayoffs]
	@TournamentCode INT
AS
BEGIN
	SELECT s.tournament_code AS TournamentCode,
		round_code AS RoundCode,
		game_number AS GameNumber,
		team_1_prereq AS Team1Prereq,
		team_2_prereq AS Team2Prereq,
		sort_order AS SortOrder
	FROM wc_tournament_format_playoff_setup s
	WHERE s.tournament_code = @TournamentCode
	ORDER BY sort_order, game_number
END
GO