CREATE PROCEDURE [dbo].[spWC_GetGroupGameList]
	@tournament_code INT,
	@round_number INT,
	@round_code VARCHAR(10)
AS
SET NOCOUNT ON

/*
SELECT CONVERT(INT,NULL) AS row_type, 
	CONVERT(datetime,NULL) AS game_time, 
	CONVERT(INT,NULL) AS game_number,
	CONVERT(VARCHAR(100),NULL) AS team_1_name, 
	CONVERT(INT,NULL) AS team_1_code, 
	CONVERT(VARCHAR(10),NULL) AS team_1_score,
	CONVERT(VARCHAR(100),NULL) AS team_2_name, 
	CONVERT(INT,NULL) AS team_2_code, 
	CONVERT(VARCHAR(10),NULL) AS team_2_score,
	CONVERT(VARCHAR(100),NULL) AS team_1_flag_name, 
	CONVERT(VARCHAR(100),NULL) AS team_2_flag_name,
	CONVERT(VARCHAR(100),NULL) AS location, 
	CONVERT(INT,NULL) AS round_number, 
	CONVERT(VARCHAR(10),NULL) AS round_code
*/

CREATE TABLE #tmp_games (row_type INT, game_time datetime, game_number INT,
	team_1_name VARCHAR(200), team_1_code INT, team_1_score VARCHAR(10),
	team_2_name VARCHAR(200), team_2_code INT, team_2_score VARCHAR(10),
	team_1_flag_name VARCHAR(100), team_2_flag_name VARCHAR(100),
	location VARCHAR(100), round_number INT, round_code VARCHAR(10), sort_order INT)

INSERT INTO #tmp_games
SELECT 1 AS row_type, g.game_time, g.game_number,
	t1.team_name AS team_1_name, t1.team_code AS team_1_code, g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,''),
	t2.team_name AS team_2_name, t2.team_code AS team_2_code, g.team_2_normal_time_score + ISNULL(g.team_1_extra_time_score,''), 
	t1.flag_name AS team_1_flag_name, 
	t2.flag_name AS team_2_flag_name,
	g.location, g.round_number, g.round_code, 1 AS sort_order
FROM wc_game g
JOIN wc_team t1 ON g.team_1_code = t1.team_code
JOIN wc_team t2 ON g.team_2_code = t2.team_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and (g.round_code = @round_code or g.round_code = @round_code + 'a')
--ORDER BY g.game_time, g.game_number--, gl.goal_time, ISNULL(go.injury_time,0)

--Insert Team 1 Scorers
INSERT INTO #tmp_games
SELECT 2 AS row_type, g.game_time, g.game_number,
	p.player_name, p.team_code, CONVERT(VARCHAR(10),gl.goal_time),
	'',0,'',
	'Soccerball_svg.png', '',
	g.location, g.round_number, g.round_code, gl.goal_time AS sort_order
FROM wc_game g
JOIN wc_goal gl ON gl.game_code = g.game_code
JOIN wc_player p ON p.player_code = gl.player_code and g.team_1_code = p.team_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and (g.round_code = @round_code or g.round_code = @round_code + 'a')

--Insert Team 2 Scorers
INSERT INTO #tmp_games
SELECT 2 AS row_type, g.game_time, g.game_number,
	'',0,'',
	p.player_name, p.team_code, CONVERT(VARCHAR(10),gl.goal_time),
	'', 'Soccerball_svg.png',
	g.location, g.round_number, g.round_code, gl.goal_time AS sort_order
FROM wc_game g
JOIN wc_goal gl ON gl.game_code = g.game_code
JOIN wc_player p ON p.player_code = gl.player_code and g.team_2_code = p.team_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and (g.round_code = @round_code or g.round_code = @round_code + 'a')

SELECT row_type, game_time, game_number,
	team_1_name, team_1_code, team_1_score,
	team_2_name, team_2_code, team_2_score,
	team_1_flag_name, team_2_flag_name,
	location, round_number, round_code
FROM #tmp_games
ORDER BY game_time, game_number, row_type, sort_order

DROP TABLE #tmp_games