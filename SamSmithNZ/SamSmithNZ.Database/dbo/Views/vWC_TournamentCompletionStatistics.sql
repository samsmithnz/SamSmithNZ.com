CREATE VIEW [dbo].[vWC_TournamentCompletionStatistics]
AS
SELECT DISTINCT t.tournament_code, 
	CONVERT(decimal(6,2),qt.qualified_teams_count/tf.total_number_of_teams) AS team_percent, 
	CONVERT(decimal(6,2),CASE WHEN g.game_count > 0 THEN 1 ELSE 0 END) AS game_percent, 
	CONVERT(decimal(6,2),p.player_count / (CASE WHEN t.tournament_code = 9 THEN 349 
											WHEN t.tournament_code = 5 THEN 350 --South Korea missing 2 players
											WHEN t.tournament_code = 4 THEN 282 --England missing 1, US missing 2 players
											WHEN t.tournament_code = 3 THEN 298 --Lots of players in lots of different teams
											WHEN t.tournament_code = 2 THEN 333 --Lots of players in lots of different teams
											WHEN t.tournament_code = 1 THEN 241 --Lots of players in lots of different teams
											ELSE tf.total_number_of_teams * CASE WHEN t.tournament_code > 16 AND t.competition_code = 1 THEN 23 
																				 WHEN t.tournament_code = 12 AND t.competition_code = 1 THEN 20 																		 
																				 ELSE 22 END 
											END)) AS player_percent,
	CASE WHEN tg.total_goals = 0 THEN 0 ELSE CONVERT(decimal(6,2),pg.player_goal_count) / CONVERT(decimal(6,2),tg.total_goals) END AS goals_percent,
	CASE WHEN tpsog.total_penalty_shootout_goals = 0 THEN 1 ELSE CONVERT(decimal(6,2),psog.player_penalty_shootout_goal_count) / CONVERT(decimal(6,2),tpsog.total_penalty_shootout_goals) END AS penalty_shootout_goals_percent,
	1 AS cards_percent--CASE WHEN pc.player_card_count > 1 THEN 1 ELSE 0 END AS cards_percent
FROM wc_tournament t
JOIN wc_tournament_format tf ON t.format_code = tf.format_code
JOIN vWC_QualifiedTeamCount qt ON t.tournament_code = qt.tournament_code
JOIN vWC_GameCount g ON t.tournament_code = g.tournament_code
JOIN vWC_PlayerCount p ON t.tournament_code = p.tournament_code
JOIN vWC_PlayerGoalCount pg ON t.tournament_code = pg.tournament_code
JOIN vWC_TournamentGoals tg ON t.tournament_code = tg.tournament_code
JOIN vWC_PlayerPenaltyShootoutCount psog ON t.tournament_code = psog.tournament_code
JOIN vWC_TournamentPenaltyShootoutGoals tpsog ON t.tournament_code = tpsog.tournament_code
--JOIN vWC_PlayerCardCount pc ON t.tournament_code = pc.tournament_code
--JOIN vWC_TournamentCards tc ON t.tournament_code = tc.tournament_code
GO