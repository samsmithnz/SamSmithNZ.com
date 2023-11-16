CREATE PROCEDURE [dbo].[FFL_GetAlbums] 
	@AlbumCode INT = NULL
AS
BEGIN
	SELECT a.album_key AS AlbumCode, 
		a.album_name AS AlbumName, 
		a.album_release_date AS AlbumReleaseDate, 
		--a.album_label AS AlbumLabel, 
		a.album_image AS AlbumImage
	FROM ff_album a
	WHERE (a.album_key = @AlbumCode OR @AlbumCode IS NULL)
	ORDER BY CASE WHEN a.album_release_date IS NULL THEN GETDATE() ELSE a.album_release_date END, a.album_name
END