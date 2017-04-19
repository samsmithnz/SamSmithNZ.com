CREATE PROCEDURE [dbo].[spIFB_GetGameListForTeam]
	@team_code smallint = null
AS
	--From [spWC_GetGameList]
	
	SELECT g.round_number, g.round_code, r.round_name, g.game_code, 
		g.game_number, g.game_time, 
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
	--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
	WHERE (g.team_1_code = @team_code
	or g.team_2_code = @team_code)
	and te.team_code = @team_code
	ORDER BY g.game_time DESC, g.game_number--, gl.goal_time, isnull(gl.injury_time,0)