CREATE PROCEDURE [dbo].[FB_ResetTournament]
	@TournamentCode INT = NULL
AS
BEGIN
	DELETE g FROM wc_goal g
	JOIN wc_game a on g.game_code = a.game_code
	WHERE tournament_code = @TournamentCode

	DELETE FROM wc_game 
	WHERE tournament_code = @TournamentCode

	DELETE FROM wc_tournament_format_playoff_setup
	WHERE tournament_code = @TournamentCode

	DELETE FROM wc_group_stage
	WHERE tournament_code = @TournamentCode

	DELETE FROM wc_group_stage_third_placed_teams
	WHERE tournament_code = @TournamentCode
END
GO
