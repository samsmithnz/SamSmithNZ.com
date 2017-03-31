CREATE FUNCTION [dbo].[fnWC_GetTeamDraws](
	@tournament_code smallint,
	@round_number smallint,
	@round_code varchar(10),
	@team_code smallint
)
RETURNS smallint
AS
BEGIN
	DECLARE @total smallint

	SELECT @total = 0

	SELECT @total = @total + isnull(count(*),0)
	FROM wc_game 
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_1_code = @team_code
	and team_1_normal_time_score = team_2_normal_time_score

	SELECT @total = @total + isnull(count(*),0)
	FROM wc_game 
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_2_code = @team_code
	and team_2_normal_time_score = team_1_normal_time_score

	RETURN @total
END