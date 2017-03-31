CREATE PROCEDURE [dbo].[spFB2_GetAccounting]
AS

DECLARE @current_year_code smallint
DECLARE @current_week_code smallint
SELECT @current_year_code = 2007, @current_week_code = 17

SELECT player_name, isnull(sum(amount_paid),0) + sum(weekly_dollars_won) + (-@current_week_code * 5)
FROM FBSummary s
INNER JOIN FBPlayer p ON p.player_code = s.player_code and p.year_code = s.year_code
LEFT OUTER JOIN FBAccounting a ON a.player_code = p.player_code and a.year_code = p.year_code
GROUP BY player_name
ORDER BY player_name