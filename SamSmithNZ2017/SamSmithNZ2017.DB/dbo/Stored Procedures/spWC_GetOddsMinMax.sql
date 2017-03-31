CREATE PROCEDURE [dbo].[spWC_GetOddsMinMax]	
	@tournament_code smallint = null
AS

IF (@tournament_code is null)
BEGIN
	SELECT @tournament_code = 20
END

DECLARE @min_date datetime
DECLARE @max_date datetime

SELECT @min_date = min(odds_date)
FROM wc_odds
WHERE tournament_code = @tournament_code
SELECT @max_date = max(odds_date)
FROM wc_odds
WHERE tournament_code = @tournament_code

SELECT o1.team_name, o2.odds_probability - o1.odds_probability as odds_difference 
FROM wc_odds o1
INNER JOIN wc_odds o2 ON o1.team_name = o2.team_name and o1.tournament_code = o2.tournament_code
WHERE o1.odds_date = @min_date
and o2.odds_date = @max_date
and o1.tournament_code = @tournament_code
ORDER BY o2.odds_probability desc, o1.team_name