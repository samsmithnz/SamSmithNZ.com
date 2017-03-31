CREATE PROCEDURE [dbo].[spFF_GetTrackList]
AS
SELECT 0
--SELECT a.album_name, t.track_order, t.track_name, '' as first_played, '' as last_played, 0 as times_played
--FROM ff_track t 
--INNER JOIN ff_album a ON t.album_key = a.album_key
--ORDER BY a.album_key, t.track_order