CREATE PROCEDURE dbo.spSteamWebAPI_GetStatsForToday
AS
SELECT count(*) as stat_summary
FROM steamAPI_stat
WHERE year(last_updated) = year(getdate())
and month(last_updated) = month(getdate())
and day(last_updated) = day(getdate())