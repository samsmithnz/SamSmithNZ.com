CREATE PROCEDURE [dbo].[spIFB_GetGameListForTeam]
	@team_code INT = NULL
AS
	--From [spWC_GetGameList]
	
	SELECT g.round_number, g.round_code, r.round_name, g.game_code, 
		g.game_number, g.game_time, 
		t1.team_code AS team_1_code, t1.team_name AS team_1_name, g.team_1_normal_time_score, g.team_1_extra_time_score, g.team_1_penalties_score,
		t2.team_code AS team_2_code, t2.team_name AS team_2_name, g.team_2_normal_time_score, g.team_2_extra_time_score, g.team_2_penalties_score, 
		t1.flag_name AS team_1_flag_name, t2.flag_name AS team_2_flag_name,
		g.location, 
		CONVERT(bit,CASE WHEN g.team_1_normal_time_score is NULL THEN
			1 ELSE 0 END) AS team_1_withdrew,
		CONVERT(bit,CASE WHEN g.team_2_normal_time_score is NULL THEN
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
	--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE (g.team_1_code = @team_code
	or g.team_2_code = @team_code)
	and te.team_code = @team_code
	ORDER BY g.game_time DESC, g.game_number--, gl.goal_time, ISNULL(gl.injury_time,0)
