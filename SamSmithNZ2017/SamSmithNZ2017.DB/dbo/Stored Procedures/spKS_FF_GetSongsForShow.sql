CREATE PROCEDURE [dbo].[spKS_FF_GetSongsForShow] 
	@show_key smallint
AS

SELECT s.song_key, s.song_name,  
	s.song_notes, s.song_lyrics, s.song_image,
	min(sh.show_date) as first_played, min(sh.show_key) as first_played_show_key,
	max(sh.show_date) as last_played, max(sh.show_key) as last_played_show_key,
	isnull(count(ss.song_key),0) as times_played, 
	a.album_key, a.album_name, ss.show_song_order as song_order
FROM ff_song s
JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ffl_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ffl_show sh ON sh.show_key = ss.show_key
WHERE sh.show_key = @show_key
GROUP BY s.song_key, s.song_name, 
	s.song_notes, s.song_lyrics, s.song_image, 
	a.album_key, a.album_name, ss.show_song_order
ORDER BY ss.show_song_order