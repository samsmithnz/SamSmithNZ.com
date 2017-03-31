CREATE VIEW [dbo].[vWC_TournamentCompletionStatistics]
AS
SELECT DISTINCT t.tournament_code, 
	CONVERT(decimal(6,2),qt.qualified_teams_count/tf.total_number_of_teams) as team_percent, 
	CONVERT(decimal(6,2),CASE WHEN g.game_count > 0 THEN 1 ELSE 0 END) as game_percent, 
	CONVERT(decimal(6,2),p.player_count / (tf.total_number_of_teams * CASE WHEN t.tournament_code > 16 THEN 23 
																		WHEN t.tournament_code = 12 THEN 20 
																		 
																		ELSE 22 END)) as player_percent,
	CASE WHEN tg.total_goals = 0 THEN 0 ELSE CONVERT(decimal(6,2),pg.player_goal_count) / CONVERT(decimal(6,2),tg.total_goals) END as goals_percent,
	CASE WHEN tpsog.total_penalty_shootout_goals = 0 THEN 0 ELSE CONVERT(decimal(6,2),psog.player_penalty_shootout_goal_count) / CONVERT(decimal(6,2),tpsog.total_penalty_shootout_goals) END as penalty_shootout_goals_percent,
	1 as cards_percent--CASE WHEN pc.player_card_count > 1 THEN 1 ELSE 0 END as cards_percent
FROM wc_tournament t
INNER JOIN wc_tournament_format tf ON t.format_code = tf.format_code
INNER JOIN vWC_QualifiedTeamCount qt ON t.tournament_code = qt.tournament_code
INNER JOIN vWC_GameCount g ON t.tournament_code = g.tournament_code
INNER JOIN vWC_PlayerCount p ON t.tournament_code = p.tournament_code
INNER JOIN vWC_PlayerGoalCount pg ON t.tournament_code = pg.tournament_code
INNER JOIN vWC_TournamentGoals tg ON t.tournament_code = tg.tournament_code
INNER JOIN vWC_PlayerPenaltyShootoutCount psog ON t.tournament_code = psog.tournament_code
INNER JOIN vWC_TournamentPenaltyShootoutGoals tpsog ON t.tournament_code = tpsog.tournament_code
--INNER JOIN vWC_PlayerCardCount pc ON t.tournament_code = pc.tournament_code
--INNER JOIN vWC_TournamentCards tc ON t.tournament_code = tc.tournament_code