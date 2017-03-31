
CREATE PROCEDURE [dbo].spFFL2Import_GetAlbumList
AS
SELECT * 
FROM ff_album
ORDER BY album_key