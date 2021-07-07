CREATE VIEW [dbo].[vWC_GameScoreSummary]
AS

SELECT tournament_code, game_code, 
	team_1_code, 
	ISNULL(team_1_normal_time_score,0) + ISNULL(team_1_extra_time_score,0) + ISNULL(team_1_penalties_score,0) AS team_1_goals,
	team_2_code,
	ISNULL(team_2_normal_time_score,0) + ISNULL(team_2_extra_time_score,0) + ISNULL(team_2_penalties_score,0) AS team_2_goals
FROM wc_game

