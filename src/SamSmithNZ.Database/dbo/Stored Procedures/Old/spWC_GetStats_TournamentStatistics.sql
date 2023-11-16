CREATE PROCEDURE [dbo].[spWC_GetStats_TournamentStatistics]
	@tournament_code INT
AS

SELECT COUNT(game_code) AS number_of_games,
ISNULL(sum(ISNULL(team_1_normal_time_score,0)) + sum(ISNULL(team_1_extra_time_score,0)) + 
sum(ISNULL(team_2_normal_time_score,0)) + sum(ISNULL(team_2_extra_time_score,0)),0) AS goals_scored,
ISNULL(CONVERT(decimal(8,2),(sum(ISNULL(team_1_normal_time_score,0)) + sum(ISNULL(team_1_extra_time_score,0)) + 
sum(ISNULL(team_2_normal_time_score,0)) + sum(ISNULL(team_2_extra_time_score,0)))) 
	/ CONVERT(decimal(8,2),COUNT(game_code)),0) AS average_goals_per_game
FROM wc_game g
WHERE g.tournament_code = @tournament_code
and not team_1_normal_time_score is NULL 
and not team_2_normal_time_score is NULL
