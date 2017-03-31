CREATE PROCEDURE [dbo].[spImport_GetAlbumTracks]
	@album_code smallint
AS
SELECT 0
--SELECT * FROM tabtrack
--where albumkey = @album_code