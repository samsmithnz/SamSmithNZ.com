CREATE PROCEDURE [dbo].[ITunes_GetTopArtists]
	@PlaylistCode INT = NULL,
	@ShowJustSummary BIT
AS
BEGIN
	/*
	DECLARE @count INT
	SELECT top 10 max COUNT(*)
	FROM itTrack t
	JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @PlaylistCode AND ranking <= 100 AND rating = 100
	GROUP BY artist_name
	--HAVING COUNT(*) >= 4
	ORDER BY COUNT(*) DESC, artist_name*/

	IF (@ShowJustSummary = 1)
	BEGIN
		SELECT artist_name AS ArtistName, 
			COUNT(*) AS ArtistCount
		FROM itTrack t
		JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL) 
			AND ranking <= 100 AND rating = 100
		GROUP BY artist_name
		HAVING COUNT(*) >= 3
		ORDER BY ArtistCount DESC, artist_name
	END
	ELSE
	BEGIN
		SELECT artist_name AS ArtistName,
			COUNT(*) AS ArtistCount
		FROM itTrack t
		JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL)
		GROUP BY artist_name
		HAVING COUNT(*) >= 10
		ORDER BY ArtistCount DESC, artist_name
	END
END