CREATE PROCEDURE [dbo].[spit_GetPlaylistTrackCount]
	@playlist_code smallint
AS

SELECT count(*) as track_count
FROM ittrack
WHERE playlist_code = @playlist_code