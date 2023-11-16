CREATE PROCEDURE [dbo].[spWC_GetGameList] -- 18, 1, 'A', 1, 0
	@tournament_code INT,
	@round_number INT,
	@round_code VARCHAR(10),
	@show_goals BIT,
	@team_code INT
AS
BEGIN
	SET NOCOUNT ON

	/*
	SELECT CONVERT(INT,NULL) AS row_type,  
		CONVERT(INT,NULL) AS round_number, 
		CONVERT(VARCHAR(10),NULL) AS round_code,
		CONVERT(VARCHAR(50),NULL) AS round_name,
		CONVERT(INT,NULL) AS game_code,
		CONVERT(INT,NULL) AS game_number,
		CONVERT(DATETIME,NULL) AS game_time, 
		CONVERT(INT,NULL) AS team_1_code, 
		CONVERT(VARCHAR(100),NULL) AS team_1_name, 
		CONVERT(INT,NULL) AS team_1_normal_time_score, 
		CONVERT(INT,NULL) AS team_1_extra_time_score, 
		CONVERT(INT,NULL) AS team_1_penalties_score,
		CONVERT(INT,NULL) AS team_2_code, 
		CONVERT(VARCHAR(100),NULL) AS team_2_name, 
		CONVERT(INT,NULL) AS team_2_normal_time_score, 
		CONVERT(INT,NULL) AS team_2_extra_time_score, 
		CONVERT(INT,NULL) AS team_2_penalties_score,
		CONVERT(VARCHAR(100),NULL) AS team_1_flag_name, 
		CONVERT(VARCHAR(100),NULL) AS team_2_flag_name,
		CONVERT(VARCHAR(100),NULL) AS location,
		CONVERT(BIT,NULL) AS team_1_withdrew,
		CONVERT(BIT,NULL) AS team_2_withdrew, 
		CONVERT(INT,NULL) AS tournament_code,
		CONVERT(VARCHAR(50),NULL) AS tournament_name, 
		CONVERT(VARCHAR(50),NULL) AS coach_name, 
		CONVERT(VARCHAR(100),NULL) AS coach_flag,
		CONVERT(INT,NULL) AS fifa_ranking,
		CONVERT(BIT,NULL) AS is_penalty,
		CONVERT(BIT,NULL) AS is_own_goal
	*/

	CREATE TABLE #tmp_games (row_type INT, round_number INT, 
		round_code VARCHAR(10), round_name VARCHAR(50), 
		game_code INT, game_number INT, game_time DATETIME,
		team_1_code INT, team_1_name VARCHAR(200), 
		team_1_normal_time_score INT, team_1_extra_time_score INT, team_1_penalties_score INT,
		team_2_code INT, team_2_name VARCHAR(200),
		team_2_normal_time_score INT, team_2_extra_time_score INT, team_2_penalties_score INT,
		team_1_flag_name VARCHAR(100), team_2_flag_name VARCHAR(100),
		location VARCHAR(100), team_1_withdrew BIT, team_2_withdrew BIT, 
		tournament_code INT, tournament_name VARCHAR(50),
		coach_name VARCHAR(50), coach_flag VARCHAR(100), fifa_ranking INT,
		is_penalty BIT, is_own_goal BIT, sort_order INT)

	CREATE TABLE #tmp_round_codes (round_code VARCHAR(10))

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
		SELECT 1 AS row_type, g.round_number, g.round_code, r.round_name, g.game_code, g.game_number, g.game_time, 
			t1.team_code AS team_1_code, t1.team_name AS team_1_name, g.team_1_normal_time_score, g.team_1_extra_time_score, g.team_1_penalties_score,
			t2.team_code AS team_2_code, t2.team_name AS team_2_name, g.team_2_normal_time_score, g.team_2_extra_time_score, g.team_2_penalties_score, 
			t1.flag_name AS team_1_flag_name, t2.flag_name AS team_2_flag_name,
			g.location, 
			CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN
				1 ELSE 0 END) AS team_1_withdrew,
			CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN
				1 ELSE 0 END) AS team_2_withdrew,
			g.tournament_code, '' AS tournament_name,
			'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
			NULL AS is_penalty, NULL AS is_own_goal, -1 AS sort_order
		FROM wc_game g
		JOIN wc_round r ON g.round_code = r.round_code
		JOIN wc_team t1 ON g.team_1_code = t1.team_code
		JOIN wc_team t2 ON g.team_2_code = t2.team_code
		JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
		WHERE g.tournament_code = @tournament_code
		AND (g.round_number = @round_number or @round_number = 0)
	END
	ELSE 
	BEGIN
		INSERT INTO #tmp_games
		SELECT 1 AS row_type, g.round_number, g.round_code, r.round_name, g.game_code, g.game_number, g.game_time, 
			t1.team_code AS team_1_code, t1.team_name AS team_1_name, g.team_1_normal_time_score, g.team_1_extra_time_score, g.team_1_penalties_score,
			t2.team_code AS team_2_code, t2.team_name AS team_2_name, g.team_2_normal_time_score, g.team_2_extra_time_score, g.team_2_penalties_score, 
			t1.flag_name AS team_1_flag_name, t2.flag_name AS team_2_flag_name,
			g.location, 
			CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN
				1 ELSE 0 END) AS team_1_withdrew,
			CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN
				1 ELSE 0 END) AS team_2_withdrew,
			t.tournament_code, t.[name] AS tournament_name, 
			ISNULL(te.coach_name,'') AS coach_name, ISNULL(t3.flag_name,'') AS coach_flag,
			ISNULL(te.fifa_ranking,0) AS fifa_ranking, 
			NULL AS is_penalty, NULL AS is_own_goal, -1 AS sort_order
		FROM wc_game g
		JOIN wc_round r ON g.round_code = r.round_code
		JOIN wc_team t1 ON g.team_1_code = t1.team_code
		JOIN wc_team t2 ON g.team_2_code = t2.team_code
		JOIN wc_tournament t ON g.tournament_code = t.tournament_code
		LEFT JOIN wc_tournament_team_entry te ON t.tournament_code = te.tournament_code 
		LEFT JOIN wc_team t3 ON te.coach_nationality = t3.team_name
		JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
		WHERE (g.team_1_code = @team_code
		or g.team_2_code = @team_code)
		and te.team_code = @team_code
	END

	--Insert Team 1 Scorers
	INSERT INTO #tmp_games
	SELECT 2 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
		g.game_code, g.game_number, g.game_time, 
		p.player_code AS team_1_code, CONVERT(VARCHAR(50),p.player_name) AS team_1_name, 
		gl.goal_time AS team_1_normal_time_score, injury_time AS team_1_extra_time_score, 0 AS team_1_penalties_score,
		p.team_code AS team_2_code, '' AS team_2_name, 
		0 AS team_2_normal_time_score, 0 AS team_2_extra_time_score, 0 AS team_2_penalties_score,
		'Soccerball_svg.png' AS team_1_flag_name, '' AS team_2_flag_name,
		g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
		g.tournament_code, '' AS tournament_name,
		'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
		gl.is_penalty, gl.is_own_goal, gl.goal_time + injury_time AS sort_order
	FROM wc_game g 
	JOIN wc_goal gl ON gl.game_code = g.game_code
	JOIN wc_player p ON p.player_code = gl.player_code and g.team_1_code = p.team_code
	JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE g.tournament_code = @tournament_code
	and g.round_number = @round_number
	and @show_goals = 1

	--Insert Team 2 Scorers
	INSERT INTO #tmp_games
	SELECT 2 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
		g.game_code, g.game_number, g.game_time, 
		p.team_code AS team_1_code, '' AS team_1_name, 
		0 AS team_1_normal_time_score, 0 AS team_1_extra_time_score, 0 AS team_1_penalties_score,
		p.player_code AS team_2_code, CONVERT(VARCHAR(50),p.player_name) AS team_2_name, 
		gl.goal_time AS team_2_normal_time_score, injury_time AS team_2_extra_time_score, 0 AS team_2_penalties_score,
		'' AS team_1_flag_name, 'Soccerball_svg.png' AS team_2_flag_name,
		g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
		g.tournament_code, '' AS tournament_name,
		'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
		gl.is_penalty, gl.is_own_goal, gl.goal_time + injury_time AS sort_order
	FROM wc_game g
	JOIN wc_goal gl ON gl.game_code = g.game_code
	JOIN wc_player p ON p.player_code = gl.player_code and g.team_2_code = p.team_code
	JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE g.tournament_code = @tournament_code
	and g.round_number = @round_number
	and @show_goals = 1

	--Insert Team 1 Penalty Shootout Scorers
	INSERT INTO #tmp_games
	SELECT 3 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
		g.game_code, g.game_number, g.game_time, 
		p.player_code AS team_1_code, CONVERT(VARCHAR(50),p.player_name) AS team_1_name, 
		0 AS team_1_normal_time_score, 0 AS team_1_extra_time_score, ps.scored AS team_1_penalties_score,
		p.team_code AS team_2_code, '' AS team_2_name, 
		0 AS team_2_normal_time_score, 0 AS team_2_extra_time_score, 0 AS team_2_penalties_score,
		CASE WHEN ps.scored = 1 THEN 'Soccerball_svg.png' ELSE 'Soccerball_Miss_svg.png' END AS team_1_flag_name, '' AS team_2_flag_name,
		g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
		g.tournament_code, '' AS tournament_name,
		'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
		1 AS is_penalty, 0 AS is_own_goal, penalty_order AS sort_order
	FROM wc_game g 
	JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
	JOIN wc_player p ON p.player_code = ps.player_code and g.team_1_code = p.team_code
	JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE g.tournament_code = @tournament_code
	and g.round_number = @round_number
	and @show_goals = 1

	--Insert Team 2 Penalty Shootout Scorers
	INSERT INTO #tmp_games
	SELECT 3 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
		g.game_code, g.game_number, g.game_time, 
		p.team_code AS team_1_code, '' AS team_1_name, 
		0 AS team_1_normal_time_score, 0 AS team_1_extra_time_score, 0 AS team_1_penalties_score,
		p.player_code AS team_2_code, CONVERT(VARCHAR(50),p.player_name) AS team_2_name, 
		0 AS team_2_normal_time_score, 0 AS team_2_extra_time_score, ps.scored AS team_2_penalties_score,
		'' AS team_1_flag_name, CASE WHEN ps.scored = 1 THEN 'Soccerball_svg.png' ELSE 'Soccerball_Miss_svg.png' END AS team_2_flag_name,
		g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
		g.tournament_code, '' AS tournament_name,
		'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
		1 AS is_penalty, 0 AS is_own_goal, penalty_order AS sort_order
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
	--WHERE game_code = 58
	ORDER BY CASE WHEN @team_code > 0 THEN game_time END DESC, 
		CASE WHEN @team_code = 0 THEN game_time END ASC, 
		game_number, game_code, row_type, sort_order

	DROP TABLE #tmp_games
	DROP TABLE #tmp_round_codes
END