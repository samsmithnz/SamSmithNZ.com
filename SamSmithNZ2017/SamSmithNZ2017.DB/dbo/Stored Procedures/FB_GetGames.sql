CREATE PROCEDURE [dbo].[FB_GetGames]
	@TournamentCode INT = NULL,
	@RoundNumber INT = NULL,
	@RoundCode VARCHAR(10) = NULL,
	@TeamCode INT = NULL
AS
BEGIN
	
	IF (NOT @TeamCode IS NULL)
	BEGIN
		SELECT 
			1 AS RowType, --1 is a team
			g.round_number AS RoundNumber, 
			g.round_code AS RoundCode, 
			r.round_name AS RoundName, 
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime, 
			t1.team_code AS Team1Code, 
			t1.team_name AS Team1Name, 
			g.team_1_normal_time_score AS Team1NormalTimeScore, 
			g.team_1_extra_time_score AS Team1ExtraTimeScore, 
			g.team_1_penalties_score AS Team1PenaltiesScore,
			t2.team_code AS Team2Code, 
			t2.team_name AS Team2Name, 
			g.team_2_normal_time_score AS Team2NormalTimeScore, 
			g.team_2_extra_time_score AS Team2ExtraTimeScore, 
			g.team_2_penalties_score AS Team2PenaltiesScore, 
			t1.flag_name AS Team1FlagName, 
			t2.flag_name AS Team2FlagName,
			CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team1withdrew,
			CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team2withdrew,
			g.[location] AS [Location], 
			t.tournament_code AS TournamentCode, 
			t.[name] AS TournamentName, 
			ISNULL(te.coach_name,'') AS CoachName, 
			ISNULL(t3.flag_name,'') AS CoachFlag,
			ISNULL(te.fifa_ranking,0) AS FifaRanking, 
			NULL AS IsPenalty, 
			NULL AS IsOwnGoal, 
			1 AS SortOrder
		FROM wc_game g
		JOIN wc_round r ON g.round_code = r.round_code
		JOIN wc_team t1 ON g.team_1_code = t1.team_code
		JOIN wc_team t2 ON g.team_2_code = t2.team_code
		JOIN wc_tournament t ON g.tournament_code = t.tournament_code
		LEFT JOIN wc_tournament_team_entry te ON t.tournament_code = te.tournament_code 
		LEFT JOIN wc_team t3 ON te.coach_nationality = t3.team_name
		--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
		WHERE t.competition_code = 1
		AND (g.team_1_code = @TeamCode OR g.team_2_code = @TeamCode)
		AND te.team_code = @TeamCode
		ORDER BY g.game_time DESC, g.game_number, SortOrder--, gl.goal_time, ISNULL(gl.injury_time,0)
	END
	ELSE
	BEGIN		
		SELECT 1 AS RowType, --1 is a team
			g.round_number AS RoundNumber, 
			g.round_code AS RoundCode, 
			r.round_name AS RoundName, 
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime, 
			t1.team_code AS Team1Code, 
			t1.team_name AS Team1Name, 
			g.team_1_normal_time_score AS Team1NormalTimeScore, 
			g.team_1_extra_time_score AS Team1ExtraTimeScore, 
			g.team_1_penalties_score AS Team1PenaltiesScore,
			t2.team_code AS Team2Code, 
			t2.team_name AS Team2Name, 
			g.team_2_normal_time_score AS Team2NormalTimeScore, 
			g.team_2_extra_time_score AS Team2ExtraTimeScore, 
			g.team_2_penalties_score AS Team2PenaltiesScore, 
			t1.flag_name AS Team1FlagName, 
			t2.flag_name AS Team2FlagName,
			CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team1withdrew,
			CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team2withdrew,
			g.[location] AS [Location], 
			t.tournament_code AS TournamentCode, 
			t.[name] AS TournamentName, 
			NULL AS CoachName, 
			NULL AS CoachFlag,
			0 AS FifaRanking, 
			NULL AS IsPenalty, 
			NULL AS IsOwnGoal, 
			1 AS SortOrder
		FROM wc_game g
		JOIN wc_team t1 ON g.team_1_code = t1.team_code
		JOIN wc_team t2 ON g.team_2_code = t2.team_code
		JOIN wc_tournament t ON g.tournament_code = t.tournament_code
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)

		--UNION
		--SELECT 
		--	2 AS RowType, --2 is a player in normal/extra time
		--	g.round_number AS RoundNumber, 
		--	NULL AS RoundCode, 
		--	CONVERT(VARCHAR(50),'') AS RoundName,
		--	g.game_code AS GameCode, 
		--	g.game_number AS GameNumber, 
		--	g.game_time AS GameTime, 
		--	p.player_code AS Team1Code, 
		--	CONVERT(VARCHAR(50),p.player_name) AS Team1Name, 
		--	gl.goal_time AS Team1NormalTimeScore, 
		--	gl.injury_time AS Team1ExtraTimeScore, 
		--	NULL AS Team1PenaltiesScore,
		--	p.team_code AS Team2Code, 
		--	'' AS Team2Name, 
		--	NULL AS Team2NormalTimeScore, 
		--	NULL AS Team2ExtraTimeScore, 
		--	NULL AS Team2PenaltiesScore,
		--	'Soccerball_svg.png' AS Team1FlagName, 
		--	NULL AS Team2FlagName,
		--	0 AS Team1withdrew, 
		--	0 AS Team2withdrew,
		--	NULL AS [Location],
		--	g.tournament_code AS TournamentName, 
		--	'' AS TournamentName,
		--	'' AS CoachName, 
		--	'' AS CoachFlag, 
		--	0 AS FifaRanking, 
		--	0 AS IsPenalty, 
		--	0 AS IsOwnGoal, 
		--	ISNULL(gl.goal_time,0) + ISNULL(gl.injury_time,0) AS sort_order
		--FROM wc_game g 
		----JOIN wc_team t ON g.team_1_code = t.team_code
		--JOIN wc_goal gl ON gl.game_code = g.game_code
		--JOIN wc_player p ON p.player_code = gl.player_code and g.team_1_code = p.team_code
		--LEFT JOIN wc_round r ON g.round_code = r.round_code
		--WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		--AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		--AND (@RoundCode IS NULL OR g.round_code = @RoundCode)

		--UNION
		--SELECT 
		--	2 AS RowType, --2 is a player in normal/extra time
		--	g.round_number AS RoundNumber, 
		--	NULL AS RoundCode, 
		--	CONVERT(VARCHAR(50),'') AS RoundName,
		--	g.game_code AS GameCode, 
		--	g.game_number AS GameNumber, 
		--	g.game_time AS GameTime, 
		--	p.team_code AS Team1Code, 
		--	'' AS Team1Name, 
		--	NULL AS Team1NormalTimeScore, 
		--	NULL AS Team1ExtraTimeScore, 
		--	NULL AS Team1PenaltiesScore,
		--	p.player_code AS Team2Code, 
		--	CONVERT(VARCHAR(50),p.player_name) AS Team2Name, 
		--	gl.goal_time AS Team2NormalTimeScore, 
		--	gl.injury_time AS Team2ExtraTimeScore, 
		--	NULL AS Team2PenaltiesScore,
		--	NULL AS Team1FlagName, 
		--	'Soccerball_svg.png' AS Team2FlagName,
		--	0 AS Team1withdrew, 
		--	0 AS Team2withdrew,
		--	NULL AS [Location],
		--	g.tournament_code AS TournamentName, 
		--	'' AS TournamentName,
		--	'' AS CoachName, 
		--	'' AS CoachFlag, 
		--	0 AS FifaRanking, 
		--	0 AS IsPenalty, 
		--	0 AS IsOwnGoal, 
		--	ISNULL(gl.goal_time,0) + ISNULL(gl.injury_time,0) AS sort_order
		--FROM wc_game g 
		----JOIN wc_team t ON g.team_1_code = t.team_code
		--JOIN wc_goal gl ON gl.game_code = g.game_code
		--JOIN wc_player p ON p.player_code = gl.player_code and g.team_2_code = p.team_code
		--LEFT JOIN wc_round r ON g.round_code = r.round_code
		--WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		--AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		--AND (@RoundCode IS NULL OR g.round_code = @RoundCode)

		ORDER BY g.game_time, g.game_number, g.game_code, SortOrder
	END
END


----Insert Team 1 Scorers
--INSERT INTO #tmp_games
--SELECT 2 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
--	g.game_code, g.game_number, g.game_time, 
--	p.player_code AS team_1_code, CONVERT(VARCHAR(50),p.player_name) AS team_1_name, 
--	gl.goal_time AS team_1_normal_time_score, injury_time AS team_1_extra_time_score, 0 AS team_1_penalties_score,
--	p.team_code AS team_2_code, '' AS team_2_name, 
--	0 AS team_2_normal_time_score, 0 AS team_2_extra_time_score, 0 AS team_2_penalties_score,
--	'Soccerball_svg.png' AS team_1_flag_name, '' AS team_2_flag_name,
--	g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
--	g.tournament_code, '' AS tournament_name,
--	'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
--	gl.is_penalty, gl.is_own_goal, gl.goal_time + injury_time AS sort_order
--FROM wc_game g 
----JOIN wc_team t ON g.team_1_code = t.team_code
--JOIN wc_goal gl ON gl.game_code = g.game_code
--JOIN wc_player p ON p.player_code = gl.player_code and g.team_1_code = p.team_code
--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
--WHERE g.tournament_code = @tournament_code
--and g.round_number = @round_number
--and @show_goals = 1

----Insert Team 2 Scorers
--INSERT INTO #tmp_games
--SELECT 2 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
--	g.game_code, g.game_number, g.game_time, 
--	p.team_code AS team_1_code, '' AS team_1_name, 
--	0 AS team_1_normal_time_score, 0 AS team_1_extra_time_score, 0 AS team_1_penalties_score,
--	p.player_code AS team_2_code, CONVERT(VARCHAR(50),p.player_name) AS team_2_name, 
--	gl.goal_time AS team_2_normal_time_score, injury_time AS team_2_extra_time_score, 0 AS team_2_penalties_score,
--	'' AS team_1_flag_name, 'Soccerball_svg.png' AS team_2_flag_name,
--	g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
--	g.tournament_code, '' AS tournament_name,
--	'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
--	gl.is_penalty, gl.is_own_goal, gl.goal_time + injury_time AS sort_order
--FROM wc_game g
----JOIN wc_team t ON g.team_1_code = t.team_code
--JOIN wc_goal gl ON gl.game_code = g.game_code
--JOIN wc_player p ON p.player_code = gl.player_code and g.team_2_code = p.team_code
--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
--WHERE g.tournament_code = @tournament_code
--and g.round_number = @round_number
--and @show_goals = 1

----Insert Team 1 Penalty Shootout Scorers
--INSERT INTO #tmp_games
--SELECT 3 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
--	g.game_code, g.game_number, g.game_time, 
--	p.player_code AS team_1_code, CONVERT(VARCHAR(50),p.player_name) AS team_1_name, 
--	0 AS team_1_normal_time_score, 0 AS team_1_extra_time_score, ps.scored AS team_1_penalties_score,
--	p.team_code AS team_2_code, '' AS team_2_name, 
--	0 AS team_2_normal_time_score, 0 AS team_2_extra_time_score, 0 AS team_2_penalties_score,
--	CASE WHEN ps.scored = 1 THEN 'Soccerball_svg.png' ELSE 'Soccerball_Miss_svg.png' END AS team_1_flag_name, '' AS team_2_flag_name,
--	g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
--	g.tournament_code, '' AS tournament_name,
--	'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
--	1 AS is_penalty, 0 AS is_own_goal, penalty_order AS sort_order
--FROM wc_game g 
--JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
--JOIN wc_player p ON p.player_code = ps.player_code and g.team_1_code = p.team_code
--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
--WHERE g.tournament_code = @tournament_code
--and g.round_number = @round_number
--and @show_goals = 1

----Insert Team 2 Penalty Shootout Scorers
--INSERT INTO #tmp_games
--SELECT 3 AS row_type, g.round_number, g.round_code, CONVERT(VARCHAR(50),'') AS round_name,
--	g.game_code, g.game_number, g.game_time, 
--	p.team_code AS team_1_code, '' AS team_1_name, 
--	0 AS team_1_normal_time_score, 0 AS team_1_extra_time_score, 0 AS team_1_penalties_score,
--	p.player_code AS team_2_code, CONVERT(VARCHAR(50),p.player_name) AS team_2_name, 
--	0 AS team_2_normal_time_score, 0 AS team_2_extra_time_score, ps.scored AS team_2_penalties_score,
--	'' AS team_1_flag_name, CASE WHEN ps.scored = 1 THEN 'Soccerball_svg.png' ELSE 'Soccerball_Miss_svg.png' END AS team_2_flag_name,
--	g.location, 0 AS team_1_withdrew, 0 AS team_2_withdrew,
--	g.tournament_code, '' AS tournament_name,
--	'' AS coach_name, '' AS coach_flag, 0 AS fifa_ranking, 
--	1 AS is_penalty, 0 AS is_own_goal, penalty_order AS sort_order
--FROM wc_game g 
--JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
--JOIN wc_player p ON p.player_code = ps.player_code and g.team_2_code = p.team_code
--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
--WHERE g.tournament_code = @tournament_code
--and g.round_number = @round_number
--and @show_goals = 1
GO