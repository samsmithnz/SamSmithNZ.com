CREATE PROCEDURE [dbo].[spTab_InsertAlbum]
	@AlbumName varchar(50), 
	@Path varchar(500), 
	@ArtistName varchar(50), 
	@AlbumYear int, 
	@IsABassTabAlbum bit, 
	@IsNewAlbum bit, 
	@IsUpdatedAlbum bit, 
	@IsAVariousSelection bit
AS
SELECT 0
--DECLARE @AlbumKey int
--SELECT @AlbumKey = max(AlbumKey) + 1 FROM TabAlbum
--INSERT INTO TabAlbum (AlbumKey, AlbumName, Path, ArtistName, AlbumYear, IsABassTabAlbum, IsNewAlbum, IsUpdatedAlbum, IsAVariousSelection, LastUpdated) 
--VALUES (@AlbumKey, @AlbumName, @Path, @ArtistName, @AlbumYear, @IsABassTabAlbum, @IsNewAlbum, @IsUpdatedAlbum, @IsAVariousSelection, GetDate()) 
--SELECT @AlbumKey