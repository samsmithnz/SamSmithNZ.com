CREATE PROCEDURE [dbo].[spMusic_GetArtistNames]
AS

SELECT distinct artist_name, replace(replace(artist_name,' ',''),'''','') as artist_name_no_spaces
FROM tab_album
ORDER BY artist_name