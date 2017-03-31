CREATE PROCEDURE [dbo].[spFF_GetSongList]
AS

SELECT * FROM ff_song
ORDER BY song_name