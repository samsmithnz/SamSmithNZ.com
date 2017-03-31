CREATE PROCEDURE [dbo].[spWC_GetTeamGameList]
	@team_code smallint
AS
SELECT g.game_number, g.game_time, 
	t1.team_code as team_1_code, t1.team_name as team_1_name, 
	g.team_1_normal_time_score, g.team_1_extra_time_score, g.team_1_penalties_score,
	t2.team_code as team_2_code, t2.team_name as team_2_name, 
	g.team_2_normal_time_score, g.team_2_extra_time_score, g.team_2_penalties_score,
	g.round_code, r.round_name, g.location, 
	t1.flag_name as team_1_flag_name, 
	t2.flag_name as team_2_flag_name, 
	t.[name] as tournament_name, t.tournament_code,
	isnull(te.coach_name,'') as coach_name, isnull(t3.flag_name,'') as coach_flag,
	isnull(te.fifa_ranking,0) as fifa_ranking
FROM wc_game g
INNER JOIN wc_team t1 ON g.team_1_code = t1.team_code
INNER JOIN wc_team t2 ON g.team_2_code = t2.team_code
INNER JOIN wc_tournament t ON g.tournament_code = t.tournament_code
INNER JOIN wc_round r ON r.round_code = g.round_code
LEFT OUTER JOIN wc_tournament_team_entry te ON t.tournament_code = te.tournament_code 
LEFT OUTER JOIN wc_team t3 ON te.coach_nationality = t3.team_name
WHERE (g.team_1_code = @team_code or g.team_2_code = @team_code)
and te.team_code = @team_code
ORDER BY g.game_time DESC, g.game_number