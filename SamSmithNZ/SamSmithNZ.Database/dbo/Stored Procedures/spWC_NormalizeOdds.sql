CREATE PROCEDURE [dbo].[spWC_NormalizeOdds]
	@odds_date datetime
AS

UPDATE o
SET o.odds_probability = CONVERT(decimal(18,4),o.odds_probability/ (SELECT sum(o2.odds_probability)
																	FROM wc_odds o2
																	WHERE o2.odds_date = @odds_date))
FROM wc_odds o
WHERE o.odds_date = @odds_date
and o.odds_probability > 0