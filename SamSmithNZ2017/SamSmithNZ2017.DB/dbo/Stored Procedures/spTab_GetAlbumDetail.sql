CREATE PROCEDURE [dbo].[spTab_GetAlbumDetail]
	@albumkey smallint
AS
SELECT *, (ArtistName + ' - ' + AlbumName) AS FullName 
FROM TabAlbum_old 
WHERE AlbumKey = @albumkey