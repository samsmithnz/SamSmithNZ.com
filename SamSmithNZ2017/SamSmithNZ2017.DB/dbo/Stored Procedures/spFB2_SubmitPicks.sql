
CREATE PROCEDURE [dbo].[spFB2_SubmitPicks]
	@player_code smallint,
	@record_id uniqueidentifier,
	@pick smallint
AS

UPDATE fbweek
SET fav_team_picked = @pick, last_updated = getdate()
WHERE player_code = @player_code and record_id = @record_id