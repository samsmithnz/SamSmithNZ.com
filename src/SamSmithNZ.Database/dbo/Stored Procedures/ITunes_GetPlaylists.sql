CREATE PROCEDURE [dbo].[ITunes_GetPlaylists]
	@PlaylistCode INT = NULL
AS
BEGIN
	SELECT p.playlist_code AS PlaylistCode, p.playlist_date AS PlaylistDate
	FROM itPlaylist p 
	WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL)
	ORDER BY playlist_date DESC
END
