
CREATE PROCEDURE [dbo].[spImport_ImportAlbum]
	@album_code smallint,
	@artist_name varchar(100),
	@album_name varchar(100),
	@album_year smallint, 
	@is_bass_tab bit, 
	@is_misc_collection_album bit, 
	@include_in_index bit, 
	@include_on_website bit
AS
SELECT 0
--INSERT INTO tab_album
--SELECT @album_code, @artist_name, @album_name, @album_year, 
--@is_bass_tab, 0, @is_misc_collection_album, 
--@include_in_index, @include_on_website, getdate()
--FROM tabalbum
--where albumkey =14