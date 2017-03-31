CREATE PROCEDURE [dbo].[Tab_GetArtists]
	@IncludeInIndex BIT = NULL
AS
SELECT DISTINCT a.artist_name AS ArtistName, 
	REPLACE(a.artist_name,' ','') AS ArtistNameTrimed
FROM tab_album a
WHERE (include_in_index = @IncludeInIndex OR @IncludeInIndex IS NULL)
ORDER BY a.artist_name