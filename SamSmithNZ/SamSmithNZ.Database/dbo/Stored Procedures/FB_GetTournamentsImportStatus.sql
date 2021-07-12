CREATE PROCEDURE [dbo].[FB_GetTournamentsImportStatus]
	@CompetitionCode INT = NULL,
	@TournamentCode INT = NULL
AS
BEGIN
	DECLARE @CurrentTime DATETIME 
	SELECT @CurrentTime = GETDATE()

	SELECT t.tournament_code AS TournamentCode, 
        t.[year],	
		((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)) AS ImportingTotalPercentComplete,
		tcs.team_percent AS ImportingTeamPercent, 
		tcs.game_percent AS ImportingGamePercent, 
		tcs.player_percent AS ImportingPlayerPercent, 
		tcs.goals_percent AS ImportingGoalsPercent, 
		tcs.penalty_shootout_goals_percent AS ImportingPenaltyShootoutGoalsPercent,
		ISNULL(COUNT(g.game_code),0) AS TotalGames,
		SUM(g.team_1_normal_time_score) + SUM(ISNULL(g.team_1_extra_time_score,0)) + SUM(g.team_2_normal_time_score) + SUM(ISNULL(g.team_2_extra_time_score,0)) AS TotalGoals,
		(SELECT COUNT(g5.goal_code) FROM wc_goal g5 JOIN wc_game g6 ON g5.game_code = g6.game_code WHERE g6.tournament_code = t.tournament_code AND g5.is_penalty = 1) AS TotalPenalties,
		SUM(ISNULL(g.team_1_penalties_score,0)) + SUM(ISNULL(g.team_2_penalties_score,0)) AS TotalShootoutGoals
	FROM wc_tournament t
	LEFT JOIN wc_game g ON t.tournament_code = g.tournament_code
	JOIN vWC_TournamentCompletionStatistics tcs ON t.tournament_code = tcs.tournament_code 
	WHERE (t.tournament_code = @TournamentCode OR @TournamentCode IS NULL)
	AND (t.competition_code = @CompetitionCode OR @CompetitionCode IS NULL)
	AND t.competition_code != 4
	GROUP BY t.tournament_code, 
        t.[year],
		((tcs.team_percent * 0.25) + (tcs.game_percent * 0.25) + (tcs.player_percent * 0.25) + (tcs.goals_percent * 0.20) + (tcs.penalty_shootout_goals_percent * 0.05)),
		tcs.team_percent, 
		tcs.game_percent, 
		tcs.player_percent, 
		tcs.goals_percent, 
		tcs.penalty_shootout_goals_percent
	ORDER BY [year] DESC
END
GO