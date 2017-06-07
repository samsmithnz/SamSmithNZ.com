CREATE PROCEDURE [dbo].FFL_GetYearList
AS
BEGIN
	CREATE TABLE #YearSummary (YearCode INT, SongCountTotal INT, SongCountWithDate INT)

	INSERT INTO #YearSummary
	SELECT DISTINCT YEAR(s1.ShowDate) AS YearCode, COUNT(s1.ShowDate), NULL
	FROM vFF_Show s1
	GROUP BY YEAR(s1.ShowDate)

	UPDATE t1
	SET t1.SongCountWithDate = (SELECT COUNT(s1.ShowDate) 
									FROM vFF_Show s1
									WHERE t1.YearCode = YEAR(s1.ShowDate)
									AND s1.SongCount = 0)
	FROM #YearSummary t1

	SELECT YearCode AS YearCode, 
		CONVERT(VARCHAR(5),YearCode) + ': ' + CONVERT(VARCHAR(50),SongCountTotal - SongCountWithDate) + '/' + CONVERT(VARCHAR(50),SongCountTotal) +  ' of shows have setlist data' AS YearText
	FROM #YearSummary 
	ORDER BY YearCode DESC

	DROP TABLE #YearSummary
END