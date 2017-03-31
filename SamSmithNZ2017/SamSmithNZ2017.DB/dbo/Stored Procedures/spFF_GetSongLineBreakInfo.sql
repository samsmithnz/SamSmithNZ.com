CREATE PROCEDURE [dbo].[spFF_GetSongLineBreakInfo]
AS

SELECT song_key, song_lyrics
FROM ff_song
WHERE song_lyrics is not null