CREATE PROCEDURE [dbo].[ITunes_GetMovement]
	@PlaylistCode INT = NULL,
	@ShowJustSummary BIT
AS
BEGIN
	DECLARE @count INT

	IF (@ShowJustSummary = 1)
	BEGIN
		SELECT TOP 1 @count = play_count - previous_play_count
		FROM itTrack t
		JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL)
		--AND ranking <= 100 
		AND rating = 100
		ORDER BY play_count - previous_play_count DESC
	
		IF (@count > 0)
		BEGIN
			SELECT TOP 10 @count = play_count - previous_play_count
			FROM itTrack t
			JOIN itPlaylist p ON t.playlist_code = p.playlist_code
			WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL)
			--AND ranking <= 100 
			AND rating = 100
			ORDER BY play_count - previous_play_count DESC, artist_name + ' - ' + track_name
		
			SELECT DISTINCT artist_name + ' - ' + track_name AS TrackName, 
				play_count AS PlayCount, 
				play_count - previous_play_count AS ChangeThisMonth
				--0 AS ChangeThisMonth
			FROM itTrack t
			JOIN itPlaylist p ON t.playlist_code = p.playlist_code
			WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL)
			--AND ranking <= 100 
			AND rating = 100 AND play_count - previous_play_count >= @count
			ORDER BY ChangeThisMonth DESC, artist_name + ' - ' + track_name
		END
	END 
	ELSE
	BEGIN
		SELECT TOP 1 @count = play_count - previous_play_count
		FROM itTrack t
		JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL) 
		ORDER BY play_count - previous_play_count DESC
	
		IF (@count > 0)
		BEGIN
			SELECT TOP 10 @count = play_count - previous_play_count
			FROM itTrack t
			JOIN itPlaylist p ON t.playlist_code = p.playlist_code
			WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL) 
			ORDER BY play_count - previous_play_count DESC, artist_name + ' - ' + track_name

			SELECT artist_name + ' - ' + track_name AS TrackName, 
				play_count AS PlayCount, 
				play_count - previous_play_count AS ChangeThisMonth
			FROM itTrack t
			JOIN itPlaylist p ON t.playlist_code = p.playlist_code
			WHERE (p.playlist_code = @PlaylistCode OR @PlaylistCode IS NULL) 
			AND play_count - previous_play_count > = @count
			ORDER BY ChangeThisMonth DESC, artist_name + ' - ' + track_name
		END
	END
END