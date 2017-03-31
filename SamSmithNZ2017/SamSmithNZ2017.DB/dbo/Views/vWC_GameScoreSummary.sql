CREATE VIEW [dbo].[vWC_GameScoreSummary]
AS
SELECT tournament_code, game_code, team_1_code as team_code, 1 as is_team_1, 
isnull(team_1_normal_time_score,0) + isnull(team_1_extra_time_score,0) as goals, 
isnull(team_1_normal_time_score,0) + isnull(team_1_extra_time_score,0) + isnull(team_1_penalties_score,0) as goals_with_penalties
FROM wc_game
--where team_1_code = 12
--GROUP BY tournament_code, team_1_code

UNION

SELECT tournament_code, game_code, team_2_code as team_code, 0 as is_team_2,
isnull(team_2_normal_time_score,0) + isnull(team_2_extra_time_score,0) as goals,
isnull(team_2_normal_time_score,0) + isnull(team_2_extra_time_score,0) + isnull(team_2_penalties_score,0) as goals_with_penalties
FROM wc_game
--where team_2_code = 12
--GROUP BY tournament_code, team_2_code