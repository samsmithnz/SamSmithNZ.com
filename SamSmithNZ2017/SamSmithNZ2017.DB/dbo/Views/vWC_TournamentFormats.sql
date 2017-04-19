﻿CREATE VIEW [dbo].[vWC_TournamentFormats]
AS
SELECT tf.format_code,
tfr1.format_round_code as r1_format_round_code, 
tfr1.is_group_stage as r1_is_group_stage, 
tfr1.number_of_teams_in_group as r1_number_of_teams_in_group, 
tfr1.number_of_groups_in_round as r1_number_of_groups_in_round, 
tfr1.number_of_teams_from_group_that_advance as r1_number_of_teams_from_group_that_advance, 
tfr1.total_number_of_teams_that_advance as r1_total_number_of_teams_that_advance,
tfr2.format_round_code as r2_format_round_code, 
tfr2.is_group_stage as r2_is_group_stage, 
tfr2.number_of_teams_in_group as r2_number_of_teams_in_group, 
tfr2.number_of_groups_in_round as r2_number_of_groups_in_round, 
tfr2.number_of_teams_from_group_that_advance as r2_number_of_teams_from_group_that_advance, 
tfr2.total_number_of_teams_that_advance as r2_total_number_of_teams_that_advance,
tfr3.format_round_code as r3_format_round_code, 
tfr3.is_group_stage as r3_is_group_stage, 
tfr3.number_of_teams_in_group as r3_number_of_teams_in_group, 
tfr3.number_of_groups_in_round as r3_number_of_groups_in_round, 
tfr3.number_of_teams_from_group_that_advance as r3_number_of_teams_from_group_that_advance, 
tfr3.total_number_of_teams_that_advance as r3_total_number_of_teams_that_advance
from wc_tournament_format tf
JOIN wc_tournament_format_round tfr1 ON tf.round_1_format_code = tfr1.format_round_code
JOIN wc_tournament_format_round tfr2 ON tf.round_2_format_code = tfr2.format_round_code
JOIN wc_tournament_format_round tfr3 ON tf.round_3_format_code = tfr3.format_round_code
