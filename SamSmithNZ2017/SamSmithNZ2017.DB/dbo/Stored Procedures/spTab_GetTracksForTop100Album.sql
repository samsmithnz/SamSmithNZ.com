CREATE PROCEDURE [dbo].[spTab_GetTracksForTop100Album]
	@AlbumKey int
AS
SELECT 0

--DECLARE @minranking smallint
--DECLARE @maxranking smallint
--DECLARE @playlist_code smallint

--SELECT @playlist_code = max(playlist_code)
--FROM itPlaylist

--SELECT @minranking = ((@albumkey - 1) * 10) + 1
--SELECT @maxranking = ((@albumkey - 1) * 10) + 10

--SELECT ranking, ta.artistname, tt.trackname, ta.path, tt.trackpath, tt.rating
--FROM ittrack it 
--LEFT OUTER JOIN TabTrack tt ON tt.TrackName = it.track_name COLLATE database_default
--LEFT OUTER JOIN TabAlbum ta ON tt.albumkey = ta.albumkey
--WHERE playlist_code = @playlist_code and IsABassTabAlbum = 0
--and it.ranking >= @minranking and it.ranking <= @maxranking

--UNION 

--SELECT ranking, '' as artistname, it.track_name COLLATE database_default as trackname, '' as path, '' as trackpath, 0 as rating
--FROM ittrack it 
--WHERE playlist_code = @playlist_code
--and it.ranking >= @minranking and it.ranking <= @maxranking
--and it.track_name not in (select tt.TrackName COLLATE database_default from TabTrack tt)

--ORDER BY ranking