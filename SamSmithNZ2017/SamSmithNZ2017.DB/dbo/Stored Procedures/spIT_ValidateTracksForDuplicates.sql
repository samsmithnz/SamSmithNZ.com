CREATE PROCEDURE [dbo].[spIT_ValidateTracksForDuplicates]
	@playlist_code int
AS

--First get a list of all tracks that have to be updated 
DECLARE @tmp_duplicate_tracks TABLE (artist_name varchar(100), album_name varchar(100), track_name varchar(100), track_count smallint, playlist_code smallint)
INSERT INTO @tmp_duplicate_tracks
SELECT artist_name, album_name, track_name, count(*), playlist_code
FROM ittrack
WHERE playlist_code = @playlist_code
GROUP BY artist_name, album_name, track_name, playlist_code
HAVING count(*) >= 2
ORDER BY artist_name, album_name, track_name, playlist_code

DECLARE @artist_name varchar(100)
DECLARE @album_name varchar(100)
DECLARE @track_name varchar(100)

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT artist_name, album_name, track_name
	FROM @tmp_duplicate_tracks 

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @artist_name, @album_name, @track_name
WHILE (@@FETCH_STATUS <> -1)
BEGIN

	DECLARE @total_play_count smallint
	DECLARE @total_previous_play_count smallint
	DECLARE @record_id uniqueidentifier
	DECLARE @highest_rating smallint

	--Get the first record off the queue
	SELECT top 1 @record_id = record_id 
	FROM ittrack t
	WHERE t.playlist_code = @playlist_code
	and t.artist_name = @artist_name 
	and t.album_name = @album_name
	and t.track_name = @track_name
	
	--total the play counts and total previous play counts from all tracks
	SELECT @total_play_count = sum(isnull(play_count,0)), @total_previous_play_count = sum(isnull(previous_play_count,0))
	FROM ittrack t
	WHERE t.playlist_code = @playlist_code
	and t.artist_name = @artist_name 
	and t.album_name = @album_name
	and t.track_name = @track_name

	--Get the highest Rating
	SELECT @highest_rating = max(isnull(rating,0))
	FROM ittrack t
	WHERE t.playlist_code = @playlist_code
	and t.artist_name = @artist_name 
	and t.album_name = @album_name
	and t.track_name = @track_name
	
	--Delete all tracks but the 'chosen' track
	DELETE t 
	FROM ittrack t
	WHERE t.playlist_code = @playlist_code
	and t.artist_name = @artist_name 
	and t.album_name = @album_name
	and t.track_name = @track_name
	and t.record_id <> @record_id

	--Update the 'chosen' track
	UPDATE ittrack
	SET play_count = @total_play_count, previous_play_count = @total_previous_play_count, rating = @highest_rating
	WHERE record_id = @record_id

	FETCH NEXT FROM Cursor1 INTO @artist_name, @album_name, @track_name
END

CLOSE Cursor1
DEALLOCATE Cursor1

SELECT *
FROM @tmp_duplicate_tracks