CREATE PROCEDURE [dbo].[Tab_GetSearchResults]
	@RecordId UNIQUEIDENTIFIER 
AS

DECLARE @SearchText VARCHAR(100)

SELECT @SearchText = search_text
FROM tab_search_parameters tsp
WHERE record_id = @RecordId

SELECT @SearchText AS SearchText, 
	ta.album_code AS AlbumCode, 
	artist_name + ' - ' + album_name AS ArtistAlbumResult, 
	CONVERT(VARCHAR(10),track_order) + '. ' + track_name AS TrackResult,
	track_name AS TrackName,
	is_bass_tab AS IsBassTab
FROM tab_album ta
LEFT JOIN tab_track tt ON ta.album_code = tt.album_code
WHERE include_in_index = 1
AND ((artist_name + ' - ' + album_name LIKE '%' + @SearchText + '%') 
OR (track_name LIKE '%' + @SearchText + '%'))
ORDER BY artist_name + ' - ' + album_name, 
	CONVERT(VARCHAR(10),track_order) + '. ' + track_name, 
	is_bass_tab