CREATE PROCEDURE [dbo].[FB_GetPlayers]
	@GameCode INT = NULL,
	@TournamentCode INT = NULL,
	@PlayerName NVARCHAR(200) = NULL
AS
BEGIN
	SELECT p.player_code AS PlayerCode, 
		SUBSTRING(p.player_name, CHARINDEX(' ', p.player_name) + 1, LEN(p.player_name)) + ', ' + SUBSTRING(p.player_name, 1, CASE WHEN (CHARINDEX(' ', p.player_name) - 1) < 0 THEN 0 ELSE CHARINDEX(' ', p.player_name) -1 END) + ' (' + t.team_name + ')' AS PlayerName,       
		p.number AS Number, 
		p.position AS Position, 
		t.team_name AS TeamName
	FROM wc_player p 
	JOIN wc_team t ON p.team_code = t.team_code
	JOIN wc_game g ON p.tournament_code = g.tournament_code and (p.team_code = g.team_1_code or p.team_code = g.team_2_code)
	WHERE (g.game_code = @GameCode OR @GameCode IS NULL)
	AND (g.tournament_code = @TournamentCode OR @TournamentCode IS NULL)
	AND (p.player_name = @PlayerName OR @PlayerName IS NULL)
	ORDER BY team_name, SUBSTRING(p.player_name, CHARINDEX(' ', p.player_name) + 1, LEN(p.player_name)) + ', ' + SUBSTRING(p.player_name, 1, CASE WHEN (CHARINDEX(' ', p.player_name) - 1) < 0 THEN 0 ELSE CHARINDEX(' ', p.player_name) -1 END) + ' (' + t.team_name + ')'
END
