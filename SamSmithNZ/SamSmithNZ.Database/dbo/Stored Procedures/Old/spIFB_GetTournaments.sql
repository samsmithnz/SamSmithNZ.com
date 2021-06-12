CREATE PROCEDURE [dbo].[spIFB_GetTournaments] 
	@competition_code INT = NULL,
	@tournament_code INT = NULL
AS

SELECT t.competition_code, t.tournament_code, [year] AS tournament_year , [name] AS tournament_name, 
	ISNULL(t.host_team_code,0) AS host_team_code, te.team_name AS host_team_name, ISNULL(te.flag_name,'') AS host_flag_name, 
	ISNULL(t.co_host_team_code,0) AS co_host_team_code, te2.team_name AS co_host_team_name, ISNULL(te2.flag_name,'') AS co_host_flag_name,
	CONVERT(VARCHAR(8000),t.notes) AS notes, t.logo_image, t.qualification_image,
	ISNULL(COUNT(g.game_code),0) AS game_count,
	ISNULL((SELECT MIN(ga1.game_time) FROM wc_game ga1 WHERE ga1.tournament_code = t.tournament_code),NULL) AS min_game_time,
	ISNULL((SELECT MAX(ga2.game_time) FROM wc_game ga2 WHERE ga2.tournament_code = t.tournament_code),NULL) AS max_game_time,
	tf.format_code,
	r1_format_round_code, 
	r1_is_group_stage, 
	r1_number_of_teams_in_group, 
	r1_number_of_groups_in_round, 
	r1_number_of_teams_from_group_that_advance, 
	r1_total_number_of_teams_that_advance,
	ISNULL((SELECT TOP 1 gr1.round_code FROM wc_group_stage gr1 WHERE gr1.tournament_code = t.tournament_code and gr1.round_number = 1 ORDER BY gr1.round_code),'') AS r1_first_group_code,
	r2_format_round_code, 
	r2_is_group_stage, 
	r2_number_of_teams_in_group, 
	r2_number_of_groups_in_round, 
	r2_number_of_teams_from_group_that_advance, 
	r2_total_number_of_teams_that_advance,
	ISNULL((SELECT TOP 1 gr2.round_code FROM wc_group_stage gr2 WHERE gr2.tournament_code = t.tournament_code and gr2.round_number = 2 ORDER BY gr2.round_code),'') AS r2_first_group_code,
	r3_format_round_code, 
	r3_is_group_stage, 
	r3_number_of_teams_in_group, 
	r3_number_of_groups_in_round, 
	r3_number_of_teams_from_group_that_advance, 
	r3_total_number_of_teams_that_advance,
	ISNULL((SELECT TOP 1 gr3.round_code FROM wc_group_stage gr3 WHERE gr3.tournament_code = t.tournament_code and gr3.round_number = 3 ORDER BY gr3.round_code),'') AS r3_first_group_code,
	((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)) AS percent_complete,
	tcs.team_percent, tcs.game_percent, tcs.player_percent, tcs.goals_percent, tcs.penalty_shootout_goals_percent
--+ (tcs.cards_percent * 0.2))
FROM wc_tournament t
LEFT JOIN wc_team te ON te.team_code = t.host_team_code
LEFT JOIN wc_team te2 ON te2.team_code = t.co_host_team_code
LEFT JOIN wc_game g ON t.tournament_code = g.tournament_code
JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
JOIN vWC_TournamentCompletionStatistics tcs ON t.tournament_code = tcs.tournament_code 
WHERE ((t.competition_code = @competition_code) or (@competition_code is NULL))
and ((t.tournament_code = @tournament_code) or (@tournament_code is NULL))
GROUP BY t.competition_code, t.tournament_code, [year] , [name], 
	ISNULL(t.host_team_code,0), te.team_name, ISNULL(te.flag_name,''), 
	ISNULL(t.co_host_team_code,0), te2.team_name, ISNULL(te2.flag_name,''),
	CONVERT(VARCHAR(8000),t.notes), t.logo_image, t.qualification_image,
	tf.format_code,
	r1_format_round_code, 
	r1_is_group_stage, 
	r1_number_of_teams_in_group, 
	r1_number_of_groups_in_round, 
	r1_number_of_teams_from_group_that_advance, 
	r1_total_number_of_teams_that_advance,
	r2_format_round_code, 
	r2_is_group_stage, 
	r2_number_of_teams_in_group, 
	r2_number_of_groups_in_round, 
	r2_number_of_teams_from_group_that_advance, 
	r2_total_number_of_teams_that_advance,
	r3_format_round_code, 
	r3_is_group_stage, 
	r3_number_of_teams_in_group, 
	r3_number_of_groups_in_round, 
	r3_number_of_teams_from_group_that_advance, 
	r3_total_number_of_teams_that_advance,
	((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)),
	tcs.team_percent, tcs.game_percent, tcs.player_percent, tcs.goals_percent, tcs.penalty_shootout_goals_percent
	--+ (tcs.cards_percent * 0.2))
ORDER BY [year] DESC