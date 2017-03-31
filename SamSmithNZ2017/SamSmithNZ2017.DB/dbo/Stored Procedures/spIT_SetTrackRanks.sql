CREATE PROCEDURE [dbo].[spIT_SetTrackRanks] -- [spIT_SetTrackRanks] 56
	@playlist_code int
AS
/*
UPDATE itTrack
SET ranking = 
(
	SELECT sort_order
	FROM
	(
		SELECT --track_name,album_name,
			record_id, RANK() OVER (ORDER BY rating desc, play_count desc, track_name) as sort_order
		FROM itTrack T2
		WHERE T2.playlist_code = 55
	) D
	WHERE D.record_id = itTrack.record_id
)
WHERE playlist_code = 55
*/
SET NOCOUNT ON
DECLARE @track_name varchar(100)
DECLARE @album_name varchar(100)
DECLARE @artist_name varchar(100)
DECLARE @ranking smallint
DECLARE @count smallint
DECLARE @current_play_count smallint
DECLARE @ranking_count smallint
DECLARE @play_count smallint
DECLARE @previous_ranking smallint
DECLARE @previous_play_count smallint
SET @count = 1
SET @ranking_count = 1
SET @current_play_count = 0
SET @play_count = 1

--Clean up tracks that have never been played. I don't care about these at all.
DELETE FROM itTrack
WHERE play_count = 0
/*
CREATE TABLE #tmp_itTrack ([playlist_code] [int] NULL ,
	[track_name] [varchar] (75) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[album_name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[artist_name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[play_count] [smallint] NULL ,
	[previous_play_count] [smallint] NULL ,
	[ranking] [smallint] NULL ,
	[previous_ranking] [smallint] NULL ,
	[is_new_entry] [bit] NULL ,
	[rating] [smallint] NULL,
	[record_id] uniqueidentifier null)
INSERT INTO #tmp_itTrack
SELECT * 
FROM itTrack
WHERE playlist_code = @playlist_code

DELETE FROM itTrack
WHERE playlist_code = @playlist_code

INSERT INTO itTrack
SELECT @playlist_code, track_name, album_name, artist_name, sum(play_count),
	max(previous_play_count), max(ranking), max(previous_ranking), is_new_entry, rating, record_id
FROM #tmp_itTrack
WHERE playlist_code = @playlist_code
GROUP BY track_name, album_name, artist_name, is_new_entry, rating

DROP TABLE #tmp_itTrack*/

--Create an overall ranking. 10,000 basically means, "I haven't been ranked yet"
UPDATE itTrack
SET ranking = 10000
WHERE playlist_code = @playlist_code

UPDATE itTrack
SET ranking = 
(
	SELECT sort_order
	FROM
	(
		SELECT --track_name,album_name,
			record_id, RANK() OVER (ORDER BY rating desc, play_count desc, track_name) as sort_order
		FROM itTrack T2
		WHERE T2.playlist_code = @playlist_code and T2.rating = 100
	) D
	WHERE D.record_id = itTrack.record_id
)
WHERE playlist_code = @playlist_code
/*
UPDATE itTrack
SET ranking = 
(
	SELECT sort_order
	FROM
	(
		SELECT --track_name,album_name,
			record_id, RANK() OVER (ORDER BY rating desc, play_count desc, track_name) as sort_order
		FROM itTrack T2
		WHERE T2.playlist_code = @playlist_code and T2.rating < 100
	) D
	WHERE D.record_id = itTrack.record_id
)
WHERE playlist_code = @playlist_code*/

/*
DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT track_name, album_name, artist_name, sum(play_count)
	FROM itTrack
	WHERE playlist_code = @playlist_code and rating = 100
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
		WHERE playlist_code = @playlist_code
			and track_name = @track_name
			and album_name = @album_name
			and artist_name = @artist_name
		SET @ranking_count = @count
	END
	ELSE
	BEGIN
		UPDATE itTrack 
		SET ranking = @ranking_count
		WHERE playlist_code = @playlist_code
			and track_name = @track_name
			and album_name = @album_name
			and artist_name = @artist_name
	END
	SET @count = @count + 1
	
	FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name, @play_count
END
CLOSE Cursor1
DEALLOCATE Cursor1*/

--Now set the previous ranking (to generate those nice arrows)
DECLARE @last_playlist_code int
SELECT @last_playlist_code = isnull(max(playlist_code),-1)
FROM itPlaylist
WHERE playlist_code < @playlist_code
IF (@last_playlist_code = -1)
BEGIN
	UPDATE itTrack
	SET previous_ranking = ranking, @previous_play_count = play_count
	WHERE playlist_code = @playlist_code
END
ELSE
BEGIN
	UPDATE it
	SET previous_ranking = isnull(it2.ranking,0), previous_play_count = 0, is_new_entry = 1
	FROM itTrack it
	LEFT OUTER JOIN itTrack it2 ON it.track_name = it2.track_name and it.album_name = it2.album_name and it.artist_name = it2.artist_name
		and it2.playlist_code = @last_playlist_code
		and it2.previous_ranking is null
	WHERE it.playlist_code = @playlist_code

	UPDATE it
	SET previous_ranking = isnull(it2.ranking,0), previous_play_count = isnull(it2.play_count,0), is_new_entry = 0
	FROM itTrack it
	INNER JOIN itTrack it2 ON it.track_name = it2.track_name and it.album_name = it2.album_name and it.artist_name = it2.artist_name
	WHERE it.playlist_code = @playlist_code
		and it2.playlist_code = @last_playlist_code
		and not it2.previous_ranking is null
/*
	UPDATE itTrack 
	SET previous_ranking = @previous_ranking, previous_play_count = @previous_play_count, is_new_entry = 0
	WHERE playlist_code = @playlist_code
		and track_name = @track_name
		and album_name = @album_name
		and artist_name = @artist_name*/
/*
	DECLARE Cursor1 CURSOR LOCAL FOR
		SELECT track_name, album_name, artist_name
		FROM itTrack
		WHERE playlist_code = @playlist_code
	
	OPEN Cursor1
	
	--loop through all the items
	FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		SELECT @previous_ranking = null, @previous_play_count = null
		SELECT @previous_ranking = max(ranking), @previous_play_count = max(play_count)
		FROM itTrack
		WHERE playlist_code = @last_playlist_code
			and track_name = @track_name
			and album_name = @album_name
			and artist_name = @artist_name
		IF (@previous_ranking is null)
		BEGIN
			UPDATE itTrack
			SET previous_ranking = ranking, previous_play_count = 0, is_new_entry = 1
			WHERE playlist_code = @playlist_code
				and track_name = @track_name
				and album_name = @album_name
				and artist_name = @artist_name
		END
		ELSE
		BEGIN
			UPDATE itTrack 
			SET previous_ranking = @previous_ranking, previous_play_count = @previous_play_count, is_new_entry = 0
			WHERE playlist_code = @playlist_code
				and track_name = @track_name
				and album_name = @album_name
				and artist_name = @artist_name
		END
	
		FETCH NEXT FROM Cursor1 INTO @track_name, @album_name, @artist_name
	END
	
	CLOSE Cursor1
	DEALLOCATE Cursor1*/
END