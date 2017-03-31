cREATE PROCEDURE [dbo].[spFF_ImportSubmitSetlistSong]
	@show_key smallint,
	@song_order smallint,
	@song_key smallint
AS

DELETE FROM ff_show_song
WHERE show_key = @show_key and show_song_order = @song_order

INSERT INTO ff_show_song
SELECT (SELECT max(showtrack_key) + 1 FROM ff_show_song), @show_key, @song_key, @song_order