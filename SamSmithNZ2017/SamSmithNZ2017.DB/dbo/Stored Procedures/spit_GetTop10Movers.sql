CREATE PROCEDURE [dbo].[spit_GetTop10Movers]
	@playlist_code smallint,
	@show_just_summary bit
AS

DECLARE @count smallint

IF (@show_just_summary = 1)
BEGIN
	SELECT top 1 @count = play_count - previous_play_count
	FROM itTrack t
	INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code 
		--and ranking <= 100 
		and rating = 100
	ORDER BY play_count - previous_play_count desc
	
	IF (@count > 0)
	BEGIN
		SELECT top 10 @count = play_count - previous_play_count
		FROM itTrack t
		INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE p.playlist_code = @playlist_code 
			--and ranking <= 100 
			and rating = 100
		ORDER BY play_count - previous_play_count desc, artist_name + ' - ' + track_name
		SELECT artist_name + ' - ' + track_name as track_name, play_count, play_count - previous_play_count as change_this_month
		FROM itTrack t
		INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE p.playlist_code = @playlist_code 
			--and ranking <= 100 
			and rating = 100 and play_count - previous_play_count > = @count
		ORDER BY change_this_month desc, artist_name + ' - ' + track_name
	END
END 
ELSE
BEGIN
	SELECT top 1 @count = play_count - previous_play_count
	FROM itTrack t
	INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code 
	ORDER BY play_count - previous_play_count desc
	
	IF (@count > 0)
	BEGIN
		SELECT top 10 @count = play_count - previous_play_count
		FROM itTrack t
		INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE p.playlist_code = @playlist_code 
		ORDER BY play_count - previous_play_count desc, artist_name + ' - ' + track_name
		SELECT artist_name + ' - ' + track_name as track_name, play_count, play_count - previous_play_count as change_this_month
		FROM itTrack t
		INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
		WHERE p.playlist_code = @playlist_code 
			and play_count - previous_play_count > = @count
		ORDER BY change_this_month desc, artist_name + ' - ' + track_name
	END
END