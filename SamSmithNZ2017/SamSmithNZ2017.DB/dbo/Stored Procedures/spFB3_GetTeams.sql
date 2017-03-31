CREATE PROCEDURE [dbo].[spFB3_GetTeams]
	@team_code smallint = null
AS

SELECT *
FROM FBTeam
WHERE (@team_code is null or team_code = @team_code)
ORDER BY team_name