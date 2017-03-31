CREATE PROCEDURE [dbo].[spWC_GetGroupDetails]
	@tournament_code smallint,
	@round_number smallint,
	@round_code varchar(10)
AS
SELECT t.team_name, 
	CONVERT(varchar(100),t.flag_name) as team_flag_name, 
	gs.* 
FROM wc_group_stage gs
INNER JOIN wc_team t ON gs.team_code = t.team_code
WHERE tournament_code = @tournament_code
and round_number = @round_number
and round_code = @round_code
ORDER BY group_ranking