CREATE PROCEDURE [dbo].[spFB2_GetPlayerListForPeopleWithNoPicks]
	@year_code smallint,
	@week_code smallint
AS

SELECT DISTINCT p.player_code, player_name
FROM FBPlayer p
INNER JOIN fbweek w ON p.player_code = w.player_code and p.year_code = w.year_code
WHERE w.year_code = @year_code and w.week_code = @week_code and fav_team_picked = -1
ORDER BY player_name