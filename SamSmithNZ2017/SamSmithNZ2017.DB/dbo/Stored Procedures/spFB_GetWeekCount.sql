
CREATE PROCEDURE [dbo].[spFB_GetWeekCount]
	@year_code smallint,
	@player_code smallint,
	@week_code smallint
AS

SELECT * 
FROM FBWeek
WHERE player_code = @player_code and year_code = @year_code and week_code = @week_code