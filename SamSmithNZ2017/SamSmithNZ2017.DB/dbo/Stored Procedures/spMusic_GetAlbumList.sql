CREATE PROCEDURE [dbo].[spMusic_GetAlbumList]
AS

SELECT *
FROM tab_album
ORDER BY artist_name, album_year, is_bass_tab