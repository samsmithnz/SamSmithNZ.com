CREATE PROCEDURE [dbo].[FB_SavePlayer]
	@TeamCode INT,
	@TournamentCode INT,
	@Number INT,
	@Position VARCHAR(50),
	@IsCaptain BIT,
	@PlayerName NVARCHAR(200),
	@DateOfBirth DATETIME,
	@ClubName NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO wc_player
	SELECT (SELECT MAX(player_code) FROM wc_player) + 1,
		@TeamCode,
		@TournamentCode,
		@PlayerName,
		@Number,
		@Position,
		@DateOfBirth,
		@IsCaptain,
		@ClubName,
		NULL
END

GO