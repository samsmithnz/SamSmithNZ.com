CREATE PROCEDURE [dbo].[Tab_GetTabs]
	@AlbumCode INT = NULL,
	@TabCode INT = NULL
AS
SELECT track_code AS TabCode, 
	album_code AS AlbumCode, 
	track_name AS TabName, 
	REPLACE(track_name,' ','') AS TabNameTrimed, 
	ISNULL(track_text,'') AS TabText, 
	track_order AS TabOrder, 
	rating AS Rating, 
	tr.tuning_code AS TuningCode, 
	CASE WHEN tr.tuning_code = 0 THEN '' ELSE tu.tuning_name END as TuningName, 
	tr.last_updated AS LastUpdated
FROM tab_track tr
LEFT OUTER JOIN tab_tuning tu ON tr.tuning_code = tu.tuning_code
WHERE (@AlbumCode IS NULL OR album_code = @AlbumCode)
AND (@TabCode IS NULL OR track_code = @TabCode)
ORDER BY track_order, track_name