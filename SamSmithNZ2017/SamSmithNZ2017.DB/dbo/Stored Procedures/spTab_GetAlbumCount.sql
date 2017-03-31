CREATE PROCEDURE [dbo].[spTab_GetAlbumCount]
	@get_bass_albums bit
AS
SELECT 0
--SELECT count(*) as AlbumCount FROM TabAlbum WHERE IsABassTabAlbum = @get_bass_albums