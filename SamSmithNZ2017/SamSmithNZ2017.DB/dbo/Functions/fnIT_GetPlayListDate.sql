CREATE FUNCTION [dbo].[fnIT_GetPlayListDate] (@playlist_code smallint)
RETURNS datetime
BEGIN

DECLARE @playlist_date datetime

SELECT @playlist_date = playlist_date
FROM itplaylist it
WHERE it.playlist_code = @playlist_code

RETURN @playlist_date
END