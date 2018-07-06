CREATE PROCEDURE [dbo].[FB_GetTournamentTeamsPlacing]
	@TournamentCode INT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tmp_final_placing (SortOrder int, FinalPlacing VARCHAR(50), TeamCode INT)

	IF (@TournamentCode <= 19)
	BEGIN
		INSERT INTO #tmp_final_placing
		SELECT final_placing, final_placing, team_code 
		FROM wc_tournament_team_final_placing
		WHERE tournament_code = @TournamentCode
	END
	ELSE
	BEGIN
		--1st Place
		INSERT INTO #tmp_final_placing 
		SELECT 1, '1st', 
			CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'FF'
 
		--2nd Place
		INSERT INTO #tmp_final_placing 
		SELECT 2, '2nd', 
			CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'FF'

		--3rd Place
		INSERT INTO #tmp_final_placing 
		SELECT 3, '3rd', 
			CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '3P'

		--4th Place
		INSERT INTO #tmp_final_placing 
		SELECT 4, '4th', 
			CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '3P'

		--Top 4
		INSERT INTO #tmp_final_placing 
		SELECT 4, 
			CASE WHEN te.is_active = 1 THEN 'Top 4' END,
			g.team_1_code
			--CASE WHEN g.team_1_normal_time_score IS NULL THEN g.team_1_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE 0 END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'SF'
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		INSERT INTO #tmp_final_placing 
		SELECT 4, 
			CASE WHEN te.is_active = 1 THEN 'Top 4' END,
			g.team_2_code
			--CASE WHEN g.team_2_normal_time_score IS NULL THEN g.team_2_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN 0 ELSE g.team_2_code END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'SF'
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)

		--5th - 8th Place
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 5 ELSE 6 END, 
			CASE WHEN te.is_active = 1 THEN 'Top 8' ELSE 'Knocked out in quarter finals' END,
			g.team_1_code
			--CASE WHEN g.team_1_normal_time_score IS NULL THEN g.team_1_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE 0 END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'QF'
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 5 ELSE 6 END, 
			CASE WHEN te.is_active = 1 THEN 'Top 8' ELSE 'Knocked out in quarter finals' END,
			g.team_2_code
			--CASE WHEN g.team_2_normal_time_score IS NULL THEN g.team_2_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN 0 ELSE g.team_2_code END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'QF'
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)

		--9th - 16th Place
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 9 ELSE 10 END AS SortOrder,  
			CASE WHEN te.is_active = 1 THEN 'Top 16' ELSE 'Knocked out in top 16' END AS info,
			g.team_1_code
			--CASE WHEN g.team_1_normal_time_score IS NULL THEN g.team_1_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE 0 END 
			--END AS TeamCode
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '16'
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 9 ELSE 10 END, 
			CASE WHEN te.is_active = 1 THEN 'Top 16' ELSE 'Knocked out in top 16' END,
			g.team_2_code
			--CASE WHEN g.team_2_normal_time_score IS NULL THEN g.team_2_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN 0 ELSE g.team_2_code END 
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '16'
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)

		--17th - 32nd Place
		INSERT INTO #tmp_final_placing 
		SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 17 ELSE 18 END,  
			CASE WHEN te.is_active = 1 THEN 'Top 32' ELSE 'Knocked out in group stage' END, 
			g.team_1_code
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		INSERT INTO #tmp_final_placing 
		SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 17 ELSE 18 END, 
			CASE WHEN te.is_active = 1 THEN 'Top 32' ELSE 'Knocked out in group stage' END, 
			g.team_2_code
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing)
	END

	SELECT DISTINCT fp.FinalPlacing AS Placing, 
		t.team_code AS TeamCode, 
		t.team_name AS TeamName, 
		t.flag_name AS FlagName, 
		r.region_code AS RegionCode, 
		r.region_abbrev AS RegionName, 
		ISNULL(te.fifa_ranking,0) AS FifaRanking, 
		te.coach_name AS CoachName, 
		ISNULL(ct.flag_name,'') AS CoachNationalityFlagName,
		e.elo_rating AS ELORating,
		te.is_active AS IsActive,
		fp.SortOrder,
		cw.chance_to_win * 100 AS ChanceToWin
	FROM #tmp_final_placing fp
	JOIN wc_team t ON fp.TeamCode = t.team_code
	JOIN wc_tournament_team_entry te ON te.team_code = t.team_code
	JOIN wc_region r ON t.region_code = r.region_code
	LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
	LEFT JOIN wc_tournament_team_elo_rating e ON te.tournament_code = e.tournament_code AND te.team_code = e.team_code
	LEFT JOIN wc_tournament_team_chance_to_win cw ON te.tournament_code = cw.tournament_code AND te.team_code = cw.team_code
	WHERE te.tournament_code = @TournamentCode
	--AND fp.TeamCode = 29
	ORDER BY cw.chance_to_win * 100 DESC,
		fp.SortOrder, 
		e.elo_rating DESC, 
		t.team_name

	DROP TABLE #tmp_final_placing
END