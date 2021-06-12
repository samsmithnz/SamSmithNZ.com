CREATE PROCEDURE [dbo].[spWC_GetPlayerGames]
	@player_code INT
AS
SELECT p.player_code, p.player_name, t.team_code, t.team_name, t.flag_name,
	g.game_code, g.game_time, g.round_number, r.round_name, g.location, 
	gl.goal_time, gl.injury_time, gl.is_penalty, gl.is_own_goal,
	tt.name AS tournament_name
FROM wc_game g 
JOIN wc_team t ON g.team_2_code = t.team_code
JOIN wc_goal gl ON gl.game_code = g.game_code
JOIN wc_round r ON g.round_code = r.round_code 
JOIN wc_player p ON p.player_code = gl.player_code and g.team_1_code = p.team_code
JOIN wc_tournament tt ON g.tournament_code = tt.tournament_code
WHERE p.player_code = @player_code

UNION
SELECT p.player_code, p.player_name, t.team_code, t.team_name, t.flag_name,
	g.game_code, g.game_time, g.round_number, r.round_name, g.location, 
	gl.goal_time, gl.injury_time, gl.is_penalty, gl.is_own_goal,
	tt.name AS tournament_name
FROM wc_game g 
JOIN wc_team t ON g.team_1_code = t.team_code
JOIN wc_goal gl ON gl.game_code = g.game_code
JOIN wc_round r ON g.round_code = r.round_code 
JOIN wc_player p ON p.player_code = gl.player_code and g.team_2_code = p.team_code
JOIN wc_tournament tt ON g.tournament_code = tt.tournament_code
WHERE p.player_code = @player_code

ORDER BY game_time DESC, goal_time DESC, injury_time DESC