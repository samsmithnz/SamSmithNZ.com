CREATE PROCEDURE [dbo].[ITunes_ImportDeletePlaylistTracks]
	@PlaylistCode INT
AS
BEGIN
	DELETE itTrack
	WHERE playlist_code = @PlaylistCode
	
	DELETE itPlaylist
	WHERE playlist_code = @PlaylistCode
END