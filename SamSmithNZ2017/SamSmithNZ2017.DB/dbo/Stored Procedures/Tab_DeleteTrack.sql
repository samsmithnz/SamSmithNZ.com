CREATE PROCEDURE [dbo].[Tab_DeleteTrack]
	@TrackCode INT
AS
DELETE FROM tab_track
WHERE track_code = @TrackCode