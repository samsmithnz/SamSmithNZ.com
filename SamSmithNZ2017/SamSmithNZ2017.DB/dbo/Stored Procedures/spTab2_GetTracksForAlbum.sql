CREATE PROCEDURE [dbo].[spTab2_GetTracksForAlbum]
	@album_code smallint
AS
SELECT track_code, album_code, track_name, track_text, track_order, rating, tuning_code, last_updated
FROM tab_track
WHERE album_code = @album_code 
ORDER BY track_order, track_name