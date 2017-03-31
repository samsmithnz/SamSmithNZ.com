CREATE PROCEDURE [dbo].[spFB_AddMissingTeamOdds]
	@odds_date datetime,
	@year_code INT = null
AS
CREATE TABLE #tmp_teams (team_name varchar(100))

IF (@year_code is null)
BEGIN
	SELECT @year_code = 2014
END

--IF (not exists (SELECT 1 FROM fb_odds WHERE year_code = @year_code))
--BEGIN
--    exec [spWC_AddOddsForOldTournament] @tournament_code
--END

IF (@year_code = 2014)
BEGIN
	INSERT INTO #tmp_teams
	SELECT distinct team_name 
	FROM fb_odds
	WHERE team_name not in (SELECT team_name
							FROM fb_odds
							WHERE odds_date = @odds_date
							and year_code = @year_code)
	and year_code = @year_code

	DELETE FROM fb_odds
	WHERE team_name in (SELECT team_name FROM #tmp_teams)
	and odds_date = @odds_date
	and year_code = @year_code

	INSERT INTO fb_odds
	SELECT distinct team_name, @odds_date, 0, 0, 0, 0, 0, 0, @year_code
	FROM #tmp_teams

	SELECT * 
	FROM fb_odds
	WHERE odds_date = @odds_date
	and odds_probability = 0
	and year_code = @year_code
	ORDER BY team_name

	DROP TABLE #tmp_teams
END
ELSE
BEGIN
	SELECT * 
	FROM fb_odds
	WHERE 0 = 1
	and year_code = @year_code
	ORDER BY team_name
END