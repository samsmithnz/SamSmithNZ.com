CREATE PROCEDURE [dbo].[spWC_GetTeamList]
	@team_code INT = NULL
AS
SELECT team_code, team_name, flag_name 
FROM wc_team
WHERE (@team_code is NULL or team_code = @team_code)
ORDER by team_name