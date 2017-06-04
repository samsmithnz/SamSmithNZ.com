CREATE PROCEDURE [dbo].[spWC_AddMissingTeamOdds]
	@odds_date datetime,
	@tournament_code SMALLINT = null
AS
CREATE TABLE #tmp_teams (team_name varchar(200))

IF (@tournament_code is null)
BEGIN
	SELECT @tournament_code = 20
END

IF (not exists (SELECT 1 FROM wc_odds WHERE tournament_code = @tournament_code))
BEGIN
    exec [spWC_AddOddsForOldTournament] @tournament_code
END

--IF (@tournament_code = 20)
--BEGIN
--	INSERT INTO #tmp_teams
--	SELECT distinct team_name 
--	FROM wc_odds
--	WHERE team_name not in (SELECT team_name
--							FROM wc_odds
--							WHERE odds_date = @odds_date
--							and tournament_code = @tournament_code)
--	and tournament_code = @tournament_code

--	DELETE FROM wc_odds
--	WHERE team_name in (SELECT team_name FROM #tmp_teams)
--	and odds_date = @odds_date
--	and tournament_code = @tournament_code

--	INSERT INTO wc_odds
--	SELECT distinct team_name, @odds_date, 0, 0, 0, 0, 0, 0, @tournament_code
--	FROM #tmp_teams

--	SELECT * 
--	FROM wc_odds
--	WHERE odds_date = @odds_date
--	and odds_probability = 0
--	and tournament_code = @tournament_code
--	ORDER BY team_name

--	DROP TABLE #tmp_teams
--END
--ELSE
--BEGIN
	SELECT o.tournament_code, o.team_name, o.odds_date, o.odds_max, o.odds_mean, o.odds_min, o.odds_probability, o.odds_sample_size, o.odds_stdDev
	FROM wc_odds o
	WHERE 0 = 1
	and tournament_code = @tournament_code
	ORDER BY team_name
--END