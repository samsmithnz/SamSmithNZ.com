CREATE PROCEDURE [dbo].[spit_GetArtistCounts]
	@playlist_code smallint,
	@show_just_summary bit
AS
/*
DECLARE @count smallint
SELECT top 10 max count(*)
FROM itTrack t
INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
WHERE p.playlist_code = @playlist_code and ranking <= 100 and rating = 100
GROUP BY artist_name
--HAVING count(*) >= 4
ORDER BY count(*) DESC, artist_name*/

IF (@show_just_summary = 1)
BEGIN
	SELECT artist_name, count(*) as artist_count
	FROM itTrack t
	INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code and ranking <= 100 and rating = 100
	GROUP BY artist_name
	HAVING count(*) >= 3
	ORDER BY artist_count DESC, artist_name
END
ELSE
BEGIN
	SELECT artist_name, count(*) as artist_count
	FROM itTrack t
	INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code
	GROUP BY artist_name
	HAVING count(*) >= 10
	ORDER BY artist_count DESC, artist_name
END