CREATE PROCEDURE [dbo].[FB_GetTeams]
	@TeamCode INT = NULL
AS
BEGIN
	SELECT team_code AS TeamCode, 
		team_name AS TeamName, 
		flag_name AS FlagName
	FROM wc_team t
	WHERE (@TeamCode is NULL or team_code = @TeamCode)
	ORDER by team_name
END