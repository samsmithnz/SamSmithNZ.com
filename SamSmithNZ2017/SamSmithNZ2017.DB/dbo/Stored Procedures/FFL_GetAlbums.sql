CREATE PROCEDURE [dbo].[FFL_GetAlbums] 
	@album_key INT = NULL
AS
BEGIN
	SELECT a.album_key AS AlbumKey, 
		a.album_name AS AlbumName, 
		a.album_release_date AS AlbumReleaseDate, 
		a.album_label AS AlbumLabel, 
		a.album_image AS AlbumImage
	FROM ff_album a
	WHERE (a.album_key = @album_key OR @album_key IS NULL)
	ORDER BY CASE WHEN a.album_release_date IS NULL THEN GETDATE() ELSE a.album_release_date END, a.album_name
END