

CREATE PROCEDURE [dbo].[spFB_GetTeams]
	@team_name varchar(50)
AS

SELECT team_code, team_name FROM FBTeam
WHERE team_name like @team_name + '%'
ORDER BY team_name