CREATE PROCEDURE [dbo].[spFF_UpdateSong]
	@song_key smallint,
	@song_name varchar(100),
	@album_key smallint,
	@song_order smallint,
	@song_image varchar(200),
	@song_notes varchar(2000),
	@song_lyrics varchar(8000)
AS

UPDATE ff_song
SET song_name = @song_name, album_key = @album_key, song_order = @song_order, song_notes = @song_notes, song_image = @song_image, song_lyrics = @song_lyrics
WHERE song_key = @song_key