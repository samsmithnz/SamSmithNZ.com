CREATE PROCEDURE [dbo].[spITPL_InsertTrack]
	@track_code int,
	@track_name varchar(250),
	@album_name varchar(250),
	@artist_name varchar(250)
AS
INSERT INTO itPLTrack
SELECT @track_code, @track_name, @artist_name, @album_name