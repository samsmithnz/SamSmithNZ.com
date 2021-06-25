CREATE PROCEDURE [dbo].[FB_SaveMigratePlayoffsGames]
	@TournamentCode INT,
	@RoundNumber INT,
	@GameNumber INT, 
	@Team1Prereq VARCHAR(50),
	@Team2Prereq VARCHAR(50)
AS
BEGIN
	INSERT INTO wc_tournament_format_playoff_setup
	SELECT @TournamentCode, @RoundNumber, @GameNumber, @Team1Prereq, @Team2Prereq
END
GO