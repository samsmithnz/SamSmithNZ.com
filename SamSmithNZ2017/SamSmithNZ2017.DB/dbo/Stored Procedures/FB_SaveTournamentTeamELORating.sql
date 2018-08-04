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

	--UPDATE g
	--SET g.team_1_elo_rating = @ELORating
	--FROM wc_game g
	--WHERE g.tournament_code = @TournamentCode
	--AND g.team_1_code = @TeamCode

	--UPDATE g
	--SET g.team_2_elo_rating = @ELORating
	--FROM wc_game g
	--WHERE g.tournament_code = @TournamentCode
	--AND g.team_2_code = @TeamCode

END