CREATE  PROCEDURE [dbo].[spTab_GetTracksForAlbum]
	@AlbumKey int
AS
SELECT 0
--SELECT dbo.fnIT_GetPlayListDate(max(playlist_code)) as playlist_date, dbo.fnIT_GetRanking(TrackName, max(playlist_code)) as ranking, 
--isnull(max(playlist_code),0) as playlist_code,
--TrackKey, TrackName, TrackPath, TrackOrder, AlbumKey, tt.rating
--FROM TabTrack tt
--LEFT OUTER JOIN itTrack it ON tt.TrackName = it.track_name COLLATE database_default
--WHERE AlbumKey = @AlbumKey 
--GROUP BY trackKey, TrackName, TrackPath, TrackOrder, AlbumKey, tt.rating
--ORDER BY TrackOrder