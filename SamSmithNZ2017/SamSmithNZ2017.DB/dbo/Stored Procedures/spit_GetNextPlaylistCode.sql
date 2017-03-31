CREATE PROCEDURE [dbo].[spit_GetNextPlaylistCode]
	@playlist_code int
AS
SELECT isnull(min(playlist_code),-1)
FROM itPlaylist
WHERE playlist_code > @playlist_code