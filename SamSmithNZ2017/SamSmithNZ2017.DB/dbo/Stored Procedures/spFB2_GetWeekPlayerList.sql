CREATE PROCEDURE [dbo].[spFB2_GetWeekPlayerList]
	@year_code smallint,
	@week_code smallint
AS

SELECT DISTINCT p.player_name 
FROM fbweek fb
INNER JOIN fbplayer p ON fb.player_code = p.player_code and fb.year_code = p.year_code
WHERE fb.year_code = @year_code 
and fb.week_code = @week_code
and fav_team_picked >= 0
and is_celebrity = 0