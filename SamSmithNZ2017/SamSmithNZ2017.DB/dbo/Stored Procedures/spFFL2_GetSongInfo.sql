CREATE PROCEDURE [dbo].[spFFL2_GetSongInfo]
	@song_key smallint
AS

SELECT s.song_key, s.song_name, 
	min(sh.show_date) as first_played, min(sh.show_key) as first_played_show_key,
	max(sh.show_date) as last_played, max(sh.show_key) as last_played_show_key,
	count(ss.song_key) as song_times_played, 
	a.album_key, a.album_name, 
	s.song_notes, s.song_lyrics, 
	s.song_order, s.song_image
FROM ff_song s
INNER JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ffl_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ffl_show sh ON sh.show_key = ss.show_key
--LEFT OUTER JOIN ffl_show_song ss2 ON ss2.song_key = s.song_key
--LEFT OUTER JOIN ffl_show sh2 ON sh2.show_key = ss2.show_key
WHERE s.song_key = @song_key
GROUP BY s.song_key, s.song_name, --sh.show_key, --sh2.show_key, 
	a.album_key, a.album_name, s.song_notes, s.song_lyrics, s.song_order, s.song_image