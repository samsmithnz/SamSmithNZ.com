CREATE PROCEDURE [dbo].[spWC_GetTeamName]
	@team_code INT
AS
SELECT team_name
FROM wc_team
WHERE team_code = @team_code