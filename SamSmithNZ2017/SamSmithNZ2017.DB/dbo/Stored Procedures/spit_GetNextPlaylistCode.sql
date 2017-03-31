CREATE PROCEDURE [dbo].[spit_GetNextPlaylistCode]
	@playlist_code int
AS
SELECT isnull(min(playlist_code),-1)
FROM itplaylist
WHERE playlist_code > @playlist_code