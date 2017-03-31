CREATE PROCEDURE [dbo].[spTab_UpdateAlbumLastUpdated]
	@AlbumKey int,
	@LastUpdated datetime
AS
SELECT 0
--UPDATE TabAlbum SET LastUpdated = @LastUpdated 
--WHERE AlbumKey = @AlbumKey