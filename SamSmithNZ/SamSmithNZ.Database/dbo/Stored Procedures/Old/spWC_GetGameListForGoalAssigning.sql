CREATE PROCEDURE [dbo].[spWC_GetGameListForGoalAssigning] 
	@tournament_code INT
AS
SET NOCOUNT ON

/*
SELECT CONVERT(INT,NULL) AS game_code,  
	CONVERT(INT,NULL) AS game_number, 
	CONVERT(datetime,NULL) AS game_time, 
	CONVERT(VARCHAR(100),NULL) AS team_1_name, 
	CONVERT(VARCHAR(100),NULL) AS team_2_name,
	CONVERT(VARCHAR(100),NULL) AS score, 
	CONVERT(INT,NULL) AS total_game_table_goals, 
	CONVERT(INT,NULL) AS total_goal_table_goals,
	CONVERT(INT,NULL) AS total_game_table_penalty_shootout_goals,
	CONVERT(INT,NULL) AS total_penalty_shootout_table_goals
*/

CREATE TABLE #tmp_games_for_assigning (row_type INT, round_number INT, 
	round_code VARCHAR(10), round_name VARCHAR(50), 
	game_code INT, game_number INT, game_time datetime,
	team_1_code INT, team_1_name VARCHAR(200), 
	team_1_normal_time_score INT, team_1_extra_time_score INT, team_1_penalties_score INT,
	team_2_code INT, team_2_name VARCHAR(200),
	team_2_normal_time_score INT, team_2_extra_time_score INT, team_2_penalties_score INT,
	team_1_flag_name VARCHAR(100), team_2_flag_name VARCHAR(100),
	location VARCHAR(100), team_1_withdrew bit, team_2_withdrew bit, 
	tournament_code INT, tournament_name VARCHAR(50),
	coach_name VARCHAR(50), coach_flag VARCHAR(100), fifa_ranking INT, ispen bit, isog bit)

INSERT INTO #tmp_games_for_assigning
exec [spWC_GetGameList] @tournament_code,0,'',0,0

CREATE TABLE #tmp_goals (game_code INT, total_goals INT)

INSERT INTO #tmp_goals
SELECT ga.game_code, ISNULL(COUNT(gl.goal_code),0)
FROM #tmp_games_for_assigning ga
LEFT JOIN wc_goal gl ON ga.game_code = gl.game_code
GROUP BY ga.game_code

CREATE TABLE #tmp_penalty_shootout_goals (game_code INT, total_penalties INT)

INSERT INTO #tmp_penalty_shootout_goals
SELECT ga.game_code, ISNULL(COUNT(pso.penalty_code),0)
FROM #tmp_games_for_assigning ga
LEFT JOIN wc_penalty_shootout pso ON ga.game_code = pso.game_code and pso.scored = 1
GROUP BY ga.game_code

SELECT ga.game_code, ga.game_number, ga.game_time, ga.team_1_name, ga.team_2_name,
	CONVERT(VARCHAR(3),ga.team_1_normal_time_score+ISNULL(ga.team_1_extra_time_score,'')) + ' - ' + 
	CONVERT(VARCHAR(3),ga.team_2_normal_time_score+ISNULL(ga.team_2_extra_time_score,'')) + 
	case when not ga.team_1_extra_time_score is NULL then case when ga.team_1_penalties_score is NULL then ' (et)' else '' end else '' end + 
	case when not ga.team_1_penalties_score is NULL then ' (' + CONVERT(VARCHAR(3),ga.team_1_penalties_score) + ' - ' + CONVERT(VARCHAR(3),ga.team_2_penalties_score) + ' pens)' else '' end AS score,
	ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
	ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0) AS total_game_table_goals,
	(ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
	ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0)) - g.total_goals AS total_goal_table_goals,
	(ISNULL(ga.team_1_penalties_score,0) + ISNULL(ga.team_2_penalties_score,0)) AS total_game_table_penalty_shootout_goals,
	(ISNULL(ga.team_1_penalties_score,0) + ISNULL(ga.team_2_penalties_score,0)) - pso.total_penalties AS total_penalty_shootout_table_goals
FROM #tmp_games_for_assigning ga
JOIN #tmp_goals g ON ga.game_code = g.game_code
JOIN #tmp_penalty_shootout_goals pso ON pso.game_code = g.game_code
--WHERE ga.game_code = 65
/*GROUP BY ga.game_code, ga.game_number, ga.game_time, ga.team_1_name, ga.team_2_name,
	CONVERT(VARCHAR(3),ga.team_1_normal_time_score+ISNULL(ga.team_1_extra_time_score,'')) + ' - ' + 
	CONVERT(VARCHAR(3),ga.team_2_normal_time_score+ISNULL(ga.team_2_extra_time_score,'')) + 
	case when not ga.team_1_extra_time_score is NULL then case when ga.team_1_penalties_score is NULL then ' (et)' else '' end else '' end + 
	case when not ga.team_1_penalties_score is NULL then ' (' + CONVERT(VARCHAR(3),ga.team_1_penalties_score) + ' - ' + CONVERT(VARCHAR(3),ga.team_2_penalties_score) + ' pens)' else '' end,
	ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
	ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0),
	(ISNULL(ga.team_1_normal_time_score,0)+ISNULL(ga.team_1_extra_time_score,0)+
	ISNULL(ga.team_2_normal_time_score,0)+ISNULL(ga.team_2_extra_time_score,0)),
	ISNULL(ga.team_1_penalties_score,0) + ISNULL(ga.team_2_penalties_score,0)*/

DROP TABLE #tmp_games_for_assigning
DROP TABLE #tmp_penalty_shootout_goals
DROP TABLE #tmp_goals