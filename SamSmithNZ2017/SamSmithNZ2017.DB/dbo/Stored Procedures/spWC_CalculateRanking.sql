CREATE PROCEDURE dbo.spWC_CalculateRanking
	@ranking_date datetime
AS

SET NOCOUNT ON

DELETE FROM wc_ranking
WHERE ranking_date = @ranking_date

--SELECT * from wc_ranking
--http://www.seomoz.org/blog/how-to-make-awesome-ranking-charts
--http://www.moserware.com/2010/03/computing-your-skill.html
-----------------------------
---------   DONE  -----------
-----------------------------

--Match result
/*
Result						Points
Win (no penalty shootout)	+3
Win (penalty shootout)		+1
Draw						+1
Loss (penalty shootout)		-1
Loss (no penalty shootout)	-3
*/

--Opponent Strength
/*
opposition strength multiplier = (200 - ranking position) / 100
*/

--Game Age
/*
Date of match				Multiplier
Within the last 12 months	× 1.0
Within 12 - 24 months ago	× 0.8
Within 24 - 36 months ago	× 0.6
Within 36 - 48 months ago	× 0.4
Within 48 - 60 months ago	× 0.2
*/


-----------------------------
---------   TODO  -----------
-----------------------------

--Match Status
/*
Match status	Multiplier
Friendly match	x 1.0
FIFA World Cup and Continental cup qualifiers	x 2.5
Continental cup and Confederations Cup finals	x 3.0
World Cup finals match	x 4.0
*/

--Regional strength
/*
Confederation				After 2010 World Cup	After 2006 World Cup	Up to and including 2006 World Cup
UEFA (Europe)				1.00					1.00					1.00
CONMEBOL (South America)	1.00					0.98					1.00
CONCACAF (North America)	0.88					0.85					0.88
AFC (Asia)					0.86					0.85					0.85
CAF (Africa)				0.86					0.85					0.85
OFC (Oceania)				0.85					0.85					0.85
*/

--Number of Goals
/*
0			x0.5
1			x1
2			x1.5
3			x2
>3			x2.5
PK's		x0.5
*/

--Overall Formula
/*
Ranking Points = 100 X (result points X match status X opposition strength X regional strength X number of goals)
*/

IF (@ranking_date = '1900-1-1')
BEGIN
	INSERT INTO wc_ranking
	SELECT @ranking_date, team_code, 1 as ranking, 0 as score
	FROM wc_tournament_team_entry
	WHERE tournament_code = 1
END
ELSE
BEGIN

	CREATE TABLE #tmp_ranking (team_code smallint, total_score decimal(10,2))
	
	INSERT INTO #tmp_ranking
	SELECT team_code, 0
	FROM wc_tournament_team_entry te
	JOIN wc_tournament t ON te.tournament_code = t.tournament_code
	WHERE t.[year] = year(@ranking_date)
	
	-------------------------------------------------------------------------------
	--http://en.wikipedia.org/wiki/FIFA_World_Rankings#Current_calculation_method--
	-------------------------------------------------------------------------------
	
	CREATE TABLE #tmp_game_ranking (
		game_code smallint, 
		game_age_multiplier decimal(10,2),
		team_1_code smallint, 
		team_1_match_result decimal(10,2),
		team_1_opposition_strength decimal(10,2),
		team_2_code smallint, 
		team_2_match_result decimal(10,2),
		team_2_opposition_strength decimal(10,2))
	
	INSERT INTO #tmp_game_ranking
	SELECT game_code, 
		dbo.fnWC_CalculateRanking_GameAge(game_code,@ranking_date),
		team_1_code, 
		dbo.fnWC_CalculateRanking_MatchResult(game_code, team_1_code), 
		dbo.fnWC_CalculateRanking_OppositionStrength(game_code, team_1_code),
		team_2_code, 
		dbo.fnWC_CalculateRanking_MatchResult(game_code, team_2_code),
		dbo.fnWC_CalculateRanking_OppositionStrength(game_code, team_2_code)
	FROM wc_game
	WHERE year(game_time) = year(@ranking_date)
	ORDER BY game_time
	
	--SELECT * FROM #tmp_game_ranking
	
	--Get the team 1 scores
	INSERT INTO #tmp_ranking
	SELECT team_1_code, game_age_multiplier * SUM(team_1_match_result * team_1_opposition_strength)
	FROM #tmp_game_ranking
	GROUP BY team_1_code, game_age_multiplier
	
	--get the team 2 scores
	INSERT INTO #tmp_ranking
	SELECT team_2_code, game_age_multiplier * SUM(team_2_match_result * team_2_opposition_strength)
	FROM #tmp_game_ranking
	GROUP BY team_2_code, game_age_multiplier
	
	--Sum the scores, grouping by team code
	INSERT INTO wc_ranking
	SELECT @ranking_date, 
		team_code, 
		RANK() OVER (ORDER BY SUM(total_score) DESC), 
		SUM(total_score)
	FROM #tmp_ranking
	GROUP BY team_code
	
	
	--opposition strength multiplier = (200 - ranking position) / 100
	
	
	
	DROP TABLE #tmp_game_ranking
	DROP TABLE #tmp_ranking 
END