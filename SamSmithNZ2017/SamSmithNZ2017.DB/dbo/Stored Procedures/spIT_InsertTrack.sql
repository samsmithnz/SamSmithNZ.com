CREATE PROCEDURE [dbo].[spIT_InsertTrack]
	@playlist_code int,
	@track_name varchar(75),
	@album_name varchar(50),
	@artist_name varchar(50),
	@play_count smallint,
	@ranking smallint,
	@rating smallint
AS
INSERT INTO itTrack
SELECT @playlist_code, @track_name, @album_name, @artist_name, @play_count, null, @ranking, null, 0, @rating, newid()