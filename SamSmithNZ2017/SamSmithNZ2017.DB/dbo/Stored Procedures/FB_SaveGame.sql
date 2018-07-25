﻿CREATE PROCEDURE [dbo].[FB_SaveGame] 
	@GameCode INT,
	@Team1NormalTimeScore INT, 
	@Team1ExtraTimeScore INT, 
	@Team1PenaltiesScore INT,
	@Team2NormalTimeScore INT, 
	@Team2ExtraTimeScore INT, 
	@Team2PenaltiesScore INT,
	@Team1StartingELORating INT = NULL,
	@Team2StartingELORating INT = NULL,
	@Team1EndingELORating INT = NULL,
	@Team2EndingELORating INT = NULL
AS
BEGIN
	SET NOCOUNT ON

	UPDATE g
	SET g.team_1_normal_time_score = @Team1NormalTimeScore,
		g.team_1_extra_time_score = @Team1ExtraTimeScore,
		g.team_1_penalties_score = @Team1PenaltiesScore,
		g.team_2_normal_time_score = @Team2NormalTimeScore,
		g.team_2_extra_time_score = @Team2ExtraTimeScore,
		g.team_2_penalties_score = @Team2PenaltiesScore,
		g.team_1_pregame_elo_rating = @Team1StartingELORating,
		g.team_2_pregame_elo_rating = @Team2StartingELORating,
		g.team_1_postgame_elo_rating = @Team1EndingELORating, 
		g.team_2_postgame_elo_rating = @Team2EndingELORating
	FROM wc_game g
	WHERE g.game_code = @GameCode

	DECLARE @TournamentCode INT
	DECLARE @RoundNumber INT
	DECLARE @RoundCode VARCHAR(10)
	SELECT @TournamentCode = g.tournament_code,
		@RoundNumber = g.round_number,
		@RoundCode = g.round_code
	FROM wc_game g
	WHERE g.game_code = @GameCode

	IF (@RoundNumber = 1)
	BEGIN
		EXEC FB_SaveGroupDetails @TournamentCode = @TournamentCode, @RoundNumber = @RoundNumber, @RoundCode = @RoundCode
	END
	ELSE
	BEGIN
		EXEC FB_SavePlayoffDetails @TournamentCode = @TournamentCode, @RoundNumber = @RoundNumber, @RoundCode = @RoundCode
	END

END