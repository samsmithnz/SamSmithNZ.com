CREATE PROCEDURE [dbo].[spFFL_ImportSubmitSetlistSong]
	@show_key smallint,
	@song_order smallint,
	@song_key smallint,
	@unknown_song_key smallint,
	@is_partial bit,
	@is_jam bit,
	@song_notes varchar(50)
AS

DELETE FROM ffl_show_song
WHERE show_key = @show_key and show_song_order = @song_order

INSERT INTO ffl_show_song
SELECT isnull((SELECT max(showtrack_key) + 1 FROM ffl_show_song),1), 
	@show_key, @song_key, @unknown_song_key, @song_order,
	@is_partial, @is_jam, @song_notes