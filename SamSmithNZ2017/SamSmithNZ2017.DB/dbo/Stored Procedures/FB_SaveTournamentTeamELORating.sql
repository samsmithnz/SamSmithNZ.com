CREATE PROCEDURE [dbo].[FB_SaveTournamentTeamELORating] 
	@TournamentCode INT,
	@TeamCode INT, 
	@ELORating INT
AS
BEGIN
	SET NOCOUNT ON

	DELETE 
	FROM wc_tournament_team_elo_rating 
	WHERE tournament_code = @TournamentCode 
	AND team_code = @TeamCode

	INSERT INTO wc_tournament_team_elo_rating 
	SELECT @TournamentCode, @TeamCode, @ELORating 

END