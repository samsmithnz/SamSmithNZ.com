CREATE PROCEDURE [dbo].[spFF_GetAverageSetlist]
	@year_code int
AS

CREATE TABLE #tmp_show (show_key int, song_count int)

--Get the average size of a setlist by year
INSERT INTO #tmp_show
SELECT sh.show_key, count(ss.song_key)
FROM ff_show sh
INNER JOIN ff_show_song ss ON sh.show_key = ss.show_key
WHERE year(show_date) = @year_code
GROUP BY sh.show_key

DECLARE @average_song_count int
SELECT @average_song_count = avg(song_count)
FROM #tmp_show

select @average_song_count

--Create a setlist using the above count
/*SELECT sg.song_name, count(sg.song_key)
FROM ff_show sh
INNER JOIN ff_show_song ss ON sh.show_key = ss.show_key
INNER JOIN ff_song sg ON ss.song_key = sg.song_key
WHERE year(show_date) = @year_code
GROUP BY sg.song_name
ORDER BY count(sg.song_key) DESC
*/

SELECT sg.song_key, sg.song_name, a.song_count
FROM 
(
	SELECT ss.song_key, ROW_NUMBER() OVER(ORDER BY count(ss.song_key)) as song_count
	FROM ff_show sh
	INNER JOIN ff_show_song ss ON sh.show_key = ss.show_key
	WHERE year(show_date) = @year_code
	GROUP BY ss.song_key
) a
INNER JOIN ff_song sg ON a.song_key = sg.song_key
WHERE a.song_count >= @average_song_count
ORDER BY a.song_count DESC

DROP TABLE #tmp_show