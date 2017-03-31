CREATE PROCEDURE [dbo].[spGT_SaveAlbum]
	@album_code smallint,
	@artist_name varchar(100),
	@album_name varchar(100),
	@album_year smallint,
	@is_bass_tab bit,
	@is_new_album bit,
	@is_misc_collection_album bit,
	@include_in_index bit,
	@include_on_website bit
AS
IF (@album_code > 0)
BEGIN
	UPDATE tab_album
	SET artist_name = @artist_name, album_name = @album_name, album_year = @album_year, 
		is_bass_tab = @is_bass_tab, is_new_album = @is_new_album, is_misc_collection_album = @is_misc_collection_album, 
		include_in_index = @include_in_index, include_on_website = @include_on_website, last_updated = getdate()
	WHERE album_code = @album_code

	SELECT @album_code
END
ELSE
BEGIN
	SELECT @album_code = max(album_code) + 1
	FROM tab_album
	
	INSERT INTO tab_album
	SELECT @album_code, @artist_name, @album_name, @album_year, @is_bass_tab, 
		@is_new_album, @is_misc_collection_album, @include_in_index,
		@include_on_website,  getdate()

	SELECT @album_code
END