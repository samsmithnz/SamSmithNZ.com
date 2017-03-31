CREATE PROCEDURE [dbo].[spFFL_ImportSearchForUnknownSong]
	@song_name varchar(50)
AS

DECLARE @song_key smallint

SELECT @song_key = song_key
FROM ffl_unknown_song
WHERE song_name = @song_name

IF (@song_key is null)
BEGIN
	SELECT @song_key = isnull((SELECT Max(song_key)+1 FROM ffl_unknown_song),1)

	INSERT INTO ffl_unknown_song
	SELECT @song_key, @song_name
END

SELECT 0 as song_order, song_key, song_name 
FROM ffl_unknown_song
WHERE song_key = @song_key