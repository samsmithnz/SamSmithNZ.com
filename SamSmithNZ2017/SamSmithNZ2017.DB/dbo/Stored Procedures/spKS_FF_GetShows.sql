CREATE PROCEDURE [dbo].[spKS_FF_GetShows]
	@show_key smallint = null,
	@year_code smallint = null,
	@song_key smallint = null
AS
  
SELECT s.show_key, s.show_date, 
	s.show_location,
	isnull(s.show_city + CASE WHEN not s.show_state is null and s.show_state <> '' THEN ', ' + s.show_state ELSE '' END,'') as show_city,
	CASE WHEN s.show_country = s.show_state THEN 'United States' ELSE s.show_country END as show_country,
	s.notes, other_performers, 
	isnull((SELECT count(sr.show_key) FROM ffl_show_recording sr WHERE s.show_key = sr.show_key and sr.IsCirculatedRecording = 1),0) as number_of_recordings,
	isnull((SELECT count(sr2.show_key) FROM ffl_show_recording sr2 WHERE s.show_key = sr2.show_key and sr2.IsCirculatedRecording = 0),0) as number_of_recordings_unconfirmed,
	s.is_postponed_show, s.is_cancelled_show, count(isnull(ss.song_key,0)) as songs_played
FROM ffl_show s
LEFT OUTER JOIN ffl_show_song ss ON ss.show_key = s.show_key
WHERE (year(s.show_date) = @year_code or @year_code is null)
and (s.show_key = @show_key or @show_key is null)
and (ss.song_key = @song_key or @song_key is null)
GROUP BY s.show_key, s.show_date,
	s.show_location, 
	isnull(s.show_city + CASE WHEN not s.show_state is null and s.show_state <> '' THEN ', ' + s.show_state ELSE '' END,''),
	CASE WHEN s.show_country = s.show_state THEN 'United States' ELSE s.show_country END,
	s.notes, other_performers, s.is_postponed_show, s.is_cancelled_show
ORDER BY show_date