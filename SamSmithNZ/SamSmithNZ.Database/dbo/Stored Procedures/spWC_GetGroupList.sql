CREATE PROCEDURE [dbo].[spWC_GetGroupList]
	@tournament_code INT,
	@round_number INT
AS
SELECT distinct round_code
FROM --wc_tournament_team_entry te JOIN 
wc_group_stage gs --ON te.tournament_code = gs.tournament_code
WHERE gs.tournament_code = @tournament_code
and gs.round_number = @round_number
ORDER BY round_code