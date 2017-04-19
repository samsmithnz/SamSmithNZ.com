CREATE PROCEDURE [dbo].[spMusic_GetTrackListForTuning]
	@tuning_code smallint
AS

SELECT ta.artist_name, tt.*
FROM tab_track tt
JOIN tab_album ta ON tt.album_code = ta.album_code
WHERE tuning_code = @tuning_code
ORDER BY artist_name, album_year, track_order, track_name