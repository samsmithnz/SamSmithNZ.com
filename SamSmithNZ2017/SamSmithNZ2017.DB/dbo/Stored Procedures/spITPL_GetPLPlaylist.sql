CREATE  PROCEDURE [dbo].[spITPL_GetPLPlaylist]
	@playlist_code int
AS
SELECT 0
--SELECT dbo.fnIT_GetPlayListDate(max(it.playlist_code)) as playlist_date, 
--	dbo.fnIT_GetRanking(TrackName, max(it.playlist_code)) as ranking,
--	isnull(max(it.playlist_code),0) as playlist_code,
--	TrackKey, ta.artistname,TrackName, ta.path, TrackPath, TrackOrder, tt.AlbumKey, tt.rating, pt.track_order
--FROM TabTrack tt
--LEFT OUTER JOIN itTrack it ON tt.TrackName = it.track_name COLLATE database_default
--LEFT OUTER JOIN TabAlbum ta ON tt.albumkey = ta.albumkey
--INNER JOIN itPLTrack t ON tt.TrackName = t.track_name COLLATE database_default
--INNER JOIN itPLPlaylistTrack pt ON t.track_code = pt.track_code 
--WHERE pt.playlist_code = @playlist_code
--and ta.IsABassTabAlbum = 0
--GROUP BY trackKey, ta.artistname,TrackName, ta.path, TrackPath, TrackOrder, tt.AlbumKey, tt.rating, pt.track_order
--/*
--UNION 

--SELECT dbo.fnIT_GetPlayListDate(max(it.playlist_code)) as playlist_date, 
--	dbo.fnIT_GetRanking(TrackName, max(it.playlist_code)) as ranking,
--	isnull(max(it.playlist_code),0) as playlist_code,
--	TrackKey, ta.artistname, TrackName, '' as path, '' as TrackPath, TrackOrder, tt.AlbumKey, tt.rating, pt.track_order
--FROM TabTrack tt
--LEFT OUTER JOIN itTrack it ON tt.TrackName = it.track_name COLLATE database_default
--LEFT OUTER JOIN TabAlbum ta ON tt.albumkey = ta.albumkey
--INNER JOIN itPLTrack t ON tt.TrackName = t.track_name COLLATE database_default
--INNER JOIN itPLPlaylistTrack pt ON t.track_code = pt.track_code 
--WHERE pt.playlist_code = @playlist_code
--and ta.IsABassTabAlbum = 0
--and t.track_name not in (select tt.TrackName COLLATE database_default from TabTrack tt)
--GROUP BY trackKey, ta.artistname,TrackName, TrackOrder, tt.AlbumKey, tt.rating, pt.track_order
--*/
--ORDER BY pt.track_order