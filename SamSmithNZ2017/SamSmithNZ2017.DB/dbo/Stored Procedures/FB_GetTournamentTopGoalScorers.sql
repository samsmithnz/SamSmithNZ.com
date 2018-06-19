CREATE PROCEDURE [dbo].[FB_GetTournamentTopGoalScorers]
	@TournamentCode INT
AS
BEGIN
	SELECT p.player_name AS PlayerName, 
		t.team_name AS TeamName, 
		t.flag_name AS FlagName,
		COUNT(g.goal_time) AS GoalsScored
	FROM wc_goal g
	JOIN wc_game ga ON g.game_code = ga.game_code
	JOIN wc_player p ON g.player_code = p.player_code
	JOIN wc_team t ON p.team_code = t.team_code
	WHERE ga.tournament_code = @TournamentCode
	GROUP BY p.player_name, 
		t.team_name,
		t.flag_name
	ORDER BY GoalsScored DESC
END
GO