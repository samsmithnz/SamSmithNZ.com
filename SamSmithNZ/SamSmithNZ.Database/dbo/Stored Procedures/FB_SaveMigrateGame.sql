CREATE PROCEDURE [dbo].[FB_SaveMigrateGame] 
	@TournamentCode INT,
	@GameNumber INT,
	@GameTime DATETIME,
	@RoundNumber INT,
	@RoundCode VARCHAR(10),
	@Location VARCHAR(100),
	@Team1Code INT,
	@Team2Code INT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO wc_game
	SELECT (SELECT MAX(game_code) + 1 FROM wc_game), @TournamentCode, @GameNumber, @GameTime, @RoundNumber, @RoundCode,
		@Location, @Team1Code, @Team2Code, 0,0,0,0,0,0,0,0,0,0,0,0
	
	IF (@RoundNumber = 1)
	BEGIN
		EXEC FB_SaveGroupDetails @TournamentCode = @TournamentCode, @RoundNumber = @RoundNumber, @RoundCode = @RoundCode
	END
	ELSE
	BEGIN
		EXEC FB_SavePlayoffDetails @TournamentCode = @TournamentCode, @RoundNumber = @RoundNumber, @RoundCode = @RoundCode, @GameNumber = @GameNumber
	END
END
GO

