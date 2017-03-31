CREATE PROCEDURE [dbo].[spMusic_GetTrackListForAlbum]
	@album_code smallint
AS

SELECT ta.artist_name, tt.*
FROM tab_track tt
INNER JOIN tab_album ta ON tt.album_code = ta.album_code
WHERE tt.album_code = @album_code
ORDER BY track_order