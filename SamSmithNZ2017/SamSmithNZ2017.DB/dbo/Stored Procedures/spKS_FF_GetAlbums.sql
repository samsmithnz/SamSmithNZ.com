CREATE PROCEDURE [dbo].[spKS_FF_GetAlbums] 
	@album_key smallint = null
AS
SELECT a.album_key, a.album_name, a.album_release_date, 
	album_label, album_image
FROM ff_album a
WHERE (a.album_key = @album_key or @album_key is null)
ORDER BY CASE WHEN album_release_date is null THEN getdate() ELSE album_release_date END, album_name