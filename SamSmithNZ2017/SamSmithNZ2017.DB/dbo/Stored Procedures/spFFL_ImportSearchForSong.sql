CREATE PROCEDURE [dbo].[spFFL_ImportSearchForSong]
	@song_name varchar(50)
AS

SELECT 0 as song_order, song_key, song_name 
FROM ff_song
WHERE song_name = @song_name