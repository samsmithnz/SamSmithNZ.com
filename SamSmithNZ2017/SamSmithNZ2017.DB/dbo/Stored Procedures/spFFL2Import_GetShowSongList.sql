CREATE PROCEDURE [dbo].spFFL2Import_GetShowSongList
AS
SELECT * 
FROM ffl_show_song
ORDER BY showtrack_key