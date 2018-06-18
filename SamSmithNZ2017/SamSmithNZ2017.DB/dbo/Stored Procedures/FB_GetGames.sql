CREATE PROCEDURE [dbo].[FB_GetGames] 
	@TournamentCode INT = NULL,
	@RoundNumber INT = NULL,
	@RoundCode VARCHAR(10) = NULL,
	@TeamCode INT = NULL,
	@IncludeGoals BIT = 0,
	@GameCode INT = NULL
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
			g.team_1_elo_rating AS Team1EloRating,
			t2.team_code AS Team2Code, 
			t2.team_name AS Team2Name, 
			g.team_2_normal_time_score AS Team2NormalTimeScore, 
			g.team_2_extra_time_score AS Team2ExtraTimeScore, 
			g.team_2_penalties_score AS Team2PenaltiesScore, 
			g.team_2_elo_rating AS Team2EloRating,
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
			g.team_1_elo_rating AS Team1EloRating,
			t2.team_code AS Team2Code, 
			t2.team_name AS Team2Name, 
			g.team_2_normal_time_score AS Team2NormalTimeScore, 
			g.team_2_extra_time_score AS Team2ExtraTimeScore, 
			g.team_2_penalties_score AS Team2PenaltiesScore, 
			g.team_2_elo_rating AS Team2EloRating,
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
		AND (@GameCode IS NULL OR g.game_code = @GameCode)

		--Normal team 1 goal
		UNION
		SELECT 
			2 AS RowType, --2 is a player in normal/extra time
			g.round_number AS RoundNumber, 
			NULL AS RoundCode, 
			CONVERT(VARCHAR(50),'') AS RoundName,
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime, 
			p.player_code AS Team1Code, 
			CONVERT(VARCHAR(50),p.player_name) AS Team1Name, 
			gl.goal_time AS Team1NormalTimeScore, 
			gl.injury_time AS Team1ExtraTimeScore, 
			NULL AS Team1PenaltiesScore,
			NULL AS Team1EloRating,
			p.team_code AS Team2Code, 
			'' AS Team2Name, 
			NULL AS Team2NormalTimeScore, 
			NULL AS Team2ExtraTimeScore, 
			NULL AS Team2PenaltiesScore,
			NULL AS Team2EloRating,
			'Soccerball_svg.png' AS Team1FlagName, 
			NULL AS Team2FlagName,
			0 AS Team1withdrew, 
			0 AS Team2withdrew,
			NULL AS [Location],
			g.tournament_code AS TournamentName, 
			'' AS TournamentName,
			'' AS CoachName, 
			'' AS CoachFlag, 
			0 AS FifaRanking, 
			gl.is_penalty AS IsPenalty, 
			gl.is_own_goal AS IsOwnGoal, 
			ISNULL(gl.goal_time,0) + ISNULL(gl.injury_time,0) AS SortOrder
		FROM wc_game g 
		--JOIN wc_team t ON g.team_1_code = t.team_code
		JOIN wc_goal gl ON gl.game_code = g.game_code
		JOIN wc_player p ON p.player_code = gl.player_code AND g.team_1_code = p.team_code AND gl.is_own_goal = 0
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		AND @IncludeGoals = 1
		AND (@GameCode IS NULL OR g.game_code = @GameCode)

		--Team 2 scored own goal (counts as team 1 goal)
		UNION
		SELECT 
			2 AS RowType, --2 is a player in normal/extra time
			g.round_number AS RoundNumber, 
			NULL AS RoundCode, 
			CONVERT(VARCHAR(50),'') AS RoundName,
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime, 
			p.player_code AS Team1Code, 
			CONVERT(VARCHAR(50),p.player_name) AS Team1Name, 
			gl.goal_time AS Team1NormalTimeScore, 
			gl.injury_time AS Team1ExtraTimeScore, 
			NULL AS Team1PenaltiesScore,
			NULL AS Team1EloRating,
			p.team_code AS Team2Code, 
			'' AS Team2Name, 
			NULL AS Team2NormalTimeScore, 
			NULL AS Team2ExtraTimeScore, 
			NULL AS Team2PenaltiesScore,
			NULL AS Team2EloRating,
			'Soccerball_svg.png' AS Team1FlagName, 
			NULL AS Team2FlagName,
			0 AS Team1withdrew, 
			0 AS Team2withdrew,
			NULL AS [Location],
			g.tournament_code AS TournamentName, 
			'' AS TournamentName,
			'' AS CoachName, 
			'' AS CoachFlag, 
			0 AS FifaRanking, 
			gl.is_penalty AS IsPenalty, 
			gl.is_own_goal AS IsOwnGoal, 
			ISNULL(gl.goal_time,0) + ISNULL(gl.injury_time,0) AS SortOrder
		FROM wc_game g 
		--JOIN wc_team t ON g.team_1_code = t.team_code
		JOIN wc_goal gl ON gl.game_code = g.game_code
		JOIN wc_player p ON p.player_code = gl.player_code AND g.team_2_code = p.team_code AND gl.is_own_goal = 1
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		AND @IncludeGoals = 1
		AND (@GameCode IS NULL OR g.game_code = @GameCode)
		
		--Normal team 2 goal
		UNION
		SELECT 
			2 AS RowType, --2 is a player in normal/extra time
			g.round_number AS RoundNumber, 
			NULL AS RoundCode, 
			CONVERT(VARCHAR(50),'') AS RoundName,
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime, 
			p.team_code AS Team1Code, 
			'' AS Team1Name, 
			NULL AS Team1NormalTimeScore, 
			NULL AS Team1ExtraTimeScore, 
			NULL AS Team1PenaltiesScore,
			NULL AS Team1EloRating,
			p.player_code AS Team2Code, 
			CONVERT(VARCHAR(50),p.player_name) AS Team2Name, 
			gl.goal_time AS Team2NormalTimeScore, 
			gl.injury_time AS Team2ExtraTimeScore, 
			NULL AS Team2PenaltiesScore,
			NULL AS Team2EloRating,
			NULL AS Team1FlagName, 
			'Soccerball_svg.png' AS Team2FlagName,
			0 AS Team1withdrew, 
			0 AS Team2withdrew,
			NULL AS [Location],
			g.tournament_code AS TournamentCode, 
			'' AS TournamentName,
			'' AS CoachName, 
			'' AS CoachFlag, 
			0 AS FifaRanking, 
			gl.is_penalty AS IsPenalty, 
			gl.is_own_goal AS IsOwnGoal, 
			ISNULL(gl.goal_time,0) + ISNULL(gl.injury_time,0) AS sort_order
		FROM wc_game g 
		--JOIN wc_team t ON g.team_1_code = t.team_code
		JOIN wc_goal gl ON gl.game_code = g.game_code
		JOIN wc_player p ON p.player_code = gl.player_code AND g.team_2_code = p.team_code AND gl.is_own_goal = 0
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		AND @IncludeGoals = 1
		AND (@GameCode IS NULL OR g.game_code = @GameCode)
		
		--Team 1 scored own goal (counts as team 2 goal)
		UNION
		SELECT 
			2 AS RowType, --2 is a player in normal/extra time
			g.round_number AS RoundNumber, 
			NULL AS RoundCode, 
			CONVERT(VARCHAR(50),'') AS RoundName,
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime, 
			p.team_code AS Team1Code, 
			'' AS Team1Name, 
			NULL AS Team1NormalTimeScore, 
			NULL AS Team1ExtraTimeScore, 
			NULL AS Team1PenaltiesScore,
			NULL AS Team1EloRating,
			p.player_code AS Team2Code, 
			CONVERT(VARCHAR(50),p.player_name) AS Team2Name, 
			gl.goal_time AS Team2NormalTimeScore, 
			gl.injury_time AS Team2ExtraTimeScore, 
			NULL AS Team2PenaltiesScore,
			NULL AS Team2EloRating,
			NULL AS Team1FlagName, 
			'Soccerball_svg.png' AS Team2FlagName,
			0 AS Team1withdrew, 
			0 AS Team2withdrew,
			NULL AS [Location],
			g.tournament_code AS TournamentCode, 
			'' AS TournamentName,
			'' AS CoachName, 
			'' AS CoachFlag, 
			0 AS FifaRanking, 
			gl.is_penalty AS IsPenalty, 
			gl.is_own_goal AS IsOwnGoal, 
			ISNULL(gl.goal_time,0) + ISNULL(gl.injury_time,0) AS sort_order
		FROM wc_game g 
		--JOIN wc_team t ON g.team_1_code = t.team_code
		JOIN wc_goal gl ON gl.game_code = g.game_code
		JOIN wc_player p ON p.player_code = gl.player_code AND g.team_1_code = p.team_code AND gl.is_own_goal = 1
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		AND @IncludeGoals = 1
		AND (@GameCode IS NULL OR g.game_code = @GameCode)
		
		--Insert Team 1 Penalty Shootout Scorers
		UNION
		SELECT 3 AS RowType, 			
			g.round_number AS RoundNumber, 
			NULL AS RoundCode, 
			CONVERT(VARCHAR(50),'') AS RoundName,
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime,  
			p.player_code AS Team1Code, 
			CONVERT(VARCHAR(50),p.player_name) AS Team1Name, 
			0 AS Team1NormalTimeScore, 
			0 AS Team1ExtraTimeScore, 
			ps.scored AS Team1PenaltiesScore,
			NULL AS Team1EloRating,
			p.team_code AS Team2Code, 
			'' AS Team2Name, 
			0 AS Team2NormalTimeScore, 
			0 AS Team2ExtraTimeScore, 
			NULL AS Team2PenaltiesScore,
			NULL AS Team2EloRating,
			NULL AS Team1FlagName, 
			NULL AS Team2FlagName,
			0 AS Team1withdrew, 
			0 AS Team2withdrew,
			NULL AS [Location],
			g.tournament_code AS TournamentCode, 
			'' AS TournamentName,
			'' AS CoachName, 
			'' AS CoachFlag, 
			0 AS FifaRanking, 
			0 AS IsPenalty, 
			0 AS IsOwnGoal,  
			penalty_order AS sort_order
		FROM wc_game g 
		JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
		JOIN wc_player p ON p.player_code = ps.player_code AND g.team_1_code = p.team_code
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		AND @IncludeGoals = 1
		AND (@GameCode IS NULL OR g.game_code = @GameCode)

		--Insert Team 2 Penalty Shootout Scorers
		UNION
		SELECT 3 AS RowType, 			
			g.round_number AS RoundNumber, 
			NULL AS RoundCode, 
			CONVERT(VARCHAR(50),'') AS RoundName,
			g.game_code AS GameCode, 
			g.game_number AS GameNumber, 
			g.game_time AS GameTime,  
			p.player_code AS Team1Code, 
			'' AS Team1Name, 
			0 AS Team1NormalTimeScore, 
			0 AS Team1ExtraTimeScore, 
			NULL AS Team1PenaltiesScore,
			NULL AS Team1EloRating,
			p.player_code AS Team2Code, 
			CONVERT(VARCHAR(50),p.player_name) AS Team2Name, 
			0 AS Team2NormalTimeScore, 
			0 AS Team2ExtraTimeScore, 
			ps.scored AS Team2PenaltiesScore,
			NULL AS Team2EloRating,
			NULL AS Team1FlagName, 
			NULL AS Team2FlagName,
			0 AS Team1withdrew, 
			0 AS Team2withdrew,
			NULL AS [Location],
			g.tournament_code AS TournamentCode, 
			'' AS TournamentName,
			'' AS CoachName, 
			'' AS CoachFlag, 
			0 AS FifaRanking, 
			0 AS IsPenalty, 
			0 AS IsOwnGoal,  
			penalty_order AS sort_order
		FROM wc_game g 
		JOIN wc_penalty_shootout ps ON ps.game_code = g.game_code
		JOIN wc_player p ON p.player_code = ps.player_code AND g.team_2_code = p.team_code
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		AND @IncludeGoals = 1
		AND (@GameCode IS NULL OR g.game_code = @GameCode)

		ORDER BY g.game_time, g.game_number, g.game_code, RowType, SortOrder
	END
END
GO