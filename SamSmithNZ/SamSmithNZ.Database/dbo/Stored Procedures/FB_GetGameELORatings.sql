CREATE PROCEDURE [dbo].[FB_GetGamePreELORatings] 
	@TournamentCode INT,	
	@GameCode INT 
AS
BEGIN
	--DECLARE @TournamentCode INT
	--DECLARE @GameCode INT 
	--SELECT @TournamentCode = 22, @GameCode = 7852

	DECLARE @Team1Code INT
	DECLARE @Team2Code INT
	SELECT @Team1Code = team_1_code, @Team2Code = team_2_code
	FROM wc_game 
	WHERE game_code = @GameCode

	DECLARE @Team1PreELORating INT
	DECLARE @Team2PreELORating INT

	-- Get the last game ELO Rating
	SELECT TOP 1 @Team1PreELORating = CASE WHEN g.team_1_code = @Team1Code THEN g.team_1_postgame_elo_rating ELSE g.team_2_postgame_elo_rating END
		--CASE WHEN g.team_1_code = @Team1Code THEN g.team_1_code ELSE g.team_2_code END, game_number
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND g.game_time < (SELECT game_time FROM wc_game g1 WHERE g1.game_code = @GameCode)
	AND (g.team_1_code = @Team1Code OR g.team_2_code = @Team1Code)
	ORDER BY g.game_time DESC

	SELECT TOP 1 @Team2PreELORating = CASE WHEN g.team_1_code = @Team2Code THEN g.team_1_postgame_elo_rating ELSE g.team_2_postgame_elo_rating END 
		--CASE WHEN g.team_1_code = @Team2Code THEN g.team_1_code ELSE g.team_2_code END, game_number
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND g.game_time < (SELECT game_time FROM wc_game g1 WHERE g1.game_code = @GameCode)
	AND (g.team_1_code = @Team2Code OR g.team_2_code = @Team2Code)
	ORDER BY g.game_time DESC

	IF (@Team1PreELORating IS NULL)
	BEGIN
		SELECT @Team1PreELORating = te.starting_elo_rating
		FROM [dbo].[wc_tournament_team_entry] te
		WHERE te.tournament_code = @TournamentCode
		AND te.team_code = @Team1Code
	END
	IF (@Team2PreELORating IS NULL)
	BEGIN
		SELECT @Team2PreELORating = te.starting_elo_rating
		FROM [dbo].[wc_tournament_team_entry] te
		WHERE te.tournament_code = @TournamentCode
		AND te.team_code = @Team2Code
	END

	SELECT @Team1Code AS Team1Code, @Team1PreELORating AS Team1PreELORating, 
		@Team2Code AS Team2Code, @Team2PreELORating AS Team2PreELORating
	
END
GO