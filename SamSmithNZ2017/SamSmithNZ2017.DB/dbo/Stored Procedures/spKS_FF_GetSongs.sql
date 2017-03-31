CREATE PROCEDURE [dbo].[spKS_FF_GetSongs] 
	@song_key smallint = null,
	@album_key smallint = null
AS

SELECT s.song_key, s.song_name,  
	s.song_notes, s.song_lyrics, s.song_image,
	min(sh.show_date) as first_played, min(sh.show_key) as first_played_show_key,
	max(sh.show_date) as last_played, max(sh.show_key) as last_played_show_key,
	isnull(count(ss.song_key),0) as times_played, 
	a.album_key, a.album_name, s.song_order as song_order
FROM ff_song s
INNER JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ffl_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ffl_show sh ON sh.show_key = ss.show_key
WHERE (s.song_key = @song_key or @song_key is null)
and (a.album_key = @album_key or @album_key is null)
GROUP BY s.song_key, s.song_name, 
	s.song_notes, s.song_lyrics, s.song_image,
	a.album_key, a.album_name, s.song_order
ORDER BY CASE WHEN @song_key is null and @album_key is null THEN isnull(count(ss.song_key),0) ELSE 1 END DESC,
	CASE WHEN not @song_key is null or not @album_key is null THEN s.song_order END, s.song_name