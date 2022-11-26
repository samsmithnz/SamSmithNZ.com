CREATE PROCEDURE [dbo].[FB_SaveTournamentTeamELORating] 
	@TournamentCode INT,
	@TeamCode INT, 
	@ELORating INT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE te
	SET te.current_elo_rating = @ELORating
	FROM wc_tournament_team_entry te
	WHERE te.tournament_code = @TournamentCode
	AND te.team_code = @TeamCode

	UPDATE g
	SET g.team_1_elo_rating = @ELORating
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND g.team_1_code = @TeamCode

	UPDATE g
	SET g.team_2_elo_rating = @ELORating
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND g.team_2_code = @TeamCode

END