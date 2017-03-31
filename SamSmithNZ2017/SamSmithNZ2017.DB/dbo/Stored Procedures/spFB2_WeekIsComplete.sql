CREATE PROCEDURE [dbo].[spFB2_WeekIsComplete]
	@year_code smallint,
	@week_code smallint
AS

DECLARE @game_count smallint, @games_complete smallint
DECLARE @week_is_complete bit

SELECT @game_count = count(*)
FROM fbweektemplate
WHERE year_code = @year_code 
and week_code = @week_code

SELECT @games_complete = count(*)
FROM fbweektemplate
WHERE year_code = @year_code 
and week_code = @week_code
and game_time < getdate()

IF (@game_count - @games_complete = 0)
	SELECT @week_is_complete = 1
ELSE
	SELECT @week_is_complete = 0

SELECT @week_is_complete as week_is_complete