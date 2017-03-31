
CREATE PROCEDURE [dbo].[spGT_GetTracks]
	@album_code int = null,
	@track_code int = null
AS
SELECT track_code as TrackCode, 
	album_code as AlbumCode, 
	track_name as TrackName, 
	replace(tr.track_name,' ','') as TrackNameTrimed,
	track_text as TrackText, 
	track_order as TrackOrder, 
	rating as Rating, 
	tr.tuning_code as TuningCode, 
	CASE WHEN tr.tuning_code = 0 THEN '' ELSE tu.tuning_name END as TuningName, 
	tr.last_updated as LastUpdated
FROM tab_track tr
LEFT OUTER JOIN tab_tuning tu ON tr.tuning_code = tu.tuning_code
WHERE (@album_code is null or album_code = @album_code)
and (@track_code is null or track_code = @track_code)
ORDER BY track_order, track_name