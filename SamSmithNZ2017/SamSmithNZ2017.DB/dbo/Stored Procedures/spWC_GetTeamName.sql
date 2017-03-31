CREATE PROCEDURE [dbo].[spWC_GetTeamName]
	@team_code smallint
AS
SELECT team_name
FROM wc_team
WHERE team_code = @team_code