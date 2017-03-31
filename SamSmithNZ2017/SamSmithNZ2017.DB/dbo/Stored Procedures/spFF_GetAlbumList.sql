CREATE PROCEDURE [dbo].[spFF_GetAlbumList]
AS

SELECT album_key, album_name, isnull(album_release_date,getDate()) as album_release_date, album_label, album_image
FROM ff_album
ORDER BY album_release_date, album_name