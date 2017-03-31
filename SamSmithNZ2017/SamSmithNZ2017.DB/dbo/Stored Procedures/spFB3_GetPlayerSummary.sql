CREATE PROCEDURE [dbo].[spFB3_GetPlayerSummary]
    @year_code smallint,
    @player_code smallint
AS

SELECT week_code, player_code, ranking 
FROM fbsummary 
WHERE year_code = @year_code 
and player_code = @player_code 
ORDER BY week_code