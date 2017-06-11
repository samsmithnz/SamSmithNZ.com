CREATE PROCEDURE [dbo].[FB_GetGames]
	@TournamentCode INT = NULL,
	@RoundNumber INT = NULL,
	@RoundCode VARCHAR(10) = NULL,
	@TeamCode INT = NULL
AS
BEGIN
	
	IF (NOT @TeamCode IS NULL)
	BEGIN
		SELECT g.round_number AS RoundNumber, 
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
			t2.team_code AS Team2Code, 
			t2.team_name AS Team2Name, 
			g.team_2_normal_time_score AS Team2NormalTimeScore, 
			g.team_2_extra_time_score AS Team2ExtraTimeScore, 
			g.team_2_penalties_score AS Team2PenaltiesScore, 
			t1.flag_name AS Team1FlagName, 
			t2.flag_name AS Team2FlagName,
			CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team1withdrew,
			CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team2withdrew,
			g.[location] AS [Location], 
			t.tournament_code AS TournamentCode, 
			t.[name] AS TournamentName, 
			ISNULL(te.coach_name,'') AS CoachName, 
			ISNULL(t3.flag_name,'') AS CoachFlag,
			ISNULL(te.fifa_ranking,0) AS FifaRanking, 
			NULL AS IsPenalty, 
			NULL AS IsOwnGoal, 
			-1 AS SortOrder
		FROM wc_game g
		JOIN wc_round r ON g.round_code = r.round_code
		JOIN wc_team t1 ON g.team_1_code = t1.team_code
		JOIN wc_team t2 ON g.team_2_code = t2.team_code
		JOIN wc_tournament t ON g.tournament_code = t.tournament_code
		LEFT JOIN wc_tournament_team_entry te ON t.tournament_code = te.tournament_code 
		LEFT JOIN wc_team t3 ON te.coach_nationality = t3.team_name
		--JOIN #tmp_round_codes rc ON g.round_code = rc.round_code
		WHERE t.competition_code = 1
		AND (g.team_1_code = @TeamCode OR g.team_2_code = @TeamCode)
		AND te.team_code = @TeamCode
		ORDER BY g.game_time DESC, g.game_number--, gl.goal_time, ISNULL(gl.injury_time,0)
	END
	ELSE
	BEGIN
		SELECT g.round_number AS RoundNumber, 
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
			t2.team_code AS Team2Code, 
			t2.team_name AS Team2Name, 
			g.team_2_normal_time_score AS Team2NormalTimeScore, 
			g.team_2_extra_time_score AS Team2ExtraTimeScore, 
			g.team_2_penalties_score AS Team2PenaltiesScore, 
			t1.flag_name AS Team1FlagName, 
			t2.flag_name AS Team2FlagName,
			CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team1withdrew,
			CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team2withdrew,
			g.[location] AS [Location], 
			t.tournament_code AS TournamentCode, 
			t.[name] AS TournamentName, 
			NULL AS CoachName, 
			NULL AS CoachFlag,
			0 AS FifaRanking, 
			NULL AS IsPenalty, 
			NULL AS IsOwnGoal, 
			-1 AS SortOrder
		FROM wc_game g
		JOIN wc_team t1 ON g.team_1_code = t1.team_code
		JOIN wc_team t2 ON g.team_2_code = t2.team_code
		JOIN wc_tournament t ON g.tournament_code = t.tournament_code
		LEFT JOIN wc_round r ON g.round_code = r.round_code
		WHERE (@TournamentCode IS NULL OR g.tournament_code = @TournamentCode)
		AND (@RoundNumber IS NULL OR g.round_number = @RoundNumber)
		AND (@RoundCode IS NULL OR g.round_code = @RoundCode)
		ORDER BY g.game_time, g.game_number
	END
END