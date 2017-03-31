CREATE PROCEDURE [dbo].[spIT_CreateNewPlayList]
	@playlist_date datetime
AS
DECLARE @new_code smallint
SELECT @new_code = max(playlist_code) + 1
FROM itPlaylist
SELECT @new_code = isnull(@new_code,1)
INSERT INTO itPlaylist
SELECT @new_code, @playlist_date
SELECT @new_code