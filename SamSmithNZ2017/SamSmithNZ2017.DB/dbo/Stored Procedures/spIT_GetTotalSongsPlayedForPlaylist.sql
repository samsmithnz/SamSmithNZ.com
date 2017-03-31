CREATE PROCEDURE [dbo].[spIT_GetTotalSongsPlayedForPlaylist]
	@playlist_code int
AS

SELECT sum(play_count) as total, sum(previous_play_count) as last_total, (sum(play_count) - sum(previous_play_count)) as change
FROM ittrack
WHERE playlist_code = @playlist_code