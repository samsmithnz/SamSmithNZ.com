CREATE PROCEDURE [dbo].[Tab_GetArtists]
	@IncludeInIndex BIT = NULL,
	@IsAdmin BIT = NULL
AS
SELECT DISTINCT a.artist_name AS ArtistName, 
	REPLACE(a.artist_name,' ','') AS ArtistNameTrimed
FROM tab_album a
WHERE ((a.include_in_index = 1 AND (@IncludeInIndex IS NULL OR @IncludeInIndex = 0))
OR (@IncludeInIndex = 1) OR (@IsAdmin = 1))
ORDER BY a.artist_name