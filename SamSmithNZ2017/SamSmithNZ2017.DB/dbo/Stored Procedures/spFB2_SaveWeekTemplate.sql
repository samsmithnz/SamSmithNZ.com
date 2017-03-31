CREATE PROCEDURE [dbo].[spFB2_SaveWeekTemplate]
	@record_id uniqueidentifier,
	@home_team_code smallint,
	@away_team_code smallint,
	@fav_team_code smallint,
	@game_time smalldatetime,
	@spread decimal(18,1),
	@home_team_result smallint,
	@away_team_result smallint,
	@fav_team_won_game smallint
AS

UPDATE FBWeekTemplate
SET home_team_code = @home_team_code, away_team_code = @away_team_code, fav_team_code = @fav_team_code, game_time = @game_time, spread = @spread,
home_team_result = @home_team_result, away_team_result = @away_team_result, fav_team_won_game = @fav_team_won_game
WHERE record_id = @record_id

UPDATE w 
SET won_pick = 1
FROM FBWeek w
INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
WHERE w.record_id = @record_id 
and ((fav_team_picked = 1 and fav_team_won_game = 1)
or (fav_team_picked = 0 and fav_team_won_game = 0))
and fav_team_picked >= 0

UPDATE w 
SET won_pick = 0
FROM FBWeek w
INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
WHERE w.record_id = @record_id 
and ((fav_team_picked = 1 and fav_team_won_game = 0)
or (fav_team_picked = 0 and fav_team_won_game = 1))
and fav_team_picked >= 0