CREATE PROCEDURE [dbo].[FB_GetGameGoalAssignments]  
	@TournamentCode INT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tmp_games_for_assigning (row_type INT, 
		round_number INT, 
		round_code VARCHAR(10), 
		round_name VARCHAR(50), 
		game_code INT, 
		game_number INT, 
		game_time datetime,
		team_1_code INT, 
		team_1_name VARCHAR(200), 
		team_1_normal_time_score INT, team_1_extra_time_score INT, team_1_penalties_score INT, team_1_elo_rating INT, team_1_pregame_elo_rating INT, team_1_postgame_elo_rating INT,
		team_2_code INT, 
		team_2_name VARCHAR(200),
		team_2_normal_time_score INT, team_2_extra_time_score INT, team_2_penalties_score INT, team_2_elo_rating INT, team_2_pregame_elo_rating INT, team_2_postgame_elo_rating INT,
		team_1_flag_name VARCHAR(100), team_2_flag_name VARCHAR(100),
		team_1_withdrew BIT, team_2_withdrew BIT, 
		[location] VARCHAR(100), 
		tournament_code INT, tournament_name VARCHAR(50),
		coach_name VARCHAR(50), coach_flag VARCHAR(100), 
		isPen BIT, 
		isOg BIT, 
		sortOrder int)

	INSERT INTO #tmp_games_for_assigning
	exec [FB_GetGames] @TournamentCode = @TournamentCode

	CREATE TABLE #tmp_goals (game_code INT, total_goals INT)

	INSERT INTO #tmp_goals
	SELECT ga.game_code, ISNULL(COUNT(gl.goal_code),0)
	FROM #tmp_games_for_assigning ga
	LEFT JOIN wc_goal gl ON ga.game_code = gl.game_code AND gl.player_code > 0
	GROUP BY ga.game_code

	CREATE TABLE #tmp_penalty_shootout_goals (game_code INT, total_penalties INT)

	INSERT INTO #tmp_penalty_shootout_goals
	SELECT ga.game_code, ISNULL(COUNT(pso.penalty_code),0)
	FROM #tmp_games_for_assigning ga
	LEFT JOIN wc_penalty_shootout pso ON ga.game_code = pso.game_code and pso.scored = 1
	GROUP BY ga.game_code

	SELECT ga.game_code AS GameCode, 
		ga.game_number AS GameNumber, 
		ga.game_time AS GameTime, 
		ga.team_1_name AS Team1Name, 
		ga.team_2_name AS Team2Name,
		CONVERT(VARCHAR(3),ga.team_1_normal_time_score+ISNULL(ga.team_1_extra_time_score,'')) + ' - ' + 
		CONVERT(VARCHAR(3),ga.team_2_normal_time_score+ISNULL(ga.team_2_extra_time_score,'')) + 
		CASE WHEN not ga.team_1_extra_time_score is NULL THEN CASE WHEN ga.team_1_penalties_score is NULL THEN ' (et)' ELSE '' END ELSE '' END + 
		CASE WHEN not ga.team_1_penalties_score is NULL THEN ' (' + CONVERT(VARCHAR(3),ga.team_1_penalties_score) + ' - ' + CONVERT(VARCHAR(3),ga.team_2_penalties_score) + ' pens)' ELSE '' END AS Score,
		ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
		ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0) AS TotalGameTableGoals,
		(ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
		ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0)) - g.total_goals AS TotalGoalTableGoals,
		(ISNULL(ga.team_1_penalties_score,0) + ISNULL(ga.team_2_penalties_score,0)) AS TotalGameTablePenaltyShootoutGoals,
		(ISNULL(ga.team_1_penalties_score,0) + ISNULL(ga.team_2_penalties_score,0)) - pso.total_penalties AS TotalPenaltyShootoutTableGoals
	FROM #tmp_games_for_assigning ga
	JOIN #tmp_goals g ON ga.game_code = g.game_code
	JOIN #tmp_penalty_shootout_goals pso ON pso.game_code = g.game_code
	--WHERE ga.game_code = 65
	/*GROUP BY ga.game_code, ga.game_number, ga.game_time, ga.team_1_name, ga.team_2_name,
		CONVERT(VARCHAR(3),ga.team_1_normal_time_score+ISNULL(ga.team_1_extra_time_score,'')) + ' - ' + 
		CONVERT(VARCHAR(3),ga.team_2_normal_time_score+ISNULL(ga.team_2_extra_time_score,'')) + 
		CASE WHEN not ga.team_1_extra_time_score is NULL THEN CASE WHEN ga.team_1_penalties_score is NULL THEN ' (et)' ELSE '' END ELSE '' END + 
		CASE WHEN not ga.team_1_penalties_score is NULL THEN ' (' + CONVERT(VARCHAR(3),ga.team_1_penalties_score) + ' - ' + CONVERT(VARCHAR(3),ga.team_2_penalties_score) + ' pens)' ELSE '' END,
		ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
		ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0),
		(ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
		ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0)),
		ISNULL(ga.team_1_penalties_score,0) + ISNULL(ga.team_2_penalties_score,0)*/

	DROP TABLE #tmp_games_for_assigning
	DROP TABLE #tmp_penalty_shootout_goals
	DROP TABLE #tmp_goals
END