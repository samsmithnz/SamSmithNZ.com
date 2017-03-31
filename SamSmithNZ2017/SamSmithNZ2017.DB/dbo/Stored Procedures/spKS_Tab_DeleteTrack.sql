CREATE PROCEDURE [dbo].[spKS_Tab_DeleteTrack]
	@track_code smallint
AS
DELETE FROM tab_track
WHERE track_code = @track_code