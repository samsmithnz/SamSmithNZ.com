CREATE PROCEDURE [dbo].[spWC_GetTournamentList] 
	@get_all_tournaments bit = null
AS
SELECT t.competition_code, t.tournament_code, [year] as tournament_year , [name] as tournament_name, 
	isnull(t.host_team_code,0) as host_team_code, isnull(te.flag_name,'') as host_flag_name, 
	isnull(t.co_host_team_code,0) as co_host_team_code, isnull(te2.flag_name,'') as co_host_flag_name,
	count(g.game_code) as game_count,
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
	((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)) as percent_complete,
	tcs.team_percent, tcs.game_percent, tcs.player_percent, tcs.goals_percent, tcs.penalty_shootout_goals_percent
--+ (tcs.cards_percent * 0.2))
FROM wc_tournament t
LEFT OUTER JOIN wc_team te ON te.team_code = t.host_team_code
LEFT OUTER JOIN wc_team te2 ON te2.team_code = t.co_host_team_code
LEFT OUTER JOIN wc_game g ON t.tournament_code = g.tournament_code
JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
JOIN vWC_TournamentCompletionStatistics tcs ON t.tournament_code = tcs.tournament_code 
--WHERE (competition_code = 1 and @get_all_tournaments is null) or (not @get_all_tournaments is null)
GROUP BY t.competition_code, t.tournament_code, [year] , [name], 
	isnull(t.host_team_code,0), isnull(te.flag_name,''), 
	isnull(t.co_host_team_code,0), isnull(te2.flag_name,''),
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