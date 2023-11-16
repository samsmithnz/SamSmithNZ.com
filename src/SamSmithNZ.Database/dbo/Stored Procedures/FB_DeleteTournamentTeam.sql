CREATE PROCEDURE [dbo].[FB_DeleteTournamentTeam]
	@TournamentCode INT,
	@TeamCode INT
AS
BEGIN
	DELETE te
	FROM wc_tournament_team_entry te
	WHERE te.tournament_code = @TournamentCode
	AND te.team_code = @TeamCode
END