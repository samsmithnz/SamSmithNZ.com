CREATE PROCEDURE [dbo].[spWC_GetTournamentName]
	@tournament_code INT
AS
SELECT [name] AS tournament_name
FROM wc_tournament
WHERE tournament_code = @tournament_code