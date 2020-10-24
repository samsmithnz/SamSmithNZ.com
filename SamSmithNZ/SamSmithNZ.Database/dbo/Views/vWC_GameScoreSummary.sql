CREATE VIEW [dbo].[vWC_GameScoreSummary]
AS

SELECT tournament_code, game_code, team_1_code AS team_code, 1 AS is_team_1, 
	team_1_normal_time_score + ISNULL(team_1_extra_time_score,0) AS goals, 
	team_1_normal_time_score + ISNULL(team_1_extra_time_score,0) + ISNULL(team_1_penalties_score,0) AS goals_with_penalties
FROM wc_game
--where team_1_code = 12
--GROUP BY tournament_code, team_1_code

UNION

SELECT tournament_code, game_code, team_2_code AS team_code, 0 AS is_team_2,
	team_2_normal_time_score + ISNULL(team_2_extra_time_score,0) AS goals,
	team_2_normal_time_score + ISNULL(team_2_extra_time_score,0) + ISNULL(team_2_penalties_score,0) AS goals_with_penalties
FROM wc_game
--where team_2_code = 12
--GROUP BY tournament_code, team_2_code
