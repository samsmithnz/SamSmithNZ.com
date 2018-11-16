CREATE PROCEDURE [dbo].[FB_SaveTournamentTeam]
	@TournamentCode INT,
	@TeamCode INT
AS
BEGIN
	INSERT INTO wc_tournament_team_entry
	SELECT @TournamentCode, @TeamCode, '1', NULL, NULL, NULL, 1, NULL
END