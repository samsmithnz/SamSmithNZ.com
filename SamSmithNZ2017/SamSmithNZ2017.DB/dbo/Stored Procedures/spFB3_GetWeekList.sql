CREATE PROCEDURE [dbo].[spFB3_GetWeekList]
	@year_code smallint
AS

SELECT year_code, week_code, min(game_time) as starting_game_time
FROM fbweektemplate
WHERE year_code = @year_code
GROUP BY year_code, week_code