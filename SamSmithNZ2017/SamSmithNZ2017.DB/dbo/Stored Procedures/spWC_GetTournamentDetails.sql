CREATE PROCEDURE [dbo].[spWC_GetTournamentDetails]
	@tournament_code smallint
AS

SELECT t.tournament_code, t.co_host_team_code, t.competition_code, t.format_code, t.host_team_code, t.logo_image, t.[name], t.notes, t.qualification_image, t.[year], 
	tf.format_code, 
	tf.r1_format_round_code, tf.r1_is_group_stage, tf.r1_number_of_groups_in_round, tf.r1_number_of_teams_from_group_that_advance, tf.r1_number_of_teams_in_group, tf.r1_total_number_of_teams_that_advance,
	tf.r2_format_round_code, tf.r2_is_group_stage, tf.r2_number_of_groups_in_round, tf.r2_number_of_teams_from_group_that_advance, tf.r2_number_of_teams_in_group, tf.r2_total_number_of_teams_that_advance,
	tf.r3_format_round_code, tf.r3_is_group_stage, tf.r3_number_of_groups_in_round, tf.r3_number_of_teams_from_group_that_advance, tf.r3_number_of_teams_in_group, tf.r3_total_number_of_teams_that_advance,
	te.team_name, te.flag_name, 
	te2.team_name as co_host_team_name, te2.flag_name as co_host_flag_name,
isnull((SELECT min(game_time) FROM wc_game WHERE tournament_code = @tournament_code),'1-1-1900') as min_game_time,
isnull((SELECT max(game_time) FROM wc_game WHERE tournament_code = @tournament_code),'1-1-1900') as max_game_time,
r1_format_round_code, 
r1_is_group_stage, 
isnull((SELECT TOP 1 round_code FROM wc_group_stage
WHERE tournament_code = @tournament_code and round_number = 1 ORDER BY round_code),'') AS r1_first_group_code,
r1_number_of_teams_in_group, 
r1_number_of_groups_in_round, 
r1_number_of_teams_from_group_that_advance, 
r1_total_number_of_teams_that_advance,
r2_format_round_code, 
r2_is_group_stage, 
isnull((SELECT TOP 1 round_code FROM wc_group_stage
WHERE tournament_code = @tournament_code and round_number = 2 ORDER BY round_code),'') AS r2_first_group_code,
r2_number_of_teams_in_group, 
r2_number_of_groups_in_round, 
r2_number_of_teams_from_group_that_advance, 
r2_total_number_of_teams_that_advance,
r3_format_round_code, 
r3_is_group_stage, 
isnull((SELECT TOP 1 round_code FROM wc_group_stage
WHERE tournament_code = @tournament_code and round_number = 3 ORDER BY round_code),'') AS r3_first_group_code,
r3_number_of_teams_in_group, 
r3_number_of_groups_in_round, 
r3_number_of_teams_from_group_that_advance, 
r3_total_number_of_teams_that_advance
FROM wc_tournament t
JOIN vWC_TournamentFormats tf ON t.format_code = tf.format_code
JOIN wc_team te ON t.host_team_code = te.team_code
LEFT OUTER JOIN wc_team te2 ON t.co_host_team_code = te2.team_code
WHERE t.tournament_code = @tournament_code