CREATE PROCEDURE [dbo].[spFF_GetAlbumInfo]
	@album_key smallint
AS

SELECT a.album_key, a.album_name, isnull(a.album_release_date,'1900-01-01')album_release_date, 
	album_label, isnull(album_image,'') as album_image
FROM ff_album a
WHERE a.album_key = @album_key