CREATE FUNCTION [dbo].[fnIT_GetRanking] (@track_name varchar(50), @playlist_code smallint)
RETURNS int
BEGIN

DECLARE @ranking smallint

SELECT @ranking = ranking
FROM itTrack it
WHERE it.track_name = @track_name
and it.playlist_code = @playlist_code

RETURN isnull(@ranking,1000)
END