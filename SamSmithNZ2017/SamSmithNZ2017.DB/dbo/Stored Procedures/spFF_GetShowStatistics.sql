
CREATE PROCEDURE [dbo].[spFF_GetShowStatistics]
AS

DECLARE @total smallint
DECLARE @existing smallint
DECLARE @missing smallint

CREATE TABLE #tmp_count (item smallint)

SELECT @total = isnull(count(*),0)
FROM ff_show

INSERT INTO #tmp_count
SELECT isnull(count(ss.song_key),0) 
FROM ff_show sh
LEFT OUTER JOIN ff_show_song ss ON sh.show_key = ss.show_key
GROUP BY sh.show_key, sh.show_date
HAVING isnull(count(ss.song_key),0) > 0

SELECT @existing = count(*)
FROM #tmp_count

TRUNCATE TABLE #tmp_count

INSERT INTO #tmp_count
SELECT isnull(count(ss.song_key),0) 
FROM ff_show sh
LEFT OUTER JOIN ff_show_song ss ON sh.show_key = ss.show_key
GROUP BY sh.show_key, sh.show_date
HAVING isnull(count(ss.song_key),0) = 0

SELECT @missing = count(*)
FROM #tmp_count

TRUNCATE TABLE #tmp_count

SELECT @total as total_count, @existing as existing_count, @missing as missing_count

SELECT sh.show_key, sh.show_date, isnull(count(ss.song_key),0) as song_count
FROM ff_show sh
LEFT OUTER JOIN ff_show_song ss ON sh.show_key = ss.show_key
GROUP BY sh.show_key, sh.show_date
HAVING isnull(count(ss.song_key),0) = 0
ORDER BY sh.show_date desc, sh.show_key desc

DROP TABLE #tmp_count