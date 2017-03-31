CREATE PROCEDURE [dbo].[spIT_DeletePlaylistTracks]
	@playlist_code int
AS
DELETE itTrack
WHERE playlist_code = @playlist_code