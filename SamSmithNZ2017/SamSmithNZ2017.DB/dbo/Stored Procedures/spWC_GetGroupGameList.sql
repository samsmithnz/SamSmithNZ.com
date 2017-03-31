CREATE PROCEDURE [dbo].[spWC_GetGroupGameList]
	@tournament_code smallint,
	@round_number smallint,
	@round_code varchar(10)
AS
SET NOCOUNT ON

/*
SELECT CONVERT(smallint,null) as row_type, 
	CONVERT(datetime,null) as game_time, 
	CONVERT(smallint,null) as game_number,
	CONVERT(varchar(100),null) as team_1_name, 
	CONVERT(smallint,null) as team_1_code, 
	CONVERT(varchar(10),null) as team_1_score,
	CONVERT(varchar(100),null) as team_2_name, 
	CONVERT(smallint,null) as team_2_code, 
	CONVERT(varchar(10),null) as team_2_score,
	CONVERT(varchar(100),null) as team_1_flag_name, 
	CONVERT(varchar(100),null) as team_2_flag_name,
	CONVERT(varchar(100),null) as location, 
	CONVERT(smallint,null) as round_number, 
	CONVERT(varchar(10),null) as round_code
*/

CREATE TABLE #tmp_games (row_type smallint, game_time datetime, game_number smallint,
	team_1_name varchar(100), team_1_code smallint, team_1_score varchar(10),
	team_2_name varchar(100), team_2_code smallint, team_2_score varchar(10),
	team_1_flag_name varchar(100), team_2_flag_name varchar(100),
	location varchar(100), round_number smallint, round_code varchar(10), sort_order smallint)

INSERT INTO #tmp_games
SELECT 1 as row_type, g.game_time, g.game_number,
	t1.team_name as team_1_name, t1.team_code as team_1_code, g.team_1_normal_time_score + isnull(g.team_1_extra_time_score,''),
	t2.team_name as team_2_name, t2.team_code as team_2_code, g.team_2_normal_time_score + isnull(g.team_1_extra_time_score,''), 
	t1.flag_name as team_1_flag_name, 
	t2.flag_name as team_2_flag_name,
	g.location, g.round_number, g.round_code, 1 as sort_order
FROM wc_game g
INNER JOIN wc_team t1 ON g.team_1_code = t1.team_code
INNER JOIN wc_team t2 ON g.team_2_code = t2.team_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and (g.round_code = @round_code or g.round_code = @round_code + 'a')
--ORDER BY g.game_time, g.game_number--, go.goal_time, isnull(go.injury_time,0)

--Insert Team 1 Scorers
INSERT INTO #tmp_games
SELECT 2 as row_type, g.game_time, g.game_number,
	p.player_name, p.team_code, go.goal_time,
	'','','',
	'Soccerball_svg.png', '',
	g.location, g.round_number, g.round_code, go.goal_time as sort_order
FROM wc_game g
INNER JOIN wc_goal go ON go.game_code = g.game_code
INNER JOIN wc_player p ON p.player_code = go.player_code and g.team_1_code = p.team_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and (g.round_code = @round_code or g.round_code = @round_code + 'a')

--Insert Team 2 Scorers
INSERT INTO #tmp_games
SELECT 2 as row_type, g.game_time, g.game_number,
	'','','',
	p.player_name, p.team_code, go.goal_time,
	'', 'Soccerball_svg.png',
	g.location, g.round_number, g.round_code, go.goal_time as sort_order
FROM wc_game g
INNER JOIN wc_goal go ON go.game_code = g.game_code
INNER JOIN wc_player p ON p.player_code = go.player_code and g.team_2_code = p.team_code
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