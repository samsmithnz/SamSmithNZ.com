CREATE PROCEDURE [dbo].[spFF_GetSongIndexList]
AS
SELECT s.song_key, s.song_name, --sh1.show_key, --sh1.show_key as show_key1, 
	min(sh1.show_date) as first_played, 
		--sh2.show_key as show_key2, 
	max(sh2.show_date) as last_played, 
	count(ss.song_key) as times_played, 
	a.album_key, a.album_name
FROM ff_song s 
INNER JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ff_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ff_show sh1 ON sh1.show_key = ss.show_key 
LEFT OUTER JOIN ff_show sh2 ON sh2.show_key = ss.show_key
GROUP BY s.song_name, a.album_name, --sh1.show_key, --sh2.show_key, 
	s.song_key, a.album_key--, sh1.show_key--, sh1.show_date
ORDER BY count(ss.song_key) desc, 
s.song_name