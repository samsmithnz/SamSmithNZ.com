CREATE PROCEDURE [dbo].[spIFB_GetTournaments] 
	@competition_code smallint = null,
	@tournament_code smallint = null
AS

SELECT t.competition_code, t.tournament_code, [year] as tournament_year , [name] as tournament_name, 
	isnull(t.host_team_code,0) as host_team_code, te.team_name as host_team_name, isnull(te.flag_name,'') as host_flag_name, 
	isnull(t.co_host_team_code,0) as co_host_team_code, te2.team_name as co_host_team_name, isnull(te2.flag_name,'') as co_host_flag_name,
	CONVERT(Varchar(8000),t.notes) as notes, t.logo_image, t.qualification_image,
	isnull(count(g.game_code),0) as game_count,
	isnull((SELECT min(ga1.game_time) FROM wc_game ga1 WHERE ga1.tournament_code = t.tournament_code),null) as min_game_time,
	isnull((SELECT max(ga2.game_time) FROM wc_game ga2 WHERE ga2.tournament_code = t.tournament_code),null) as max_game_time,
	tf.format_code,
	r1_format_round_code, 
	r1_is_group_stage, 
	r1_number_of_teams_in_group, 
	r1_number_of_groups_in_round, 
	r1_number_of_teams_from_group_that_advance, 
	r1_total_number_of_teams_that_advance,
	isnull((SELECT TOP 1 gr1.round_code FROM wc_group_stage gr1 WHERE gr1.tournament_code = t.tournament_code and gr1.round_number = 1 ORDER BY gr1.round_code),'') AS r1_first_group_code,
	r2_format_round_code, 
	r2_is_group_stage, 
	r2_number_of_teams_in_group, 
	r2_number_of_groups_in_round, 
	r2_number_of_teams_from_group_that_advance, 
	r2_total_number_of_teams_that_advance,
	isnull((SELECT TOP 1 gr2.round_code FROM wc_group_stage gr2 WHERE gr2.tournament_code = t.tournament_code and gr2.round_number = 2 ORDER BY gr2.round_code),'') AS r2_first_group_code,
	r3_format_round_code, 
	r3_is_group_stage, 
	r3_number_of_teams_in_group, 
	r3_number_of_groups_in_round, 
	r3_number_of_teams_from_group_that_advance, 
	r3_total_number_of_teams_that_advance,
	isnull((SELECT TOP 1 gr3.round_code FROM wc_group_stage gr3 WHERE gr3.tournament_code = t.tournament_code and gr3.round_number = 3 ORDER BY gr3.round_code),'') AS r3_first_group_code,
	((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)) as percent_complete,
	tcs.team_percent, tcs.game_percent, tcs.player_percent, tcs.goals_percent, tcs.penalty_shootout_goals_percent
--+ (tcs.cards_percent * 0.2))
FROM wc_tournament t
LEFT OUTER JOIN wc_team te ON te.team_code = t.host_team_code
LEFT OUTER JOIN wc_team te2 ON te2.team_code = t.co_host_team_code
LEFT OUTER JOIN wc_game g ON t.tournament_code = g.tournament_code
INNER JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
INNER JOIN vWC_TournamentCompletionStatistics tcs ON t.tournament_code = tcs.tournament_code 
WHERE ((t.competition_code = @competition_code) or (@competition_code is null))
and ((t.tournament_code = @tournament_code) or (@tournament_code is null))
GROUP BY t.competition_code, t.tournament_code, [year] , [name], 
	isnull(t.host_team_code,0), te.team_name, isnull(te.flag_name,''), 
	isnull(t.co_host_team_code,0), te2.team_name, isnull(te2.flag_name,''),
	CONVERT(Varchar(8000),t.notes), t.logo_image, t.qualification_image,
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