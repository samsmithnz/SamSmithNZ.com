CREATE FUNCTION [dbo].[fnWC_GetTeamGamesPlayed](
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

	SELECT @total = @total + isnull(count(*) ,0)
	FROM wc_game 
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_1_code = @team_code
	and not team_1_normal_time_score is null

	SELECT @total = @total + isnull(count(*),0)
	FROM wc_game 
	WHERE tournament_code = @tournament_code
	and round_number = @round_number
	and round_code = @round_code
	and team_2_code = @team_code
	and not team_2_normal_time_score is null

	RETURN @total
END