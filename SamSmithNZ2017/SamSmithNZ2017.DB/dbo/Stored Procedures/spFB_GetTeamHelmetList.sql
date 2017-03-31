CREATE PROCEDURE [dbo].[spFB_GetTeamHelmetList] 
AS
SELECT * 
FROM FBTeam
WHERE team_code <> 0
ORDER BY team_name