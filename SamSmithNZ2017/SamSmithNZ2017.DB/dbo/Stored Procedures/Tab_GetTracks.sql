CREATE PROCEDURE [dbo].[Tab_GetTracks]
	@AlbumCode INT = NULL,
	@TrackCode INT = NULL
AS
SELECT track_code AS TrackCode, 
	album_code AS AlbumCode, 
	track_name AS TrackName, 
	track_text AS TrackText, 
	track_order AS TrackOrder, 
	rating AS Rating, 
	tr.tuning_code AS TuningCode, 
	CASE WHEN tr.tuning_code = 0 THEN '' ELSE tu.tuning_name END as TuningName, 
	tr.last_updated AS LastUpdated
FROM tab_track tr
LEFT OUTER JOIN tab_tuning tu ON tr.tuning_code = tu.tuning_code
WHERE (@AlbumCode IS NULL OR album_code = @AlbumCode)
AND (@TrackCode IS NULL OR track_code = @TrackCode)
ORDER BY track_order, track_name