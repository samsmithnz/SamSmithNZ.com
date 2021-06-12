CREATE PROCEDURE [dbo].[spWC_GetTournamentTeamPlayerList]
	@tournament_code INT,
	@team_code INT
AS
SELECT p.tournament_code, p.club_country_name, p.club_name, p.date_of_birth, p.is_captain, p.number, p.player_code, p.player_name, p.position, p.team_code 
FROM wc_player p
WHERE p.tournament_code = @tournament_code
and team_code = @team_code
ORDER BY player_code