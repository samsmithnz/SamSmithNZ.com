CREATE PROCEDURE [dbo].[spFF_SaveSongLyrics]
	@song_key smallint,
	@song_lyrics varchar(8000)
AS

UPDATE ff_song
SET song_lyrics = @song_lyrics
WHERE song_key = @song_key