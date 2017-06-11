CREATE FUNCTION [dbo].[fnWC_GetTeamDraws](
	@tournament_code INT,
	@round_number INT,
	@round_code VARCHAR(10),
	@team_code INT
)
RETURNS INT
AS
BEGIN
	DECLARE @total INT

	SELECT @total = 0

	SELECT @total = @total + ISNULL(COUNT(*),0)
	FROM wc_game 
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_1_code = @team_code
	and team_1_normal_time_score = team_2_normal_time_score

	SELECT @total = @total + ISNULL(COUNT(*),0)
	FROM wc_game 
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_2_code = @team_code
	and team_2_normal_time_score = team_1_normal_time_score

	RETURN @total
END