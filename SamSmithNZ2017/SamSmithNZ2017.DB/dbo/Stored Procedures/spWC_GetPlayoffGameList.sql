CREATE PROCEDURE [dbo].[spWC_GetPlayoffGameList]
	@tournament_code smallint,
	@round_number smallint
AS
SELECT g.game_number, g.game_time, 
	t1.team_code as team_1_code, t1.team_name as team_1_name, 
	isnull(g.team_1_normal_time_score,0) as team_1_normal_time_score, 
	g.team_1_extra_time_score, g.team_1_penalties_score,
	t2.team_code as team_2_code, t2.team_name as team_2_name, 
	isnull(g.team_2_normal_time_score,0) as team_2_normal_time_score, 
	g.team_2_extra_time_score, g.team_2_penalties_score,
	g.round_code, g.location, 
	t1.flag_name as team_1_flag_name, 
	t2.flag_name as team_2_flag_name,
	CONVERT(bit,CASE WHEN g.team_1_normal_time_score is null THEN
		1 ELSE 0 END) as team_1_withdrew,
	CONVERT(bit,CASE WHEN g.team_2_normal_time_score is null THEN
		1 ELSE 0 END) as team_2_withdrew
FROM wc_game g
LEFT OUTER JOIN wc_team t1 ON g.team_1_code = t1.team_code
LEFT OUTER JOIN wc_team t2 ON g.team_2_code = t2.team_code
WHERE g.tournament_code = @tournament_code
and g.round_number = @round_number
and g.round_code in ('16','QF','SF','FF','3P')
ORDER BY g.game_time, g.game_number