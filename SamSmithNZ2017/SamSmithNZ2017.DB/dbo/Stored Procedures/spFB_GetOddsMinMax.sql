CREATE PROCEDURE [dbo].[spFB_GetOddsMinMax]	
	@year_code int = null
AS

IF (@year_code is null)
BEGIN
	SELECT @year_code = 2014
END

DECLARE @min_date datetime
DECLARE @max_date datetime

SELECT @min_date = min(odds_date)
FROM fb_odds
WHERE year_code = @year_code
SELECT @max_date = max(odds_date)
FROM fb_odds
WHERE year_code = @year_code

SELECT o1.team_name, o2.odds_probability - o1.odds_probability as odds_difference 
FROM fb_odds o1
INNER JOIN fb_odds o2 ON o1.team_name = o2.team_name and o1.year_code = o2.year_code
WHERE o1.odds_date = @min_date
and o2.odds_date = @max_date
and o1.year_code = @year_code
ORDER BY o2.odds_probability desc, o1.team_name