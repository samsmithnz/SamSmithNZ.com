CREATE PROCEDURE [dbo].[spIT_DeletePlaylistTracks]
	@playlist_code int
AS
DELETE ittrack
WHERE playlist_code = @playlist_code