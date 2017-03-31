CREATE PROCEDURE [dbo].[spKS_Tab_GetArtists]
	@include_in_index int = null
AS
SELECT distinct a.artist_name, replace(a.artist_name,' ','') as artist_name_trimed
FROM tab_album a
WHERE (include_in_index = @include_in_index
or @include_in_index is null)
ORDER BY a.artist_name