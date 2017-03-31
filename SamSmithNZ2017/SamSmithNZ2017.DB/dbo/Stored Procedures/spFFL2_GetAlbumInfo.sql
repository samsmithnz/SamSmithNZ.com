CREATE PROCEDURE [dbo].[spFFL2_GetAlbumInfo]
	@album_key smallint
AS

SELECT a.album_key, a.album_name, a.album_release_date, 
	album_label, album_image
FROM ff_album a
WHERE a.album_key = @album_key