CREATE PROCEDURE [dbo].[spFF_GetYearList]
AS
CREATE TABLE #tmp_year (year_code smallint, song_count_total smallint, song_count_with_date smallint)

INSERT INTO #tmp_year
SELECT DISTINCT year(s1.show_date) as year_code, count(s1.show_date), null
FROM vFF_Show s1
GROUP BY year(s1.show_date)

UPDATE t1
SET t1.song_count_with_date = (SELECT count(s1.show_date) 
								FROM vFF_Show s1
								WHERE t1.year_code = year(s1.show_date)
								and song_count = 0)
FROM #tmp_year t1

--Return the total
SELECT 3000 as year_code, '<select year>' as year_text
UNION
SELECT year_code, CONVERT(varchar(5),year_code) + ': ' + CONVERT(varchar(50),song_count_total) + ' (' + CONVERT(varchar(50),song_count_with_date) +  ' shows have no setlist data)' as year_text
FROM #tmp_year
ORDER BY year_code DESC

DROP TABLE #tmp_year