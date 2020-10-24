CREATE PROCEDURE [dbo].[Tab_SaveAlbum]
	@AlbumCode INT,
	@ArtistName VARCHAR(100),
	@AlbumName VARCHAR(100),
	@AlbumYear INT,
	@IsBassTab BIT,
	@IsNewAlbum BIT,
	@IsMiscCollectionAlbum BIT,
	@IncludeInIndex BIT,
	@IncludeOnWebsite BIT
AS
IF (@AlbumCode > 0)
BEGIN
	UPDATE tab_album
	SET artist_name = @ArtistName, album_name = @AlbumName, album_year = @AlbumYear, 
		is_bass_tab = @IsBassTab, is_new_album = @IsNewAlbum, is_misc_collection_album = @IsMiscCollectionAlbum, 
		include_in_index = @IncludeInIndex, include_on_website = @IncludeOnWebsite, last_updated = GETDATE()
	WHERE album_code = @AlbumCode

	SELECT @AlbumCode
END
ELSE
BEGIN
	SELECT @AlbumCode = MAX(album_code) + 1
	FROM tab_album
	
	INSERT INTO tab_album
	SELECT @AlbumCode, @ArtistName, @AlbumName, @AlbumYear, @IsBassTab, 
		@IsNewAlbum, @IsMiscCollectionAlbum, @IncludeInIndex,
		@IncludeOnWebsite,  GETDATE()

	SELECT @AlbumCode
END