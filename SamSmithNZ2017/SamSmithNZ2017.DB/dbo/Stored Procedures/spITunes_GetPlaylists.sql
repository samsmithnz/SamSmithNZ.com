CREATE PROCEDURE [dbo].[spITunes_GetPlaylists]
	@playlist_code int = null
AS

SELECT *
FROM itPlaylist p 
WHERE (p.playlist_code = @playlist_code or @playlist_code is null)
ORDER BY playlist_date desc