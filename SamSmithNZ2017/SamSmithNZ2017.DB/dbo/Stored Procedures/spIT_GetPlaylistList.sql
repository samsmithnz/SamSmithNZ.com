CREATE PROCEDURE [dbo].[spIT_GetPlaylistList]
AS
SELECT p.playlist_code, p.playlist_date, (sum(play_count) - sum(previous_play_count)) as songs_played, count(*) as total_songs
FROM itPlaylist p
JOIN itTrack t ON p.playlist_code = t.playlist_code
GROUP BY p.playlist_code, p.playlist_date
ORDER BY playlist_date DESC