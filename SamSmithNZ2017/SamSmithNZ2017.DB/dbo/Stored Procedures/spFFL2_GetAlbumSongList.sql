CREATE PROCEDURE [dbo].[spFFL2_GetAlbumSongList]
	@album_key smallint
AS

SELECT s.song_order, s.song_key, s.song_name, 
	min(sh1.show_date) as first_played, min(sh1.show_key) as first_played_show_key, 
	max(sh1.show_date) as last_played, max(sh1.show_key) as last_played_show_key, 
	isnull(count(ss.song_key),0) as times_played, a.album_key, a.album_name
FROM ff_song s 
INNER JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ffl_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ffl_show sh1 ON sh1.show_key = ss.show_key
WHERE a.album_key = @album_key
GROUP BY s.song_order,s.song_name, a.album_name, --sh1.show_key, --sh2.show_key, 
s.song_key, a.album_key
ORDER BY s.song_order, s.song_name