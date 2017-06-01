CREATE PROCEDURE [dbo].[FFL_GetShows]
	@ShowCode INT = NULL,
	@YearCode INT = NULL,
	@SongCode INT = NULL
AS
BEGIN
	SELECT s.show_key AS ShowCode, 
		s.show_date AS ShowDate, 
		s.show_location AS ShowLocation,
		s.show_city AS ShowCity,
		COUNT(ss.song_key) AS NumberOfSongsPlayed
	FROM ff_show s
	LEFT JOIN ff_show_song ss ON ss.show_key = s.show_key
	WHERE (YEAR(s.show_date) = @YearCode OR @YearCode IS NULL)
	AND (s.show_key = @ShowCode OR @ShowCode IS NULL)
	AND (ss.song_key = @SongCode OR @SongCode IS NULL)
	GROUP BY s.show_key, 
		s.show_date,
		s.show_location, 
		s.show_city
	ORDER BY s.show_date
END