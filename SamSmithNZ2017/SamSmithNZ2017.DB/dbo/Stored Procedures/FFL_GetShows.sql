CREATE PROCEDURE [dbo].[FFL_GetShows]
	@ShowCode INT = NULL,
	@YearCode INT = NULL,
	@SongCode INT = NULL,
	@GetFFLCodes BIT = NULL
AS
BEGIN
	SELECT s.show_key AS ShowCode, 
		s.show_date AS ShowDate, 
		s.show_location AS ShowLocation,
		s.show_city AS ShowCity,
		s.show_country AS ShowCountry,
		s.ffl_code AS FFLCode,
		s.ffl_url AS FFLURL,
		s.notes AS Notes,
		s.last_updated AS LastUpdated,
		COUNT(ss.song_key) AS NumberOfSongsPlayed
	FROM ff_show s
	LEFT JOIN ff_show_song ss ON ss.show_key = s.show_key
	WHERE (YEAR(s.show_date) = @YearCode OR @YearCode IS NULL)
	AND (s.show_key = @ShowCode OR @ShowCode IS NULL)
	AND (ss.song_key = @SongCode OR @SongCode IS NULL)
	AND (NOT s.ffl_code IS NULL OR @GetFFLCodes IS NULL)
	GROUP BY s.show_key, 
		s.show_date,
		s.show_location, 
		s.show_city,
		s.show_country,
		s.ffl_code,
		s.ffl_url,
		s.notes,
		s.last_updated
	ORDER BY s.show_date
END