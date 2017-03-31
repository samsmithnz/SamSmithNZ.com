
CREATE PROCEDURE [dbo].[spTab2_GetTrack]
	@track_code smallint
AS

SELECT track_name, track_text, rating, tuning_code, track_order
FROM tab_track
WHERE track_code = @track_code