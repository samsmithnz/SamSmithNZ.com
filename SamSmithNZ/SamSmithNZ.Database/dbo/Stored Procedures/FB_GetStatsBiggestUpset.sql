CREATE PROCEDURE [dbo].[FB_GetStatsBiggestUpset]
	@TournamentCode INT = NULL
AS
BEGIN
/*
	SELECT CASE WHEN team_1_pregame_elo_rating - team_2_pregame_elo_rating < 0 
		THEN (team_1_pregame_elo_rating - team_2_pregame_elo_rating) * -1 
		ELSE team_1_pregame_elo_rating - team_2_pregame_elo_rating END 
	FROM wc_game g 
	WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
	AND team_1_pregame_elo_rating IS NOT NULL 
	AND team_2_pregame_elo_rating IS NOT NULL
	ORDER BY CASE WHEN team_1_pregame_elo_rating - team_2_pregame_elo_rating < 0 
		THEN (team_1_pregame_elo_rating - team_2_pregame_elo_rating) * -1 
		ELSE team_1_pregame_elo_rating - team_2_pregame_elo_rating END DESC
*/
/*
1. Need to get difference between starting elo ratings
2. For teams that were expected to win, but then lost or drew
*/
	;WITH cte AS
	(
	SELECT *,
			ROW_NUMBER() OVER (PARTITION BY tournament_code ORDER BY CASE WHEN team_1_pregame_elo_rating - team_2_pregame_elo_rating < 0 
			THEN (team_1_pregame_elo_rating - team_2_pregame_elo_rating) * -1 
			ELSE team_1_pregame_elo_rating - team_2_pregame_elo_rating END DESC) AS rn
	FROM wc_game
	)
	SELECT 1 AS RowType, --1 is a team
		g.round_number AS RoundNumber, 
		g.round_code AS RoundCode, 
		r.round_name AS RoundName, 
		g.game_code AS GameCode, 
		g.game_number AS GameNumber, 
		g.game_time AS GameTime,  
		t1.team_code AS Team1Code, 
		t1.team_name AS Team1Name, 
		g.team_1_normal_time_score AS Team1NormalTimeScore, 
		g.team_1_extra_time_score AS Team1ExtraTimeScore, 
		g.team_1_penalties_score AS Team1PenaltiesScore,
		g.team_1_elo_rating AS Team1EloRating,
		g.team_1_pregame_elo_rating AS Team1PreGameEloRating,
		g.team_1_postgame_elo_rating AS Team1PostGameEloRating,
		t2.team_code AS Team2Code, 
		t2.team_name AS Team2Name, 
		g.team_2_normal_time_score AS Team2NormalTimeScore, 
		g.team_2_extra_time_score AS Team2ExtraTimeScore, 
		g.team_2_penalties_score AS Team2PenaltiesScore,  
		g.team_2_elo_rating AS Team2EloRating,
		g.team_2_pregame_elo_rating AS Team2PreGameEloRating,
		g.team_2_postgame_elo_rating AS Team2PostGameEloRating,
		t1.flag_name AS Team1FlagName, 
		t2.flag_name AS Team2FlagName,
		CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team1Withdrew,
		CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team2Withdrew,
		g.[location] AS [Location], 
		t.tournament_code AS TournamentCode, 
		t.[name] AS TournamentName, 
		NULL AS CoachName, 
		NULL AS CoachFlag,
		NULL AS IsPenalty, 
		NULL AS IsOwnGoal, 
		NULL AS IsGoldenGoal,
		1 AS SortOrder
	FROM cte g
	JOIN wc_team t1 ON g.team_1_code = t1.team_code
	JOIN wc_team t2 ON g.team_2_code = t2.team_code
	JOIN wc_tournament t ON g.tournament_code = t.tournament_code
	LEFT JOIN wc_round r ON g.round_code = r.round_code 
	WHERE rn = 1
	AND (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
	ORDER BY CASE WHEN team_1_pregame_elo_rating - team_2_pregame_elo_rating < 0 
			THEN (team_1_pregame_elo_rating - team_2_pregame_elo_rating) * -1 
			ELSE team_1_pregame_elo_rating - team_2_pregame_elo_rating END DESC
END
GO
