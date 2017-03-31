CREATE PROCEDURE [dbo].[spFF_GetShowSongs]
	@show_key smallint
AS

SELECT ss.show_song_order, s.song_key, s.song_name, a.album_key, a.album_name
FROM ff_song s 
INNER JOIN ff_album a ON s.album_key = a.album_key
LEFT OUTER JOIN ff_show_song ss ON ss.song_key = s.song_key
LEFT OUTER JOIN ff_show sh ON sh.show_key = ss.show_key
WHERE sh.show_key = @show_key
ORDER BY ss.show_song_order, s.song_name