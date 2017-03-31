CREATE FUNCTION dbo.fnWC_CalculateRanking_GameAge(
	@game_code smallint,
	@calculation_date datetime
)
RETURNS decimal(10,2)
AS
BEGIN
	 
	--Game Age
	/*
	Date of match				Multiplier
	Within the last 12 months	× 1.0
	Within 12 - 24 months ago	× 0.8
	Within 24 - 36 months ago	× 0.6
	Within 36 - 48 months ago	× 0.4
	Within 48 - 60 months ago	× 0.2
	*/
	 
	
	DECLARE @points decimal(10,2)
	--SELECT @points = 0

	DECLARE @game_time datetime
	SELECT @game_time = game_time
	FROM wc_game 
	WHERE game_code = @game_code
		
	DECLARE @year_difference smallint
	SELECT @year_difference = year(@calculation_date) - year(@game_time)

	SELECT @points = CASE WHEN @year_difference <= 0 THEN 1
							WHEN @year_difference <= 1 THEN CONVERT(decimal(10,2),0.8)
							WHEN @year_difference <= 2 THEN CONVERT(decimal(10,2),0.6)
							WHEN @year_difference <= 3 THEN CONVERT(decimal(10,2),0.4)
							WHEN @year_difference <= 4 THEN CONVERT(decimal(10,2),0.2)
						  	ELSE 0 END

	RETURN @points
END