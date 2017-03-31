CREATE PROCEDURE [dbo].[spIFB_GetGroupList]
	@tournament_code smallint,
	@round_number smallint
AS
SELECT distinct round_code
FROM --wc_tournament_team_entry te INNER JOIN 
wc_group_stage gs --ON te.tournament_code = gs.tournament_code
WHERE gs.tournament_code = @tournament_code
and gs.round_number = @round_number
ORDER BY round_code