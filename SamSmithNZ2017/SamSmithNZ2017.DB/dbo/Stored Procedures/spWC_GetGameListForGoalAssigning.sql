CREATE PROCEDURE [dbo].[spWC_GetGameListForGoalAssigning] 
	@tournament_code smallint
AS
SET NOCOUNT ON

/*
SELECT CONVERT(smallint,null) as game_code,  
	CONVERT(smallint,null) as game_number, 
	CONVERT(datetime,null) as game_time, 
	CONVERT(varchar(100),null) as team_1_name, 
	CONVERT(varchar(100),null) as team_2_name,
	CONVERT(varchar(100),null) as score, 
	CONVERT(smallint,null) as total_game_table_goals, 
	CONVERT(smallint,null) as total_goal_table_goals,
	CONVERT(smallint,null) as total_game_table_penalty_shootout_goals,
	CONVERT(smallint,null) as total_penalty_shootout_table_goals
*/

CREATE TABLE #tmp_games_for_assigning (row_type smallint, round_number smallint, 
	round_code varchar(10), round_name varchar(50), 
	game_code smallint, game_number smallint, game_time datetime,
	team_1_code smallint, team_1_name varchar(200), 
	team_1_normal_time_score smallint, team_1_extra_time_score smallint, team_1_penalties_score smallint,
	team_2_code smallint, team_2_name varchar(200),
	team_2_normal_time_score smallint, team_2_extra_time_score smallint, team_2_penalties_score smallint,
	team_1_flag_name varchar(100), team_2_flag_name varchar(100),
	location varchar(100), team_1_withdrew bit, team_2_withdrew bit, 
	tournament_code smallint, tournament_name varchar(50),
	coach_name varchar(50), coach_flag varchar(100), fifa_ranking smallint, ispen bit, isog bit)

INSERT INTO #tmp_games_for_assigning
exec [spWC_GetGameList] @tournament_code,0,'',0,0

CREATE TABLE #tmp_goals (game_code smallint, total_goals smallint)

INSERT INTO #tmp_goals
SELECT ga.game_code, isnull(count(gl.goal_code),0)
FROM #tmp_games_for_assigning ga
LEFT OUTER JOIN wc_goal gl ON ga.game_code = gl.game_code
GROUP BY ga.game_code

CREATE TABLE #tmp_penalty_shootout_goals (game_code smallint, total_penalties smallint)

INSERT INTO #tmp_penalty_shootout_goals
SELECT ga.game_code, isnull(count(pso.penalty_code),0)
FROM #tmp_games_for_assigning ga
LEFT OUTER JOIN wc_penalty_shootout pso ON ga.game_code = pso.game_code and pso.scored = 1
GROUP BY ga.game_code

SELECT ga.game_code, ga.game_number, ga.game_time, ga.team_1_name, ga.team_2_name,
	CONVERT(varchar(3),ga.team_1_normal_time_score+isnull(ga.team_1_extra_time_score,'')) + ' - ' + 
	CONVERT(varchar(3),ga.team_2_normal_time_score+isnull(ga.team_2_extra_time_score,'')) + 
	case when not ga.team_1_extra_time_score is null then case when ga.team_1_penalties_score is null then ' (et)' else '' end else '' end + 
	case when not ga.team_1_penalties_score is null then ' (' + CONVERT(varchar(3),ga.team_1_penalties_score) + ' - ' + CONVERT(varchar(3),ga.team_2_penalties_score) + ' pens)' else '' end as score,
	isnull(ga.team_1_normal_time_score,0)+isnull(ga.team_1_extra_time_score,0)+
	isnull(ga.team_2_normal_time_score,0)+isnull(ga.team_2_extra_time_score,0) as total_game_table_goals,
	(isnull(ga.team_1_normal_time_score,0)+isnull(ga.team_1_extra_time_score,0)+
	isnull(ga.team_2_normal_time_score,0)+isnull(ga.team_2_extra_time_score,0)) - g.total_goals as total_goal_table_goals,
	(isnull(ga.team_1_penalties_score,0) + isnull(ga.team_2_penalties_score,0)) as total_game_table_penalty_shootout_goals,
	(isnull(ga.team_1_penalties_score,0) + isnull(ga.team_2_penalties_score,0)) - pso.total_penalties as total_penalty_shootout_table_goals
FROM #tmp_games_for_assigning ga
JOIN #tmp_goals g ON ga.game_code = g.game_code
JOIN #tmp_penalty_shootout_goals pso ON pso.game_code = g.game_code
--WHERE ga.game_code = 65
/*GROUP BY ga.game_code, ga.game_number, ga.game_time, ga.team_1_name, ga.team_2_name,
	CONVERT(varchar(3),ga.team_1_normal_time_score+isnull(ga.team_1_extra_time_score,'')) + ' - ' + 
	CONVERT(varchar(3),ga.team_2_normal_time_score+isnull(ga.team_2_extra_time_score,'')) + 
	case when not ga.team_1_extra_time_score is null then case when ga.team_1_penalties_score is null then ' (et)' else '' end else '' end + 
	case when not ga.team_1_penalties_score is null then ' (' + CONVERT(varchar(3),ga.team_1_penalties_score) + ' - ' + CONVERT(varchar(3),ga.team_2_penalties_score) + ' pens)' else '' end,
	isnull(ga.team_1_normal_time_score,0)+isnull(ga.team_1_extra_time_score,0)+
	isnull(ga.team_2_normal_time_score,0)+isnull(ga.team_2_extra_time_score,0),
	(isnull(ga.team_1_normal_time_score,0)+isnull(ga.team_1_extra_time_score,0)+
	isnull(ga.team_2_normal_time_score,0)+isnull(ga.team_2_extra_time_score,0)),
	isnull(ga.team_1_penalties_score,0) + isnull(ga.team_2_penalties_score,0)*/

DROP TABLE #tmp_games_for_assigning
DROP TABLE #tmp_penalty_shootout_goals
DROP TABLE #tmp_goals