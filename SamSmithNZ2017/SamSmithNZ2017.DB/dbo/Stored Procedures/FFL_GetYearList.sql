CREATE PROCEDURE [dbo].FFL_GetYearList
AS
BEGIN
	CREATE TABLE #tmp_year (year_code INT, song_count_total INT, song_count_with_date INT)

	INSERT INTO #tmp_year
	SELECT DISTINCT YEAR(s1.show_date) AS year_code, COUNT(s1.show_date), NULL
	FROM vFF_Show s1
	GROUP BY YEAR(s1.show_date)

	UPDATE t1
	SET t1.song_count_with_date = (SELECT COUNT(s1.show_date) 
									FROM vFF_Show s1
									WHERE t1.year_code = YEAR(s1.show_date)
									AND song_count = 0)
	FROM #tmp_year t1

	--Return the total
	SELECT 3000 AS YearCode, 
		'<select year>' AS YearText
	UNION
	SELECT year_code AS YearCode, 
		CONVERT(VARCHAR(5),year_code) + ': ' + CONVERT(VARCHAR(50),song_count_total - song_count_with_date) + '/' + CONVERT(VARCHAR(50),song_count_total) +  ' of shows have setlist data' AS YearText
	FROM #tmp_year
	ORDER BY YearCode DESC

	DROP TABLE #tmp_year
END