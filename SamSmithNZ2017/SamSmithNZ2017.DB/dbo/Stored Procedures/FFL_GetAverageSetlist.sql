CREATE PROCEDURE [dbo].[FFL_GetAverageSetlist]
	@YearCode INT = NULL,
	@ShowMinimumSongCount INT = 0,
	@ShowAllSongs BIT = 0
AS
BEGIN
	--Get shows with the minimum number of songs played
	CREATE TABLE #ShowsToUse(ShowCode INT, ShowSongCount INT)
	
	INSERT INTO #ShowsToUse
	SELECT s.show_key AS ShowCode,
		COUNT(ss.song_key) AS NumberOfSongsPlayed
	FROM ff_show s
	LEFT JOIN ff_show_song ss ON ss.show_key = s.show_key
	WHERE (YEAR(s.show_date) = @YearCode OR @YearCode IS NULL)
	GROUP BY s.show_key
	HAVING COUNT(ss.song_key) > @ShowMinimumSongCount

	--Get Show Summary, with each show and the number of songs played
	CREATE TABLE #ShowList(ShowCode INT, ShowDate DATETIME, ShowSongCount INT)

	INSERT INTO #ShowList
	SELECT s.show_key AS ShowCode, 
		s.show_date AS ShowDate, 
		COUNT(ss.song_key) AS NumberOfSongsPlayed
	FROM ff_show s
	JOIN #ShowsToUse su ON s.show_key = su.ShowCode
	LEFT JOIN ff_show_song ss ON ss.show_key = s.show_key
	WHERE (YEAR(s.show_date) = @YearCode OR @YearCode IS NULL)
	GROUP BY s.show_key, s.show_date	
	ORDER BY s.show_date

	--Create Year Summary, with each year, number of shows, number of shows with songs known, and the average number of songs played
	CREATE TABLE #YearSummary(YearCode INT, ShowCount INT, ShowCountWithSetlist INT, AverageSongsPlayed DECIMAL(4,2))

	INSERT INTO #YearSummary
	SELECT YEAR(sl.ShowDate), COUNT(sl.ShowCode), NULL, NULL
	FROM #ShowList sl
	GROUP BY YEAR(sl.ShowDate)

	--Update setlist count
	UPDATE ys
	SET ys.ShowCountWithSetlist = (SELECT COUNT(sl.ShowSongCount) 
									FROM #ShowList sl 
									WHERE YEAR(sl.ShowDate) = ys.YearCode
									AND sl.ShowSongCount > @ShowMinimumSongCount)
	FROM #YearSummary ys

	--Update average songs played
	UPDATE ys
	SET ys.AverageSongsPlayed = (SELECT AVG(CONVERT(DECIMAL(4,2),sl.ShowSongCount)) 
									FROM #ShowList sl 
									WHERE YEAR(sl.ShowDate) = ys.YearCode
									AND sl.ShowSongCount > @ShowMinimumSongCount)
	FROM #YearSummary ys

	--SELECT * FROM #YearSummary

	--Create a Song Summary, with each year, the songs played, the number of times, the average setlist location, and then a ranking (based on song count)
	CREATE TABLE #SongSummary(YearCode INT, SongCode INT, SongCount INT, AvgShowSongOrder DECIMAL(4,2), SongRank INT)

	--Get Song Averages
	INSERT INTO #SongSummary
	SELECT YEAR(s.show_date) AS YearCode,
		ss.song_key AS SongCode, 
		COUNT(ss.song_key) AS SongCount, 
		CONVERT(DECIMAL(4,2), AVG(CONVERT(DECIMAL(4,2),ss.show_song_order))) AS AvgShowSongOrder,
		NULL
	FROM ff_show_song ss
	JOIN ff_show s ON s.show_key = ss.show_key
	JOIN #ShowsToUse su ON s.show_key = su.ShowCode
	WHERE (YEAR(s.show_date) = @YearCode OR @YearCode IS NULL)
	GROUP BY YEAR(s.show_date), ss.song_key
	ORDER BY COUNT(ss.song_key) DESC

	--Rank the songs, per year, with the highest played songs first
	;WITH cte AS
	(
		SELECT ss.SongRank, r = RANK() OVER(PARTITION BY ss.YearCode ORDER BY ss.SongCount DESC)
		FROM #SongSummary ss
	)
	UPDATE c 
	SET c.SongRank = r 
	FROM cte c;

	--Update the table for songs that have a ranking below the average yearly song count
	IF (@ShowAllSongs = 0)
	BEGIN
		DELETE ss FROM #SongSummary ss
		JOIN #YearSummary ys ON ss.YearCode = ys.YearCode
		WHERE ss.SongRank > ROUND(ys.AverageSongsPlayed, 0)
	END

	--Return list of n (rounded up showAverageSongCount), songs in order of AverageSetListPosition
	SELECT ss.YearCode,
		ss.SongCode,
		s.song_name AS SongName,
		ss.SongCount,
		ss.SongRank,
		ss.AvgShowSongOrder
	FROM #SongSummary ss
	JOIN ff_song s ON ss.SongCode = s.song_key
	ORDER BY YearCode, AvgShowSongOrder

	--Clean Up
	DROP TABLE #SongSummary
	DROP TABLE #YearSummary
	DROP TABLE #ShowList
	DROP TABLE #ShowsToUse

END
GO