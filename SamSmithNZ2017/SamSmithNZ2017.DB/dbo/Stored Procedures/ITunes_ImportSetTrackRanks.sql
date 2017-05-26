CREATE PROCEDURE [dbo].[ITunes_ImportSetTrackRanks] -- [spIT_SetTrackRanks] 56
	@PlaylistCode INT
AS
BEGIN
	/*
	UPDATE itTrack
	SET ranking = 
	(
		SELECT sort_order
		FROM
		(
			SELECT --track_name,album_name,
				record_id, RANK() OVER (ORDER BY rating DESC, play_count DESC, track_name) AS sort_order
			FROM itTrack T2
			WHERE T2.playlist_code = 55
		) D
		WHERE D.record_id = itTrack.record_id
	)
	WHERE playlist_code = 55
	*/
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
	/*
	CREATE TABLE #tmp_itTrack ([playlist_code] [INT] NULL ,
		[track_name] [VARCHAR] (75) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
		[album_name] [VARCHAR] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
		[artist_name] [VARCHAR] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
		[play_count] [INT] NULL ,
		[previous_play_count] [INT] NULL ,
		[ranking] [INT] NULL ,
		[previous_ranking] [INT] NULL ,
		[is_new_entry] [bit] NULL ,
		[rating] [INT] NULL,
		[record_id] uniqueidentifier null)
	INSERT INTO #tmp_itTrack
	SELECT * 
	FROM itTrack
	WHERE playlist_code = @PlaylistCode

	DELETE FROM itTrack
	WHERE playlist_code = @PlaylistCode

	INSERT INTO itTrack
	SELECT @PlaylistCode, track_name, album_name, artist_name, sum(play_count),
		MAX(previous_play_count), MAX(ranking), MAX(previous_ranking), is_new_entry, rating, record_id
	FROM #tmp_itTrack
	WHERE playlist_code = @PlaylistCode
	GROUP BY track_name, album_name, artist_name, is_new_entry, rating

	DROP TABLE #tmp_itTrack*/

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
				record_id, RANK() OVER (ORDER BY rating DESC, play_count DESC, track_name) AS sort_order
			FROM itTrack T2
			WHERE T2.playlist_code = @PlaylistCode AND T2.rating = 100
		) D
		WHERE D.record_id = itTrack.record_id
	)
	WHERE playlist_code = @PlaylistCode
	/*
	UPDATE itTrack
	SET ranking = 
	(
		SELECT sort_order
		FROM
		(
			SELECT --track_name,album_name,
				record_id, RANK() OVER (ORDER BY rating DESC, play_count DESC, track_name) AS sort_order
			FROM itTrack T2
			WHERE T2.playlist_code = @PlaylistCode AND T2.rating < 100
		) D
		WHERE D.record_id = itTrack.record_id
	)
	WHERE playlist_code = @PlaylistCode*/

	/*
	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT track_name, album_name, artist_name, sum(play_count)
		FROM itTrack
		WHERE playlist_code = @PlaylistCode AND rating = 100
		GROUP BY track_name, album_name, artist_name
		ORDER BY sum(play_count) DESC, track_name
	OPEN Cursor1
	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name, @play_count
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		--SELECT @play_count, @current_play_count, @count
		IF (@play_count <> @current_play_count)
		BEGIN
			SET @current_play_count = @play_count
			UPDATE itTrack 
			SET ranking = @count
			WHERE playlist_code = @PlaylistCode
				AND track_name = @track_name
				AND album_name = @album_name
				AND artist_name = @artist_name
			SET @ranking_count = @count
		END
		ELSE
		BEGIN
			UPDATE itTrack 
			SET ranking = @ranking_count
			WHERE playlist_code = @PlaylistCode
				AND track_name = @track_name
				AND album_name = @album_name
				AND artist_name = @artist_name
		END
		SET @count = @count + 1
	
		FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name, @play_count
	END
	CLOSE Cursor1
	DEALLOCATE Cursor1*/

	--Now set the previous ranking (to generate those nice arrows)
	DECLARE @last_playlist_code INT
	SELECT @last_playlist_code = ISNULL(MAX(playlist_code),-1)
	FROM itPlaylist
	WHERE playlist_code < @PlaylistCode

	IF (@last_playlist_code = -1)
	BEGIN
		UPDATE itTrack
		SET previous_ranking = ranking, @previous_play_count = play_count
		WHERE playlist_code = @PlaylistCode
	END
	ELSE
	BEGIN
		UPDATE it
		SET previous_ranking = ISNULL(it2.ranking,0), previous_play_count = 0, is_new_entry = 1
		FROM itTrack it
		LEFT OUTER JOIN itTrack it2 ON it.track_name = it2.track_name AND it.album_name = it2.album_name AND it.artist_name = it2.artist_name
			AND it2.playlist_code = @last_playlist_code
			AND it2.previous_ranking IS NULL
		WHERE it.playlist_code = @PlaylistCode

		UPDATE it
		SET previous_ranking = ISNULL(it2.ranking,0), previous_play_count = ISNULL(it2.play_count,0), is_new_entry = 0
		FROM itTrack it
		JOIN itTrack it2 ON it.track_name = it2.track_name AND it.album_name = it2.album_name AND it.artist_name = it2.artist_name
		WHERE it.playlist_code = @PlaylistCode
			AND it2.playlist_code = @last_playlist_code
			AND NOT it2.previous_ranking IS NULL
	/*
		UPDATE itTrack 
		SET previous_ranking = @previous_ranking, previous_play_count = @previous_play_count, is_new_entry = 0
		WHERE playlist_code = @PlaylistCode
			AND track_name = @track_name
			AND album_name = @album_name
			AND artist_name = @artist_name*/
	/*
		DECLARE Cursor1 CURSOR LOCAL FOR
			SELECT track_name, album_name, artist_name
			FROM itTrack
			WHERE playlist_code = @PlaylistCode
	
		OPEN Cursor1
	
		--loop through all the items
		FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			SELECT @previous_ranking = null, @previous_play_count = null
			SELECT @previous_ranking = MAX(ranking), @previous_play_count = MAX(play_count)
			FROM itTrack
			WHERE playlist_code = @last_playlist_code
				AND track_name = @track_name
				AND album_name = @album_name
				AND artist_name = @artist_name
			IF (@previous_ranking IS NULL)
			BEGIN
				UPDATE itTrack
				SET previous_ranking = ranking, previous_play_count = 0, is_new_entry = 1
				WHERE playlist_code = @PlaylistCode
					AND track_name = @track_name
					AND album_name = @album_name
					AND artist_name = @artist_name
			END
			ELSE
			BEGIN
				UPDATE itTrack 
				SET previous_ranking = @previous_ranking, previous_play_count = @previous_play_count, is_new_entry = 0
				WHERE playlist_code = @PlaylistCode
					AND track_name = @track_name
					AND album_name = @album_name
					AND artist_name = @artist_name
			END
	
			FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name
		END
	
		CLOSE Cursor1
		DEALLOCATE Cursor1*/
	END
END
