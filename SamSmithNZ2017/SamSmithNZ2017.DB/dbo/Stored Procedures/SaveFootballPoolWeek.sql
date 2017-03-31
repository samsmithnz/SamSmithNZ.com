CREATE PROCEDURE [dbo].[SaveFootballPoolWeek]
	@PlayerCode smallint,
	@RecordId uniqueidentifier,
	@Pick smallint
AS

DECLARE @DateRightNow DATETIME
DECLARE @GameTime DATETIME

SELECT @DateRightNow = DATEADD(hh,-5,GETDATE()), 
	@GameTime = wt.game_time
FROM FBWeekTemplate wt 
WHERE wt.record_id = @RecordId

IF ((DATEDIFF(n, @GameTime, @DateRightNow) - 15) > 0) --Check to see if the game is locked
BEGIN 
	SELECT 0
END
ELSE 
BEGIN
	UPDATE FBWeek
	SET fav_team_picked = @Pick, last_updated = getdate()
	WHERE player_code = @PlayerCode 
	AND record_id = @RecordId

	SELECT 1
END