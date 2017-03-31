CREATE PROCEDURE [dbo].[GetFootballPoolWeek]
	@YearCode INT,
	@WeekCode INT,
	@PlayerCode INT,
	@ShowTeamRecords BIT
AS
SET NOCOUNT ON

--Get player picks and result based on score + status of game (open/closed/scored?)
DECLARE @EmployeeCount INT
DECLARE @PickCount INT

--Get the number of picks
SELECT @PickCount = count(*) 
FROM FBWeek
WHERE year_code = @YearCode 
and week_code = @WeekCode 
and player_code = @PlayerCode

IF (@PickCount = 0)
BEGIN
	--Create the weeks records
	INSERT INTO FBWeek
	SELECT @YearCode , @PlayerCode, @WeekCode, record_id, -1, 0, null --where -1 is nothing selected
	FROM FBWeekTemplate
	WHERE year_code = @YearCode and week_code = @WeekCode
	ORDER BY game_time
END

DECLARE @tmp_counts TABLE (record_id UNIQUEIDENTIFIER, fav_count DECIMAL(10,4))

INSERT INTO @tmp_counts 
SELECT w.record_id, sum(w.fav_team_picked)
FROM FBWeek w
WHERE w.year_code = @YearCode and w.week_code = @WeekCode and w.fav_team_picked >= 0 
GROUP BY w.record_id

DECLARE @WeekPlayerTotal DECIMAL(10,4)
SELECT @WeekPlayerTotal = count(*)
FROM FBWeekTemplate wt 
INNER JOIN FBWeek w ON wt.record_id = w.record_id
WHERE w.year_code = @YearCode and w.week_code = @WeekCode and w.fav_team_picked >= 0 
GROUP BY wt.record_id

DECLARE @dateRightNow DATETIME
SELECT @dateRightNow = DATEADD(hh,-5,GETDATE())

--Hide the % if only a few people have submitted
--IF (@WeekPlayerTotal < 30)
--BEGIN
--	SELECT @WeekPlayerTotal = 1
--END

--Now get the weeks records
SELECT w.record_id AS RecordId, 
	w.week_code AS WeekCode, 
	w.player_code AS PlayerCode, 
	wt.home_team_code AS HomeTeamCode, 
	t1.image_name AS HomeImageName, 
	t1.team_name AS HomeTeamName, 
	dbo.fnFB_GetTeamRecord(wt.home_team_code, @YearCode, @WeekCode) AS HomeTeamRecord, 
	wt.away_team_code AS AwayTeamCode, 
	t2.image_name AS AwayImageName,
	t2.team_name AS AwayTeamName,
	dbo.fnFB_GetTeamRecord(wt.away_team_code, @YearCode, @WeekCode)AS AwayTeamRecord, 
	wt.fav_team_code AS FavoriteTeamCode, 
	wt.game_time AS GameTime, 
	wt.spread AS Spread, 
	w.fav_team_picked AS FavoriteTeamPicked, 
	isnull(w.last_updated,'1900-1-1') AS LastUpdated,
	wt.home_team_result AS HomeTeamResult, 
	wt.away_team_result AS AwayTeamResult, 
	wt.fav_team_won_game AS FavoriteTeamWonGame, 
	w.won_pick AS PlayerWonPick, 
	convert(DECIMAL(10,4),isnull((c.fav_count/@WeekPlayerTotal),0)) AS FavoritePickedPercent,
	--DATEDIFF(n, wt.game_time, @dateRightNow) AS GameLockedMinutesNotAdjusted,
	--(DATEDIFF(n, wt.game_time, @dateRightNow) - 15) AS GameLockedMinutes,
	CASE WHEN (DATEDIFF(n, wt.game_time, @dateRightNow) - 15) > 0 THEN 1 ELSE 0 END AS GameLocked
FROM FBWeek w
INNER JOIN FBWeekTemplate wt ON w.record_id = wt.record_id
INNER JOIN FBTeam t1 ON wt.home_team_code = t1.team_code
INNER JOIN FBTeam t2 ON wt.away_team_code = t2.team_code
LEFT JOIN @tmp_counts c ON w.record_id = c.record_id
WHERE w.year_code = @YearCode and w.week_code = @WeekCode and w.player_code = @PlayerCode
ORDER BY wt.game_time