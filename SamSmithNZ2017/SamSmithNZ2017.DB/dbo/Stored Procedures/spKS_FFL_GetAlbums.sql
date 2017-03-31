
CREATE PROCEDURE [dbo].[spKS_FFL_GetAlbums] 
	@album_key smallint = null
AS
SELECT a.album_key, a.album_name, a.album_release_date, 
	album_label, album_image
FROM ff_album a
WHERE a.album_key = @album_key
ORDER BY album_key