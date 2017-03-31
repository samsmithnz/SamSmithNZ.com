CREATE PROCEDURE [dbo].[spTab_UpdateAlbumFlags]
	@AlbumKey int,
	@IsNewAlbum bit,
	@IsUpdatedAlbum bit
AS
SELECT 0
--UPDATE TabAlbum SET IsNewAlbum = @IsNewAlbum, IsUpdatedAlbum = @IsUpdatedAlbum WHERE AlbumKey = @AlbumKey