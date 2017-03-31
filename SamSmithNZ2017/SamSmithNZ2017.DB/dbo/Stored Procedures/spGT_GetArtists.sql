CREATE PROCEDURE [dbo].[spGT_GetArtists]
	@include_in_index int = null,
	@artist_name varchar(100) = null
AS
SELECT distinct a.artist_name as ArtistName, replace(a.artist_name,' ','') as ArtistNameTrimed
FROM tab_album a
WHERE (@include_in_index is null or include_in_index = @include_in_index)
and (@artist_name is null or a.artist_name = @artist_name)
ORDER BY a.artist_name