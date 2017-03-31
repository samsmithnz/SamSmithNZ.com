CREATE PROCEDURE [dbo].[spIT_DeletePlaylistAndTracks]
	@playlist_code int
AS
DELETE ittrack
WHERE playlist_code = @playlist_code

DELETE itplaylist
WHERE playlist_code = @playlist_code