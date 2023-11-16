CREATE PROCEDURE [dbo].[FB_GetNextGame]	
	@TournamentCode INT,
	@GameCode INT,
	@TeamCode INT
AS
BEGIN	
	DECLARE @NextGameCode INT

	-- Get the next game code
	SELECT TOP 1 @NextGameCode = g.game_code
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND g.game_time > (SELECT game_time FROM wc_game g1 WHERE g1.game_code = @GameCode)
	AND (g.team_1_code = @TeamCode OR g.team_2_code = @TeamCode)
	ORDER BY g.game_time ASC

	IF (@NextGameCode IS NOT NULL)
	BEGIN
		--Return the new game
		EXEC FB_GetGames @GameCode = @NextGameCode 
	END
END
GO
