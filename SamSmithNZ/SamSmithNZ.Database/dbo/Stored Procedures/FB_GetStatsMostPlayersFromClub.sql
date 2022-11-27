CREATE PROCEDURE [dbo].[FB_GetStatsMostPlayersFromClub]
	@TournamentCode INT = NULL
AS
BEGIN
	SELECT TOP 10 club_name, club_country_name, count(*) 
	FROM wc_player p
	WHERE (@TournamentCode IS NULL OR p.tournament_code = @TournamentCode)
	GROUP BY club_name, club_country_name
	ORDER BY count(*) DESC
END
GO
