CREATE PROCEDURE [dbo].[spGT_GetAlbums]
	@album_code smallint = null,
	@is_admin bit = null
AS

IF (@is_admin is null)
BEGIN
	SELECT @is_admin = 0
END

SELECT ta.album_code as AlbumCode, 
	ta.artist_name as ArtistName, 
	replace(ta.artist_name,' ','') as ArtistNameTrimed,
	ta.album_name as AlbumName, 
	ta.album_year as AlbumYear, 
	ta.is_bass_tab as IsBassTab, 
	ta.is_new_album as IsNewAlbum, 
	ta.is_misc_collection_album as IsMiscCollectionAlbum, 
	ta.include_in_index as IncludeInIndex, 
	ta.include_on_website as IncludeOnWebsite,
	isnull(convert(int,round(convert(decimal(16,4),sum(tt.rating))/convert(decimal(16,4),count(tt.rating)),0)),0) as AverageRating--,
	--tab.album_code as BassAlbumCode,
	--'(Bass)' as BassAlbumName
FROM tab_album ta
LEFT OUTER JOIN tab_track tt ON ta.album_code = tt.album_code and tt.rating > 0
--LEFT OUTER JOIN tab_album tab ON ta.artist_name = tab.artist_name and ta.album_name = tab.album_name and ta.is_bass_tab = 0 and tab.is_bass_tab = 1
WHERE (@is_admin = 1 or (@is_admin = 0 and ta.include_in_index = 1))
and (@album_code is null or ta.album_code = @album_code)
and ta.is_bass_tab = 0
GROUP BY ta.artist_name, ta.album_name, ta.album_year, ta.is_bass_tab, ta.is_new_album, 
	ta.is_misc_collection_album, ta.include_in_index, ta.include_on_website, ta.album_code--,
	--tab.album_code, tab.album_name
ORDER BY ta.artist_name, ta.album_year, ta.album_name, ta.is_bass_tab