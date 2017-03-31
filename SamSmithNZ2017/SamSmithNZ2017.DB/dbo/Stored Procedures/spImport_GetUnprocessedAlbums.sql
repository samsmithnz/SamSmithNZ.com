CREATE PROCEDURE [dbo].[spImport_GetUnprocessedAlbums]
AS
SELECT 0
--SELECT *
--FROM tabalbum
--WHERE albumname not in (SELECT album_name FROM tab_album WHERE  is_bass_tab = 0)
--and IsABassTabAlbum = 0
----and albumkey in  (14,15,16,17)
----and artistname = 'james'

--UNION
--SELECT *
--FROM tabalbum
--WHERE albumname not in (SELECT album_name FROM tab_album WHERE  is_bass_tab = 1)
--and IsABassTabAlbum = 1
----and artistname = 'cure'

--ORDER BY artistname--albumkey