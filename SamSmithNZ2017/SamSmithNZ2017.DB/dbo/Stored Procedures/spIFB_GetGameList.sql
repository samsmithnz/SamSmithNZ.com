CREATE PROCEDURE [dbo].[spIFB_GetGameList]
	@tournament_code INT = NULL,
	@round_number INT = NULL,
	@round_code VARCHAR(10) = NULL
AS
SELECT g.round_number, g.round_code, r.round_name, g.game_code,
	g.game_number, g.game_time, 
	t1.team_code AS team_1_code, t1.team_name AS team_1_name, 
	ISNULL(g.team_1_normal_time_score,-1) AS team_1_normal_time_score, 
	ISNULL(g.team_1_extra_time_score,-1) AS team_1_extra_time_score, 
	ISNULL(g.team_1_penalties_score,-1) AS team_1_penalties_score,
	t2.team_code AS team_2_code, t2.team_name AS team_2_name, 
	ISNULL(g.team_2_normal_time_score,-1) AS team_2_normal_time_score, 
	ISNULL(g.team_2_extra_time_score,-1) AS team_2_extra_time_score, 
	ISNULL(g.team_2_penalties_score,-1) AS team_2_penalties_score,
	g.round_code, g.location, 
	t1.flag_name AS team_1_flag_name, 
	t2.flag_name AS team_2_flag_name,
	CONVERT(bit,CASE WHEN g.team_1_normal_time_score is NULL THEN
		1 ELSE 0 END) AS team_1_withdrew,
	CONVERT(bit,CASE WHEN g.team_2_normal_time_score is NULL THEN
		1 ELSE 0 END) AS team_2_withdrew
FROM wc_game g
JOIN wc_team t1 ON g.team_1_code = t1.team_code
JOIN wc_team t2 ON g.team_2_code = t2.team_code
LEFT JOIN wc_round r ON g.round_code = r.round_code
WHERE (@tournament_code is NULL or g.tournament_code = @tournament_code)
and (@round_number is NULL or g.round_number = @round_number)
and (@round_code is NULL or g.round_code = @round_code)
--and year(g.game_time) >= 1994 
ORDER BY g.game_time, g.game_number