CREATE PROCEDURE [dbo].[spWC_GetGameList2] 
	@tournament_code smallint,
	@round_number smallint,
	@round_code varchar(10),
	@show_goals bit,
	@team_code smallint
AS
SET NOCOUNT ON

/*
SELECT CONVERT(smallint,null) as row_type,  
	CONVERT(smallint,null) as round_number, 
	CONVERT(varchar(10),null) as round_code,
	CONVERT(varchar(50),null) as round_name,
	CONVERT(smallint,null) as game_code,
	CONVERT(smallint,null) as game_number,
	CONVERT(datetime,null) as game_time, 
	CONVERT(smallint,null) as team_1_code, 
	CONVERT(varchar(100),null) as team_1_name, 
	CONVERT(smallint,null) as team_1_normal_time_score, 
	CONVERT(smallint,null) as team_1_extra_time_score, 
	CONVERT(smallint,null) as team_1_penalties_score,
	CONVERT(smallint,null) as team_2_code, 
	CONVERT(varchar(100),null) as team_2_name, 
	CONVERT(smallint,null) as team_2_normal_time_score, 
	CONVERT(smallint,null) as team_2_extra_time_score, 
	CONVERT(smallint,null) as team_2_penalties_score,
	CONVERT(varchar(100),null) as team_1_flag_name, 
	CONVERT(varchar(100),null) as team_2_flag_name,
	CONVERT(varchar(100),null) as location,
	CONVERT(bit,null) as team_1_withdrew,
	CONVERT(bit,null) as team_2_withdrew, 
	CONVERT(smallint,null) as tournament_code,
	CONVERT(varchar(50),null) as tournament_name, 
	CONVERT(varchar(50),null) as coach_name, 
	CONVERT(varchar(100),null) as coach_flag,
	CONVERT(smallint,null) as fifa_ranking,
	CONVERT(bit,null) as is_penalty,
	CONVERT(bit,null) as is_own_goal
*/

CREATE TABLE #tmp_games (row_type smallint, round_number smallint, 
	round_code varchar(10), round_name varchar(50), 
	game_code smallint, game_number smallint, game_time datetime,
	team_1_code smallint, team_1_name varchar(200), 
	team_1_normal_time_score smallint, team_1_extra_time_score smallint, team_1_penalties_score smallint,
	team_2_code smallint, team_2_name varchar(200),
	team_2_normal_time_score smallint, team_2_extra_time_score smallint, team_2_penalties_score smallint,
	team_1_flag_name varchar(100), team_2_flag_name varchar(100),
	location varchar(100), team_1_withdrew bit, team_2_withdrew bit, 
	tournament_code smallint, tournament_name varchar(50),
	coach_name varchar(50), coach_flag varchar(100), fifa_ranking smallint,
	is_penalty bit, is_own_goal bit, sort_order smallint)

CREATE TABLE #tmp_round_codes (round_code varchar(10))

IF (@round_code = 'PO') --Playoffs
BEGIN
	INSERT INTO #tmp_round_codes
	SELECT '16'
	UNION
	SELECT 'QF'
	UNION
	SELECT 'SF'
	UNION
	SELECT '3P'
	UNION
	SELECT 'FF'
END
ELSE IF (@round_code = '') --Get everything
BEGIN
	INSERT INTO #tmp_round_codes
	SELECT DISTINCT round_code
	FROM wc_round
END
ELSE
BEGIN
	INSERT INTO #tmp_round_codes
	SELECT @round_code
	UNION
	SELECT CONVERT(VARCHAR(10),@round_code + 'a')
END

IF (@team_code = 0)
BEGIN
	INSERT INTO #tmp_games
	SELECT 1 as row_type, g.round_number, g.round_code, r.round_name, g.game_code, g.game_number, g.game_time, 
		t1.team_code as team_1_code, t1.team_name as team_1_name, g.team_1_normal_time_score, g.team_1_extra_time_score, g.team_1_penalties_score,
		t2.team_code as team_2_code, t2.team_name as team_2_name, g.team_2_normal_time_score, g.team_2_extra_time_score, g.team_2_penalties_score, 
		t1.flag_name as team_1_flag_name, t2.flag_name as team_2_flag_name,
		g.location, 
		CONVERT(bit,CASE WHEN g.team_1_normal_time_score is null THEN
			1 ELSE 0 END) as team_1_withdrew,
		CONVERT(bit,CASE WHEN g.team_2_normal_time_score is null THEN
			1 ELSE 0 END) as team_2_withdrew,
		g.tournament_code, '' as tournament_name,
		'' as coach_name, '' as coach_flag, 0 as fifa_ranking, 
		null as is_penalty, null as is_own_goal, -1 as sort_order
	FROM wc_game g
	JOIN wc_round r ON g.round_code = r.round_code
	JOIN wc_team t1 ON g.team_1_code = t1.team_code
	JOIN wc_team t2 ON g.team_2_code = t2.team_code
	JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE g.tournament_code = @tournament_code
	and (g.round_number = @round_number or @round_number = 0)
	--ORDER BY g.game_time, g.game_number--, gl.goal_time, isnull(gl.injury_time,0)
END
ELSE 
BEGIN
	INSERT INTO #tmp_games
	SELECT 1 as row_type, g.round_number, g.round_code, r.round_name, g.game_code, g.game_number, g.game_time, 
		t1.team_code as team_1_code, t1.team_name as team_1_name, g.team_1_normal_time_score, g.team_1_extra_time_score, g.team_1_penalties_score,
		t2.team_code as team_2_code, t2.team_name as team_2_name, g.team_2_normal_time_score, g.team_2_extra_time_score, g.team_2_penalties_score, 
		t1.flag_name as team_1_flag_name, t2.flag_name as team_2_flag_name,
		g.location, 
		CONVERT(bit,CASE WHEN g.team_1_normal_time_score is null THEN
			1 ELSE 0 END) as team_1_withdrew,
		CONVERT(bit,CASE WHEN g.team_2_normal_time_score is null THEN
			1 ELSE 0 END) as team_2_withdrew,
		t.tournament_code, t.[name] as tournament_name, 
		isnull(te.coach_name,'') as coach_name, isnull(t3.flag_name,'') as coach_flag,
		isnull(te.fifa_ranking,0) as fifa_ranking, 
		null as is_penalty, null as is_own_goal, -1 as sort_order
	FROM wc_game g
	JOIN wc_round r ON g.round_code = r.round_code
	JOIN wc_team t1 ON g.team_1_code = t1.team_code
	JOIN wc_team t2 ON g.team_2_code = t2.team_code
	JOIN wc_tournament t ON g.tournament_code = t.tournament_code
	LEFT OUTER JOIN wc_tournament_team_entry te ON t.tournament_code = te.tournament_code 
	LEFT OUTER JOIN wc_team t3 ON te.coach_nationality = t3.team_name
	JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE (g.team_1_code = @team_code
	or g.team_2_code = @team_code)
	and te.team_code = @team_code
	--ORDER BY g.game_time, g.game_number--, gl.goal_time, isnull(gl.injury_time,0)
END

--Insert Team 1 Scorers
INSERT INTO #tmp_games
SELECT 2 as row_type, g.round_number, g.round_code, CONVERT(varchar(50),'') as round_name,
	g.game_code, g.game_number, g.game_time, 
	p.player_code as team_1_code, CONVERT(varchar(50),p.player_name) as team_1_name, 
	gl.goal_time as team_1_normal_time_score, injury_time as team_1_extra_time_score, 0 as team_1_penalties_score,
	p.team_code as team_2_code, '' as team_2_name, 
	0 as team_2_normal_time_score, 0 as team_2_extra_time_score, 0 as team_2_penalties_score,
	'Soccerball_svg.png' as team_1_flag_name, '' as team_2_flag_name,
	g.location, 0 as team_1_withdrew, 0 as team_2_withdrew,
	g.tournament_code, '' as tournament_name,
	'' as coach_name, '' as coach_flag, 0 as fifa_ranking, 
	gl.is_penalty, gl.is_own_goal, gl.goal_time + injury_time as sort_order
FROM wc_game g 
--JOIN wc_team t ON g.team_1_code = t.team_code
JOIN wc_goal gl ON gl.game_code = g.game_code
JOIN wc_player p ON p.player_code = gl.player_code and g.team_1_code = p.team_code
JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and @show_goals = 1

--Insert Team 2 Scorers
INSERT INTO #tmp_games
SELECT 2 as row_type, g.round_number, g.round_code, CONVERT(varchar(50),'') as round_name,
	g.game_code, g.game_number, g.game_time, 
	p.team_code as team_1_code, '' as team_2_name, 
	0 as team_1_normal_time_score, 0 as team_1_extra_time_score, 0 as team_1_penalties_score,
	p.player_code as team_2_code, CONVERT(varchar(50),p.player_name) as team_2_name, 
	gl.goal_time as team_2_normal_time_score, injury_time as team_2_extra_time_score, 0 as team_2_penalties_score,
	'' as team_1_flag_name, 'Soccerball_svg.png' as team_2_flag_name,
	g.location, 0 as team_1_withdrew, 0 as team_2_withdrew,
	g.tournament_code, '' as tournament_name,
	'' as coach_name, '' as coach_flag, 0 as fifa_ranking, 
	gl.is_penalty, gl.is_own_goal, gl.goal_time + injury_time as sort_order
FROM wc_game g
--JOIN wc_team t ON g.team_1_code = t.team_code
JOIN wc_goal gl ON gl.game_code = g.game_code
JOIN wc_player p ON p.player_code = gl.player_code and g.team_2_code = p.team_code
JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and @show_goals = 1

--Insert Team 1 Penalty Shootout Scorers
INSERT INTO #tmp_games
SELECT 3 as row_type, g.round_number, g.round_code, CONVERT(varchar(50),'') as round_name,
	g.game_code, g.game_number, g.game_time, 
	p.player_code as team_1_code, CONVERT(varchar(50),p.player_name) as team_1_name, 
	0 as team_1_normal_time_score, 0 as team_1_extra_time_score, ps.scored as team_1_penalties_score,
	p.team_code as team_2_code, '' as team_2_name, 
	0 as team_2_normal_time_score, 0 as team_2_extra_time_score, 0 as team_2_penalties_score,
	CASE WHEN ps.scored = 1 THEN 'Soccerball_svg.png' ELSE 'Soccerball_Miss_svg.png' END as team_1_flag_name, '' as team_2_flag_name,
	g.location, 0 as team_1_withdrew, 0 as team_2_withdrew,
	g.tournament_code, '' as tournament_name,
	'' as coach_name, '' as coach_flag, 0 as fifa_ranking, 
	1 as is_penalty, 0 as is_own_goal, penalty_order as sort_order
FROM wc_game g 
JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
JOIN wc_player p ON p.player_code = ps.player_code and g.team_1_code = p.team_code
JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and @show_goals = 1

--Insert Team 2 Penalty Shootout Scorers
INSERT INTO #tmp_games
SELECT 3 as row_type, g.round_number, g.round_code, CONVERT(varchar(50),'') as round_name,
	g.game_code, g.game_number, g.game_time, 
	p.player_code as team_1_code, '' as team_1_name, 
	0 as team_1_normal_time_score, 0 as team_1_extra_time_score, 0 as team_1_penalties_score,
	p.team_code as team_2_code, CONVERT(varchar(50),p.player_name) as team_2_name, 
	0 as team_2_normal_time_score, 0 as team_2_extra_time_score, ps.scored as team_2_penalties_score,
	'' as team_1_flag_name, CASE WHEN ps.scored = 1 THEN 'Soccerball_svg.png' ELSE 'Soccerball_Miss_svg.png' END as team_2_flag_name,
	g.location, 0 as team_1_withdrew, 0 as team_2_withdrew,
	g.tournament_code, '' as tournament_name,
	'' as coach_name, '' as coach_flag, 0 as fifa_ranking, 
	1 as is_penalty, 0 as is_own_goal, penalty_order as sort_order
FROM wc_game g 
JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
JOIN wc_player p ON p.player_code = ps.player_code and g.team_2_code = p.team_code
JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and @show_goals = 1

SELECT row_type, round_number, round_code, round_name,
	game_code, game_number, game_time,
	team_1_code, team_1_name, 
	team_1_normal_time_score, team_1_extra_time_score, team_1_penalties_score,
	team_2_code, team_2_name,
	team_2_normal_time_score, team_2_extra_time_score, team_2_penalties_score,
	team_1_flag_name, team_2_flag_name,
	location, team_1_withdrew, team_2_withdrew, 
	tournament_code, tournament_name,
	coach_name, coach_flag, fifa_ranking, 
	is_penalty, is_own_goal
FROM #tmp_games
WHERE game_code = 58
ORDER BY CASE WHEN @team_code > 0 THEN game_time END DESC, 
	CASE WHEN @team_code = 0 THEN game_time END ASC, 
	game_number, row_type, sort_order

DROP TABLE #tmp_games
DROP TABLE #tmp_round_codes