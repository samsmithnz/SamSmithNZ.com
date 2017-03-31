CREATE PROCEDURE [dbo].[spfb_GamesLeftCount]
	@year_code smallint,
	@week_code smallint
As
SET NOCOUNT ON

SELECT count(*) 
FROM fbweektemplate
WHERE year_code = @year_code
and week_code = @week_code
and game_time >= getdate()