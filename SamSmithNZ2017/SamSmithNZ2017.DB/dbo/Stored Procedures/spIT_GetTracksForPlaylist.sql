CREATE PROCEDURE [dbo].[spIT_GetTracksForPlaylist]
	@playlist_code int,
	@show_just_summary bit
AS

IF (@show_just_summary = 1)
BEGIN
	SELECT *
	FROM itTrack t
	INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code and ranking <= 100 and rating = 100
	ORDER BY ranking, track_name
END
ELSE
BEGIN	
	SELECT *
	FROM itTrack t
	INNER JOIN itPlaylist p ON t.playlist_code = p.playlist_code
	WHERE p.playlist_code = @playlist_code
	ORDER BY ranking, track_name
END