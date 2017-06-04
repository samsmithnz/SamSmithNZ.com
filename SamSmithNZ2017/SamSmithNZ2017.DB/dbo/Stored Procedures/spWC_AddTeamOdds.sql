CREATE PROCEDURE [dbo].[spWC_AddTeamOdds]
	@team_name VARCHAR(200), 
    @odds_date DATETIME,
    @odds_probability DECIMAL(18, 4),
    @odds_mean DECIMAL(18, 4),
    @odds_max DECIMAL(18, 4),
    @odds_min DECIMAL(18, 4),
    @odds_stdDev DECIMAL(18, 4),
    @odds_sample_size INT,
	@tournament_code SMALLINT = null
AS

IF (exists (SELECT 1 FROM wc_odds WHERE team_name = @team_name and odds_date = @odds_date))
BEGIN
	DELETE FROM wc_odds WHERE team_name = @team_name and odds_date = @odds_date
END

IF (@tournament_code is null)
BEGIN
	SELECT @tournament_code = 20
END

INSERT INTO wc_odds
SELECT @team_name, 
    @odds_date,
    @odds_probability,
    @odds_mean,
    @odds_max,
    @odds_min,
    @odds_stdDev,
    @odds_sample_size,
	@tournament_code