CREATE PROCEDURE [dbo].[ITunes_ImportCreateNewPlayList]
	@PlaylistDate DATETIME
AS
BEGIN
	DECLARE @NewPlaylistCode INT

	SELECT @NewPlaylistCode = MAX(playlist_code) + 1
	FROM itPlaylist
	SELECT @NewPlaylistCode = ISNULL(@NewPlaylistCode,1)

	INSERT INTO itPlaylist
	SELECT @NewPlaylistCode, @PlaylistDate

	SELECT @NewPlaylistCode AS PlayListCode
END