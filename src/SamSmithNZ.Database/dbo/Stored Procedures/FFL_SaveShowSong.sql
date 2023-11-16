CREATE PROCEDURE [dbo].[FFL_SaveShowSong] 
	@SongCode INT,
	@ShowCode INT,
	@ShowSongOrder INT
AS
BEGIN
	
	INSERT INTO ff_show_song
	SELECT (SELECT MAX(showtrack_key)+1 FROM ff_show_song),
		@ShowCode,
		@SongCode,
		@ShowSongOrder

END