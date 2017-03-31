CREATE PROCEDURE [dbo].[spITPL_CreateNewPLPlaylist]
	@playlist_code int,
	@playlist_name varchar(250)
AS
INSERT INTO itPLPlaylist
SELECT @playlist_code, @playlist_name, 0