CREATE PROCEDURE [dbo].[Tab_GetAlbums]
	@AlbumCode INT = NULL,
	@IsAdmin BIT = NULL
AS
SELECT ta.album_code AS AlbumCode, 
	artist_name AS ArtistName, 
	replace(artist_name,' ','') AS ArtistNameTrimed,
	album_name AS AlbumName, 
	album_year AS AlbumYear, 
	is_bass_tab AS IsBassTab, 
	is_new_album AS IsNewAlbum, 
	is_misc_collection_album AS IsMiscCollectionAlbum, 
	include_in_index AS IncludeInIndex, 
	include_on_website AS IncludeOnWebsite,
	isnull(convert(INT,round(convert(DECIMAL(16,4),sum(tt.rating))/convert(DECIMAL(16,4),count(tt.rating)),0)),0) AS AverageRating
FROM tab_album ta
LEFT OUTER JOIN tab_track tt ON ta.album_code = tt.album_code AND tt.rating > 0
WHERE (@IsAdmin = 1 OR (@IsAdmin = 0 AND include_in_index = 1) OR (@IsAdmin IS NULL AND include_in_index = 1))
AND (@AlbumCode IS NULL OR ta.album_code = @AlbumCode)
GROUP BY artist_name, album_name, album_year, is_bass_tab, is_new_album, 
	is_misc_collection_album, include_in_index, include_on_website, ta.album_code
ORDER BY artist_name, album_year, album_name, is_bass_tab