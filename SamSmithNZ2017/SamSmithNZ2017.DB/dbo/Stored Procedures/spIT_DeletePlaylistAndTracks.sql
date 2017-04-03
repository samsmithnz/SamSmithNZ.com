CREATE PROCEDURE [dbo].[spIT_DeletePlaylistAndTracks]
	@playlist_code int
AS
DELETE itTrack
WHERE playlist_code = @playlist_code

DELETE itPlaylist
WHERE playlist_code = @playlist_code