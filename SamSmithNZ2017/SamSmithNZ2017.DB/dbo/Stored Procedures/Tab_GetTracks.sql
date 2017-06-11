CREATE PROCEDURE [dbo].[Tab_GetTabs]
	@AlbumCode INT = NULL,
	@TabCode INT = NULL,
	@SortOrder INT = 0
AS
SELECT track_code AS TabCode, 
	album_code AS AlbumCode, 
	track_name AS TabName, 
	REPLACE(track_name,' ','') AS TabNameTrimed, 
	ISNULL(track_text,'') AS TabText, 
	track_order AS TabOrder, 
	rating AS Rating, 
	tr.tuning_code AS TuningCode, 
	CASE WHEN tr.tuning_code = 0 THEN '' ELSE tu.tuning_name END AS TuningName, 
	tr.last_updated AS LastUpdated
FROM tab_track tr
LEFT JOIN tab_tuning tu ON tr.tuning_code = tu.tuning_code
WHERE (@AlbumCode IS NULL OR album_code = @AlbumCode)
AND (@TabCode IS NULL OR track_code = @TabCode)
ORDER BY CASE WHEN @SortOrder = 0 OR tr.tuning_code = 0 THEN track_order ELSE tr.tuning_code END, track_name 