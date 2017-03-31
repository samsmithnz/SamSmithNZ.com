CREATE PROCEDURE [dbo].[spWC_GetStats_TournamentStatistics]
	@tournament_code smallint
AS

SELECT count(game_code) as number_of_games,
isnull(sum(isnull(team_1_normal_time_score,0)) + sum(isnull(team_1_extra_time_score,0)) + 
sum(isnull(team_2_normal_time_score,0)) + sum(isnull(team_2_extra_time_score,0)),0) as goals_scored,
isnull(CONVERT(decimal(8,2),(sum(isnull(team_1_normal_time_score,0)) + sum(isnull(team_1_extra_time_score,0)) + 
sum(isnull(team_2_normal_time_score,0)) + sum(isnull(team_2_extra_time_score,0)))) 
	/ CONVERT(decimal(8,2),count(game_code)),0) as average_goals_per_game
FROM wc_game g
WHERE g.tournament_code = @tournament_code
and not team_1_normal_time_score is null 
and not team_2_normal_time_score is null