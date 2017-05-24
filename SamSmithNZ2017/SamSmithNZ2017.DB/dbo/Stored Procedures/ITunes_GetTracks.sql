CREATE PROCEDURE [dbo].[ITunes_GetTracks]
	@playlist_code INT,
	@show_just_summary BIT,
	@track_name VARCHAR(50) = NULL
AS
BEGIN
	IF (@show_just_summary = 1)
	BEGIN
		SELECT t.album_name AS AlbumName,
		t.artist_name AS ArtistName,
		t.is_new_entry AS IsNewEntry,
		t.play_count AS PlayCount,
		t.playlist_code AS PlaylistCode,
		t.previous_play_count AS PreviousPlayCount,
		t.previous_ranking AS PreviousRanking,
		t.ranking AS Ranking,
		t.rating AS Rating,
		t.record_id AS RecordId,
		t.track_name AS TrackName
		FROM itTrack t
		JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE p.playlist_code = @playlist_code AND ranking <= 100 AND rating = 100
		AND (t.track_name = @track_name OR @track_name IS NULL)
		ORDER BY ranking, track_name
	END
	ELSE
	BEGIN	
		SELECT t.album_name AS AlbumName,
		t.artist_name AS ArtistName,
		t.is_new_entry AS IsNewEntry,
		t.play_count AS PlayCount,
		t.playlist_code AS PlaylistCode,
		t.previous_play_count AS PreviousPlayCount,
		t.previous_ranking AS PreviousRanking,
		t.ranking AS Ranking,
		t.rating AS Rating,
		t.record_id AS RecordId,
		t.track_name AS TrackName
		FROM itTrack t
		JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE p.playlist_code = @playlist_code
		AND (t.track_name = @track_name OR @track_name IS NULL)
		ORDER BY ranking, track_name
	END
END
