CREATE PROCEDURE [dbo].[GetFootballPoolPlayerSummary]
    @YearCode smallint,
    @WeekCode smallint,
	@PlayerCode smallint = null
AS

SELECT s.year_code as YearCode,
	s.week_code as WeekCode, 
	s.player_code as PlayerCode, 
	p.player_name as PlayerName,
	s.ranking as Ranking, 
	(SELECT count(*) FROM FBSummary WHERE year_code = @YearCode and week_code = @WeekCode) as PlayerCount
FROM FBSummary s
INNER JOIN FBPlayer p ON s.year_code = p.year_code and s.player_code = p.player_code
WHERE s.year_code = @YearCode 
and s.week_code = @WeekCode
and (@PlayerCode is null or s.player_code = @PlayerCode)
ORDER BY s.week_code, s.ranking