CREATE PROCEDURE [dbo].[FB_GetPlayersByTournament]
	@TournamentCode INT = NULL
	--@PlayerName NVARCHAR(200) = NULL
AS
BEGIN
	SELECT p.player_code AS PlayerCode, 
		SUBSTRING(p.player_name, CHARINDEX(' ', p.player_name) + 1, LEN(p.player_name)) + ', ' + SUBSTRING(p.player_name, 1, CASE WHEN (CHARINDEX(' ', p.player_name) - 1) < 0 THEN 0 ELSE CHARINDEX(' ', p.player_name) -1 END) + ' (' + t.team_name + ')' AS PlayerName,       
		p.number AS Number, 
		p.position AS Position, 
		t.team_name AS TeamName
	FROM wc_player p 
	JOIN wc_team t ON p.team_code = t.team_code
	WHERE (p.tournament_code = @TournamentCode OR @TournamentCode IS NULL)
	--AND (p.player_name like '%' + @PlayerName + '%' OR @PlayerName IS NULL)
	ORDER BY team_name, SUBSTRING(p.player_name, CHARINDEX(' ', p.player_name) + 1, LEN(p.player_name)) + ', ' + SUBSTRING(p.player_name, 1, CASE WHEN (CHARINDEX(' ', p.player_name) - 1) < 0 THEN 0 ELSE CHARINDEX(' ', p.player_name) -1 END) + ' (' + t.team_name + ')'
END
GO