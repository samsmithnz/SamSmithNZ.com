
CREATE PROCEDURE [dbo].[spFB2_GetTeams]
AS

SELECT *
FROM FBTeam
ORDER BY team_name