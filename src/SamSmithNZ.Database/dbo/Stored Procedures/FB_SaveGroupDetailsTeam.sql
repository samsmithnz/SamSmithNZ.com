CREATE PROCEDURE [dbo].[FB_SaveGroupDetailsTeam]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10),
	@TeamCode INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @TeamsInGroup INT
	SELECT @TeamsInGroup = ISNULL((SELECT COUNT(*) FROM wc_group_stage g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_number = @RoundNumber
		AND g.round_code = @RoundCode
		AND g.team_code = @TeamCode),0)

	DECLARE @ExistingTeamsInGroup INT
	SELECT @ExistingTeamsInGroup = ISNULL((SELECT COUNT(*) FROM wc_group_stage g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_number = @RoundNumber
		AND g.round_code = @RoundCode),0)

	IF (@TeamsInGroup = 0)
	BEGIN
		INSERT INTO wc_group_stage
		SELECT @RoundNumber, @RoundCode, @TournamentCode, @TeamCode, 0, 0, 0, 0, 0, 0, 0, 0, 0, @ExistingTeamsInGroup + 1
	END
END
GO
