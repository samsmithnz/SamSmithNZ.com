

CREATE PROCEDURE [dbo].[spFB_IsWeekComplete]
	@year_code smallint,
	@week_code smallint
AS

DECLARE @game_count smallint, @games_complete smallint

SELECT @game_count = count(*)
FROM fbweektemplate
WHERE year_code = @year_code and week_code = @week_code

SELECT @games_complete = count(*)
FROM fbweektemplate
WHERE year_code = @year_code and week_code = @week_code
and game_time < getdate()

IF (@game_count - @games_complete = 0)
BEGIN
	SELECT 1
END
ELSE
BEGIN
	SELECT 0
END