CREATE PROCEDURE [dbo].[spITunes_GetTracks]
	@playlist_code int,
	@show_just_summary bit,
	@track_name varchar(50) = null
AS

IF (@show_just_summary = 1)
BEGIN
	SELECT *
	FROM itTrack t
	INNER JOIN itplaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code and ranking <= 100 and rating = 100
	and (t.track_name = @track_name or @track_name is null)
	ORDER BY ranking, track_name
END
ELSE
BEGIN	
	SELECT *
	FROM itTrack t
	INNER JOIN itplaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code
	and (t.track_name = @track_name or @track_name is null)
	ORDER BY ranking, track_name
END