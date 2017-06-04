CREATE PROCEDURE [dbo].[spIFB_GetGroupDetails]
	@tournament_code smallint,
	@round_number smallint,
	@round_code varchar(10)
AS
SELECT t.team_name, 
	CONVERT(varchar(100),t.flag_name) as team_flag_name, 
	gs.draws, gs.goal_difference, gs.goals_against, gs.goals_for, gs.group_ranking, gs.has_qualified_for_next_round, gs.losses,
	gs.played, gs.points, gs.round_code, gs.round_number, gs.team_code, gs.tournament_code, gs.wins
FROM wc_group_stage gs
JOIN wc_team t ON gs.team_code = t.team_code
WHERE tournament_code = @tournament_code
and round_number = @round_number
and round_code = @round_code
ORDER BY group_ranking