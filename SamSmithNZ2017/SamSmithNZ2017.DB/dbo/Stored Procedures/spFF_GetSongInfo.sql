CREATE PROCEDURE [dbo].[spFF_GetSongInfo]
	@song_key smallint
AS

SELECT s.song_key, s.song_name, min(sh.show_date) as first_played, max(sh.show_date) as last_played, count(ss.song_key) as song_times_played, a.album_key, a.album_name, s.song_notes, s.song_lyrics, s.song_order, s.song_image
FROM ff_song s
JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ff_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ff_show sh ON sh.show_key = ss.show_key
WHERE s.song_key = @song_key
GROUP BY s.song_key, s.song_name, a.album_key, a.album_name, s.song_notes, s.song_lyrics, s.song_order, s.song_image