
CREATE PROCEDURE [dbo].spFB3_GetAccountingSummary
	@year_code smallint
AS

SELECT at.year_code, p.player_code, p.player_name, SUM(at.amount) as balance
FROM fbaccountingtransaction at
INNER JOIN fbplayer p ON at.year_code = p.year_code and at.player_code = p.player_code
WHERE at.year_code = @year_code
GROUP BY at.year_code, p.player_code, p.player_name
ORDER BY at.year_code, p.player_name, p.player_code