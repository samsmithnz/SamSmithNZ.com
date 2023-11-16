CREATE PROCEDURE [dbo].[ITunes_ImportValidateTracksForDuplicates]
	@PlaylistCode INT
AS
BEGIN
	--First get a list of all tracks that have to be updated 
	CREATE TABLE #tmpDuplicateTracks (artist_name VARCHAR(100), album_name VARCHAR(100), track_name VARCHAR(100), track_count INT, playlist_code INT)

	INSERT INTO #tmpDuplicateTracks
	SELECT artist_name, album_name, track_name, COUNT(*), playlist_code
	FROM itTrack
	WHERE playlist_code = @PlaylistCode
	GROUP BY artist_name, album_name, track_name, playlist_code
	HAVING COUNT(*) >= 2
	ORDER BY artist_name, album_name, track_name, playlist_code

	DECLARE @ArtistName VARCHAR(100)
	DECLARE @AlbumName VARCHAR(100)
	DECLARE @TrackName VARCHAR(100)

	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT DISTINCT artist_name, album_name, track_name
		FROM #tmpDuplicateTracks 

	OPEN Cursor1

	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @ArtistName, @AlbumName, @TrackName
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN

		DECLARE @TotalPlayCount INT
		DECLARE @TotalPreviousPlayCount INT
		DECLARE @RecordId UNIQUEIDENTIFIER
		DECLARE @HighestRating INT

		--Get the first record off the queue
		SELECT TOP 1 @RecordId = record_id 
		FROM itTrack t
		WHERE t.playlist_code = @PlaylistCode
		AND t.artist_name = @ArtistName 
		AND t.album_name = @AlbumName
		AND t.track_name = @TrackName
	
		--total the play counts AND total previous play counts from all tracks
		SELECT @TotalPlayCount = SUM(ISNULL(play_count,0)), @TotalPreviousPlayCount = SUM(ISNULL(previous_play_count,0))
		FROM itTrack t
		WHERE t.playlist_code = @PlaylistCode
		AND t.artist_name = @ArtistName 
		AND t.album_name = @AlbumName
		AND t.track_name = @TrackName

		--Get the highest Rating
		SELECT @HighestRating = MAX(ISNULL(rating,0))
		FROM itTrack t
		WHERE t.playlist_code = @PlaylistCode
		AND t.artist_name = @ArtistName 
		AND t.album_name = @AlbumName
		AND t.track_name = @TrackName
	
		--Delete all tracks but the 'chosen' track
		DELETE t 
		FROM itTrack t
		WHERE t.playlist_code = @PlaylistCode
		AND t.artist_name = @ArtistName 
		AND t.album_name = @AlbumName
		AND t.track_name = @TrackName
		AND t.record_id <> @RecordId

		--Update the 'chosen' track
		UPDATE itTrack
		SET play_count = @TotalPlayCount, previous_play_count = @TotalPreviousPlayCount, rating = @HighestRating
		WHERE record_id = @RecordId

		FETCH NEXT FROM Cursor1 INTO @ArtistName, @AlbumName, @TrackName
	END

	CLOSE Cursor1
	DEALLOCATE Cursor1

	SELECT artist_name AS ArtistName, 
		album_name AS AlbumName, 
		track_name AS TrackName, 
		track_count AS PlayCount, --Previously track count - this is the number of times this track appears - so it can be removed AS a duplicate 
		playlist_code AS PlaylistCode
	FROM #tmpDuplicateTracks
END