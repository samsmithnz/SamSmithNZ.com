CREATE PROCEDURE [dbo].[spFB2_GamesLeft]
	@year_code smallint,
	@week_code smallint
AS
SET NOCOUNT ON

SELECT count(*) as games_left
FROM fbweektemplate
WHERE year_code = @year_code
and week_code = @week_code
and game_time >= getdate()