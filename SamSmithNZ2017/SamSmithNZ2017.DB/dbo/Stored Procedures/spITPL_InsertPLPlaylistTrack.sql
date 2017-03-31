CREATE PROCEDURE [dbo].[spITPL_InsertPLPlaylistTrack]
	@playlist_code int,
	@track_code int,
	@track_order int
AS
INSERT INTO itPLPlaylistTrack
SELECT @playlist_code, @track_code, @track_order