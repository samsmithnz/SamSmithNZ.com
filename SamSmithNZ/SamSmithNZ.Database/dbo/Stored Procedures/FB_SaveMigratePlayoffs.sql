CREATE PROCEDURE [dbo].[FB_SaveMigratePlayoffsGames]
	@TournamentCode INT,
	@RoundCode VARCHAR(10),
	@GameNumber INT, 
	@Team1Prereq VARCHAR(50),
	@Team2Prereq VARCHAR(50)
AS
BEGIN
	INSERT INTO wc_tournament_format_playoff_setup
	SELECT @TournamentCode, @RoundCode, @GameNumber, @Team1Prereq, @Team2Prereq
END
GO