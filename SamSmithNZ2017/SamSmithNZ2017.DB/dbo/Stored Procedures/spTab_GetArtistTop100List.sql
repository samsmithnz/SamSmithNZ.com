CREATE PROCEDURE [dbo].[spTab_GetArtistTop100List]
AS
SELECT 'Top 100' as ArtistName, replace('Top 100',' ','') as artist_name_trimed
ORDER BY ArtistName