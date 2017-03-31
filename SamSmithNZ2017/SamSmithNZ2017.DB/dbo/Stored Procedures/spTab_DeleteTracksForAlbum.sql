CREATE PROCEDURE [dbo].[spTab_DeleteTracksForAlbum]
	@AlbumKey int
AS
SELECT 0	
--DELETE FROM TabTrack WHERE AlbumKey = @AlbumKey