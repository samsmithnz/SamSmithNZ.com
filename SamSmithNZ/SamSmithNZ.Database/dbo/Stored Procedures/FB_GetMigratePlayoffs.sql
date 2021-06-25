CREATE PROCEDURE [dbo].[FB_GetMigratePlayoffs]
	@TournamentCode INT
AS
BEGIN
	SELECT * 
	FROM wc_tournament_format_playoff_setup s
	WHERE s.tournament_code = @TournamentCode
	ORDER BY game_number
END
GO