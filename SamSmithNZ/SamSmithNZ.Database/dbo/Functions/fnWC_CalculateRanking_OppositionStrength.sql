CREATE FUNCTION dbo.fnWC_CalculateRanking_OppositionStrength(
	@game_code INT, 
	@team_code INT
)
RETURNS decimal(10,2)
AS
BEGIN
	 
	--Opponent Strength
	/*

	opposition strength multiplier = (200 - ranking position) / 100
	
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
	
	DECLARE @team_rank INT
	DECLARE @opposition_rank INT
	DECLARE @points decimal(10,2)
	SELECT @points = 0
	
	IF (@is_team_1 = 1)
	BEGIN
		--Find Team rank
		SELECT @team_rank = r.ranking
		FROM wc_game g
		JOIN wc_ranking r ON r.team_code = g.team_1_code and r.ranking_date > g.game_time  
		WHERE g.game_code = @game_code
		and g.team_1_code = @team_code
	
		--Find Opposition rank
		SELECT @opposition_rank = r.ranking
		FROM wc_game g
		JOIN wc_ranking r ON r.team_code = g.team_2_code and r.ranking_date > g.game_time  
		WHERE g.game_code = @game_code
		and g.team_1_code = @team_code

	END
	ELSE
	BEGIN
		--Find Team rank
		SELECT @team_rank = r.ranking
		FROM wc_game g
		JOIN wc_ranking r ON r.team_code = g.team_2_code and r.ranking_date > g.game_time  
		WHERE g.game_code = @game_code
		and g.team_2_code = @team_code

		--Find Opposition strength
		SELECT @opposition_rank = r.ranking
		FROM wc_game g
		JOIN wc_ranking r ON r.team_code = g.team_1_code and r.ranking_date > g.game_time  
		WHERE g.game_code = @game_code
		and g.team_2_code = @team_code	
	END

	SELECT @points = ISNULL(@team_rank,CONVERT(decimal(10,2),199)) - ISNULL(@opposition_rank,CONVERT(decimal(10,2),199))

	--SELECT @points = (CONVERT(decimal(10,2),200) - ISNULL(@opposition_rank,CONVERT(decimal(10,2),199))) / CONVERT(decimal(10,2),100)
	IF (@points = 0)
	BEGIN
		SELECT @points = 1
	END
	
	RETURN @points
END