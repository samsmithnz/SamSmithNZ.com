CREATE PROCEDURE [dbo].[spWC_GetTournamentTeamPlayerList]
	@tournament_code smallint,
	@team_code smallint
AS
SELECT * 
FROM wc_player
WHERE tournament_code = @tournament_code
and team_code = @team_code
ORDER BY player_code