CREATE PROCEDURE [dbo].[spFB_AddTeamOdds]
    @team_name VARCHAR(100), 
    @odds_date DATETIME,
    @odds_probability DECIMAL(18, 4),
    @odds_mean DECIMAL(18, 4),
    @odds_max DECIMAL(18, 4),
    @odds_min DECIMAL(18, 4),
    @odds_stdDev DECIMAL(18, 4),
    @odds_sample_size INT,
    @year_code INT = null
AS

IF (exists (SELECT 1 FROM fb_odds WHERE team_name = @team_name and odds_date = @odds_date))
BEGIN
 DELETE FROM fb_odds WHERE team_name = @team_name and odds_date = @odds_date
END

IF (@year_code is null)
BEGIN
 SELECT @year_code = 2014
END

IF (@team_name = 'Tampa Bay Buccanneers')
SET @team_name = 'Tampa Bay Buccaneers'

INSERT INTO fb_odds
SELECT @team_name, 
    @odds_date,
    @odds_probability,
    @odds_mean,
    @odds_max,
    @odds_min,
    @odds_stdDev,
    @odds_sample_size,
 @year_code