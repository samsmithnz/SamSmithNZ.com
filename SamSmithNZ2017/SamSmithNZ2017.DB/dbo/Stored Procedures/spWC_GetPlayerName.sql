CREATE PROCEDURE [dbo].[spWC_GetPlayerName]
	@player_code smallint
AS
SELECT player_name
FROM wc_player
WHERE player_code = @player_code