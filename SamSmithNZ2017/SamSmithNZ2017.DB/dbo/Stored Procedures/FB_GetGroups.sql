CREATE PROCEDURE [dbo].[FB_GetGroups]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10)
AS
BEGIN
	SELECT t.team_name AS TeamName, 
		CONVERT(VARCHAR(100),t.flag_name) AS TeamFlagName, 
		gs.draws AS Draws, 
		gs.goal_difference AS GoalDifference, 
		gs.goals_against AS GoalsAgainst, 
		gs.goals_for AS GoalsFor, 
		gs.group_ranking AS GroupRanking, 
		gs.has_qualified_for_next_round AS HasQualifiedForNextRound, 
		gs.losses AS Losses,
		gs.played AS Played, 
		gs.points AS Points, 
		gs.round_code AS RoundCode, 
		gs.round_number AS RoundNumber, 
		gs.team_code AS TeamCode, 
		gs.tournament_code AS TournamentCode, 
		gs.wins AS Wins,
		e.elo_rating AS ELORating
	FROM wc_group_stage gs
	JOIN wc_team t ON gs.team_code = t.team_code
	JOIN wc_tournament_team_elo_rating e ON gs.tournament_code = e.tournament_code AND gs.team_code = e.team_code
	WHERE gs.tournament_code = @TournamentCode
	AND gs.round_number = @RoundNumber
	AND gs.round_code = @RoundCode
	ORDER BY gs.group_ranking
END

