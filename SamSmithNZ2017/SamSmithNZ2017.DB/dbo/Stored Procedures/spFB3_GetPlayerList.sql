CREATE PROCEDURE [dbo].[spFB3_GetPlayerList]
	@year_code smallint
AS

SELECT player_code, player_name
FROM FBPlayer 
WHERE year_code = @year_code 
ORDER BY player_name