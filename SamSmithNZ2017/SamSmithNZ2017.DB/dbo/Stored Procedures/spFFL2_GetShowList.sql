CREATE PROCEDURE [dbo].[spFFL2_GetShowList]
	@year_code smallint
AS

SET NOCOUNT ON

SELECT s.show_key, 
	s.show_date, 
	s.show_location,
	isnull(s.show_city + CASE WHEN not s.show_state is null and s.show_state <> '' THEN ', ' + s.show_state ELSE '' END,'') as show_city,
	CASE WHEN s.show_country = s.show_state THEN 'United States' ELSE s.show_country END as show_country,
	isnull((SELECT count(sr.show_key) FROM ffl_show_recording sr WHERE s.show_key = sr.show_key and sr.IsCirculatedRecording = 1),0) as number_of_recordings,
	isnull((SELECT count(sr2.show_key) FROM ffl_show_recording sr2 WHERE s.show_key = sr2.show_key and sr2.IsCirculatedRecording = 0),0) as number_of_recordings_unconfirmed,
	s.is_postponed_show, s.is_cancelled_show
FROM ffl_show s
WHERE year(s.show_date) = @year_code 
	or @year_code = 0
GROUP BY s.show_key, s.show_date,
	s.show_location, 
	isnull(s.show_city + 
		CASE WHEN not s.show_state is null and s.show_state <> '' THEN ', ' + s.show_state ELSE '' END,''),
	CASE WHEN s.show_country = s.show_state THEN 'United States' ELSE s.show_country END,
	s.is_postponed_show, s.is_cancelled_show
ORDER BY show_date