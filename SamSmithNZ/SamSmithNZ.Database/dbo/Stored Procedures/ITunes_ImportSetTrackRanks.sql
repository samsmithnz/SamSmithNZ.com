CREATE PROCEDURE [dbo].[ITunes_ImportSetTrackRanks] 
	@PlaylistCode INT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @track_name VARCHAR(100)
	DECLARE @album_name VARCHAR(100)
	DECLARE @artist_name VARCHAR(100)
	DECLARE @ranking INT
	DECLARE @count INT
	DECLARE @current_play_count INT
	DECLARE @ranking_count INT
	DECLARE @play_count INT
	DECLARE @previous_ranking INT
	DECLARE @previous_play_count INT
	SET @count = 1
	SET @ranking_count = 1
	SET @current_play_count = 0
	SET @play_count = 1

	--Clean up tracks that have never been played. I don't care about these at all.
	DELETE FROM itTrack
	WHERE play_count = 0

	--Create an overall ranking. 10,000 basically means, "I haven't been ranked yet"
	UPDATE itTrack
	SET ranking = 10000
	WHERE playlist_code = @PlaylistCode

	UPDATE itTrack
	SET ranking = 
	(
		SELECT sort_order
		FROM
		(
			SELECT --track_name,album_name,
				record_id, 
				RANK() OVER (ORDER BY rating DESC, play_count DESC, track_name) AS sort_order
			FROM itTrack T2
			WHERE T2.playlist_code = @PlaylistCode AND T2.rating = 100
		) D
		WHERE D.record_id = itTrack.record_id
	)
	WHERE playlist_code = @PlaylistCode

	--Now set the previous ranking (to generate those nice arrows)
	DECLARE @last_playlist_code INT
	SELECT @last_playlist_code = ISNULL(MAX(playlist_code),-1)
	FROM itPlaylist
	WHERE playlist_code < @PlaylistCode

	--Fix any Album name issues
	UPDATE it
	SET album_name = ''
	FROM itTrack it
	WHERE playlist_code = @PlaylistCode
	AND album_name IS NULL

	IF (@last_playlist_code = -1)
	BEGIN
		UPDATE itTrack
		SET previous_ranking = ranking, @previous_play_count = play_count
		WHERE playlist_code = @PlaylistCode
	END
	ELSE
	BEGIN
		--tracks with the ranking and play counts from the previous playlist
		UPDATE it
		SET previous_ranking = ISNULL(it2.ranking,0), previous_play_count = ISNULL(it2.play_count,0), is_new_entry = 0
		FROM itTrack it
		JOIN itTrack it2 ON it.track_name = it2.track_name AND it.album_name = it2.album_name AND it.artist_name = it2.artist_name
		WHERE it.playlist_code = @PlaylistCode
			AND it2.playlist_code = @last_playlist_code
			AND NOT it2.previous_ranking IS NULL

		--Update all tracks that weren't present in the previous play list
		UPDATE it
		SET previous_ranking = 0, 
			previous_play_count = 0, 
			is_new_entry = 1
		FROM itTrack it
		WHERE it.playlist_code = @PlaylistCode
		AND previous_ranking IS NULL
		AND previous_play_count IS NULL
	END
END

GO