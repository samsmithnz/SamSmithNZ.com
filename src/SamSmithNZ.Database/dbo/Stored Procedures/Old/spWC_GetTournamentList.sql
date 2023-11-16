CREATE PROCEDURE [dbo].[spWC_GetTournamentList] 
	@get_all_tournaments bit = NULL
AS
SELECT t.competition_code, t.tournament_code, [year] AS tournament_year , [name] AS tournament_name, 
	ISNULL(t.host_team_code,0) AS host_team_code, ISNULL(te.flag_name,'') AS host_flag_name, 
	ISNULL(t.co_host_team_code,0) AS co_host_team_code, ISNULL(te2.flag_name,'') AS co_host_flag_name,
	COUNT(g.game_code) AS game_count,
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
	((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)) AS percent_complete,
	tcs.team_percent, tcs.game_percent, tcs.player_percent, tcs.goals_percent, tcs.penalty_shootout_goals_percent
--+ (tcs.cards_percent * 0.2))
FROM wc_tournament t
LEFT JOIN wc_team te ON te.team_code = t.host_team_code
LEFT JOIN wc_team te2 ON te2.team_code = t.co_host_team_code
LEFT JOIN wc_game g ON t.tournament_code = g.tournament_code
JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
JOIN vWC_TournamentCompletionStatistics tcs ON t.tournament_code = tcs.tournament_code 
--WHERE (competition_code = 1 and @get_all_tournaments is NULL) or (not @get_all_tournaments is NULL)
GROUP BY t.competition_code, t.tournament_code, [year] , [name], 
	ISNULL(t.host_team_code,0), ISNULL(te.flag_name,''), 
	ISNULL(t.co_host_team_code,0), ISNULL(te2.flag_name,''),
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