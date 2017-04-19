CREATE PROCEDURE [dbo].[spFFL2_GetShowSongs]
	@show_key smallint
AS
SET NOCOUNT ON

SELECT s.song_key, s.song_name, ss.show_song_order as song_order, s.song_notes
FROM ff_song s
JOIN ffl_show_song ss ON s.song_key = ss.song_key 
WHERE ss.show_key = @show_key
ORDER BY ss.show_song_order