CREATE PROCEDURE [dbo].[spTab2_GetArtists]
AS
SELECT distinct artist_name, replace(artist_name,' ','') as artist_name_trimed
FROM tab_album
ORDER BY artist_name