CREATE PROCEDURE [dbo].[FFL_GetShows]
	@show_key INT = NULL,
	@year_code INT = NULL,
	@song_key INT = NULL
AS
BEGIN
	SELECT s.show_key AS ShowKey, 
		s.show_date AS ShowDate, 
		s.show_location AS ShowLocation,
		ISNULL(s.show_city + CASE WHEN NOT s.show_state IS NULL AND s.show_state <> '' THEN ', ' + s.show_state ELSE '' END,'') AS ShowCity,
		CASE WHEN s.show_country = s.show_state THEN 'United States' ELSE s.show_country END AS ShowCountry,
		s.notes AS Notes, 
		other_performers AS OtherPerformers, 
		ISNULL((SELECT COUNT(sr.show_key) FROM ffl_show_recording sr WHERE s.show_key = sr.show_key AND sr.IsCirculatedRecording = 1),0) AS NumberOfRecordings,
		ISNULL((SELECT COUNT(sr2.show_key) FROM ffl_show_recording sr2 WHERE s.show_key = sr2.show_key AND sr2.IsCirculatedRecording = 0),0) AS NumberOfRecordingsUnconfirmed,
		s.is_postponed_show AS IsPostponedShow, 
		s.is_cancelled_show AS IsCancelledShow, 
		COUNT(ISNULL(ss.song_key,0)) AS SongsPlayed
	FROM ffl_show s
	LEFT OUTER JOIN ffl_show_song ss ON ss.show_key = s.show_key
	WHERE (YEAR(s.show_date) = @year_code OR @year_code IS NULL)
	and (s.show_key = @show_key OR @show_key IS NULL)
	and (ss.song_key = @song_key OR @song_key IS NULL)
	GROUP BY s.show_key, s.show_date,
		s.show_location, 
		ISNULL(s.show_city + CASE WHEN NOT s.show_state IS NULL AND s.show_state <> '' THEN ', ' + s.show_state ELSE '' END,''),
		CASE WHEN s.show_country = s.show_state THEN 'United States' ELSE s.show_country END,
		s.notes, other_performers, s.is_postponed_show, s.is_cancelled_show
	ORDER BY show_date
END