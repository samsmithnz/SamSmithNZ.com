CREATE PROCEDURE [dbo].[spTab2_GetAlbums]
AS
SELECT ta.album_code, artist_name, replace(artist_name,' ','') as artist_name_trimed,
	album_name, album_year, is_bass_tab, is_new_album, is_misc_collection_album, include_in_index, include_on_website,
	isnull(convert(int,round(convert(decimal(16,4),sum(tt.rating))/convert(decimal(16,4),count(tt.rating)),0)),0) as average_rating
FROM tab_album ta
LEFT OUTER JOIN tab_track tt ON ta.album_code = tt.album_code and tt.rating > 0
WHERE include_in_index = 1
--where artist_name = 'Foo Fighters'
GROUP BY artist_name, album_name, album_year, is_bass_tab, is_new_album, 
	is_misc_collection_album, include_in_index, include_on_website, ta.album_code
ORDER BY artist_name, album_year, album_name, is_bass_tab