

CREATE PROCEDURE [dbo].[spFB_GetWeekList]
	@year_code smallint,
	@employee_code char(5)
AS

SELECT year_code, week_code, min(game_time) as starting_game_time
FROM fbweektemplate
WHERE year_code = @year_code
GROUP BY year_code, week_code