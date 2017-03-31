CREATE  PROCEDURE [dbo].[spGT_GetSearchResults]
	@record_id uniqueidentifier
AS

DECLARE @search_text varchar(100)

SELECT @search_text = search_text
FROM tab_search_parameters tsp
WHERE record_id = @record_id

SELECT @search_text as SearchText, 
	ta.album_code as AlbumCode, 
	artist_name + ' - ' + album_name as ArtistAlbumResult, 
	CONVERT(varchar(10),track_order) + '. ' + track_name as TrackResult,
	track_name as TrackName, 
	replace(track_name,' ','') as TrackNameTrimed,
	is_bass_tab as IsBassTab
FROM tab_album ta
LEFT OUTER JOIN tab_track tt ON ta.album_code = tt.album_code
WHERE include_in_index = 1
and ((artist_name + ' - ' + album_name like '%' + @search_text + '%') 
or (track_name like '%' + @search_text + '%'))
and is_bass_tab = 0
ORDER BY artist_name + ' - ' + album_name, 
	CONVERT(varchar(10),track_order) + '. ' + track_name, 
	is_bass_tab