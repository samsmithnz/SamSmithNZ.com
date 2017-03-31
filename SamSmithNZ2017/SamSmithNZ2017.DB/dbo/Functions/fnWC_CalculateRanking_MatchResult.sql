CREATE FUNCTION dbo.fnWC_CalculateRanking_MatchResult(
	@game_code smallint, 
	@team_code smallint
)
RETURNS decimal(10,2)
AS
BEGIN
	 
	--Match result
	/*
	Result						Points
	Win (no penalty shootout)	+3
	Win (penalty shootout)		+1
	Draw						+1
	Loss (penalty shootout)		-1
	Loss (no penalty shootout)	-3
	*/ 
	 
	DECLARE @is_team_1 bit
	IF (exists (SELECT 1 FROM wc_game WHERE game_code = @game_code and team_1_code = @team_code))
	BEGIN
		SELECT @is_team_1 = 1
	END
	ELSE
	BEGIN
		SELECT @is_team_1 = 0
	END
	
	DECLARE @points smallint
	SELECT @points = 0
	IF (@is_team_1 = 1)
	BEGIN
		--Team 1 Win
		SELECT @points = 3
		FROM wc_game 
		WHERE game_code = @game_code
		and team_1_code = @team_code
		and (team_1_normal_time_score > team_2_normal_time_score	
			or team_1_extra_time_score > team_2_extra_time_score)
			
		--Team 1 PK Win
		SELECT @points = 1
		FROM wc_game 
		WHERE game_code = @game_code
		and team_1_code = @team_code
		and (team_1_penalties_score > team_2_penalties_score)
			
		--Team 1 Draw 
		SELECT @points = 1
		FROM wc_game 
		WHERE game_code = @game_code
		and team_1_code = @team_code
		and (team_1_normal_time_score = team_2_normal_time_score	
			or team_1_extra_time_score = team_2_extra_time_score)
			
		--Team 1 Loss
		SELECT @points = -3
		FROM wc_game 
		WHERE game_code = @game_code
		and team_1_code = @team_code
		and (team_1_normal_time_score < team_2_normal_time_score	
			or team_1_extra_time_score < team_2_extra_time_score)
			
		--Team 1 PK Loss
		SELECT @points = -1
		FROM wc_game 
		WHERE game_code = @game_code
		and team_1_code = @team_code
		and (team_1_penalties_score < team_2_penalties_score)
	END
	ELSE
	BEGIN
		--Team 2 Win
		SELECT @points = 3
		FROM wc_game 
		WHERE game_code = @game_code
		and team_2_code = @team_code
		and (team_1_normal_time_score < team_2_normal_time_score	
			or team_1_extra_time_score < team_2_extra_time_score)
			
		--Team 2 PK Win
		SELECT @points = 1
		FROM wc_game 
		WHERE game_code = @game_code
		and team_2_code = @team_code
		and (team_1_penalties_score < team_2_penalties_score)
			
		--Team 2 Draw 
		SELECT @points = 1
		FROM wc_game 
		WHERE game_code = @game_code
		and team_2_code = @team_code
		and (team_1_normal_time_score = team_2_normal_time_score	
			or team_1_extra_time_score = team_2_extra_time_score)
			
		--Team 2 Loss
		SELECT @points = -3
		FROM wc_game 
		WHERE game_code = @game_code
		and team_2_code = @team_code
		and (team_1_normal_time_score > team_2_normal_time_score	
			or team_1_extra_time_score > team_2_extra_time_score)
			
		--Team 2 PK Loss
		SELECT @points = -1
		FROM wc_game 
		WHERE game_code = @game_code
		and team_2_code = @team_code
		and (team_1_penalties_score > team_2_penalties_score)
	END

	RETURN @points
END
--SELECT dbo.fnWC_CalculateGameRanking(1189, 11) -- +3
--SELECT dbo.fnWC_CalculateGameRanking(1189, 18) -- -3