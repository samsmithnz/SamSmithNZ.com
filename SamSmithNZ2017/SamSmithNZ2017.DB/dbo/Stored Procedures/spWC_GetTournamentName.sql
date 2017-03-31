CREATE PROCEDURE [dbo].[spWC_GetTournamentName]
	@tournament_code smallint
AS
SELECT [name] as tournament_name
FROM wc_tournament
WHERE tournament_code = @tournament_code