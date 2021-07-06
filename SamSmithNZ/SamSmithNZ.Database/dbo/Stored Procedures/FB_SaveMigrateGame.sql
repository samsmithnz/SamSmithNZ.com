CREATE PROCEDURE [dbo].[FB_SaveMigrateGame] 
	@TournamentCode INT,
	@GameNumber INT,
	@GameTime DATETIME,
	@RoundNumber INT,
	@RoundCode VARCHAR(10),
	@Location VARCHAR(100),
	@Team1Code INT,
	@Team2Code INT,
	@Team1NormalTimeScore INT,
	@Team2NormalTimeScore INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @NewGameCode INT
	SELECT @NewGameCode = MAX(game_code) + 1 FROM wc_game

	INSERT INTO wc_game
	SELECT @NewGameCode, @TournamentCode, @GameNumber, @GameTime, @RoundNumber, @RoundCode,
		@Location, @Team1Code, @Team2Code, 
		@Team1NormalTimeScore,null,null,
		@Team2NormalTimeScore,null,null,
		0,0,0,0,0,0
	
	IF (@RoundNumber = 1)
	BEGIN
		EXEC FB_SaveGroupDetails @TournamentCode = @TournamentCode, @RoundNumber = @RoundNumber, @RoundCode = @RoundCode
	END
	ELSE
	BEGIN
		EXEC FB_SavePlayoffDetails @TournamentCode = @TournamentCode, @RoundNumber = @RoundNumber, @RoundCode = @RoundCode, @GameNumber = @GameNumber
	END

	SELECT @NewGameCode AS GameCode
END
GO

