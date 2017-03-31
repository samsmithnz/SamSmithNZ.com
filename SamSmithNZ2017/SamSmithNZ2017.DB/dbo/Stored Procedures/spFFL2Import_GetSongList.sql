CREATE PROCEDURE [dbo].spFFL2Import_GetSongList
AS
SELECT * 
FROM ff_song
ORDER BY song_key