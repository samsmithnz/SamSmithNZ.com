CREATE PROCEDURE [dbo].[spKS_Tab_GetTracks]
	@album_code smallint = null,
	@track_code smallint = null
AS
SELECT track_code, album_code, track_name, track_text, track_order, rating, 
	tr.tuning_code, CASE WHEN tr.tuning_code = 0 THEN '' ELSE tu.tuning_name END as tuning_name, 
	tr.last_updated
FROM tab_track tr
LEFT OUTER JOIN tab_tuning tu ON tr.tuning_code = tu.tuning_code
WHERE (@album_code is null or album_code = @album_code)
and (@track_code is null or track_code = @track_code)
ORDER BY track_order, track_name