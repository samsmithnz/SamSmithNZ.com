CREATE PROCEDURE [dbo].[spit_GetLastPlaylistCode]
	@playlist_code int
AS
SELECT isnull(max(playlist_code),-1)
FROM itplaylist
WHERE playlist_code < @playlist_code