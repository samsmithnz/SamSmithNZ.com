CREATE PROCEDURE [dbo].[FFL_GetSongs] 
	@SongCode INT = NULL,
	@AlbumCode INT = NULL,
	@ShowCode INT = NULL
AS
BEGIN
	--The group bys were getting really complicated, so broke this into two statements to keep it simple
	IF (@ShowCode IS NULL)
	BEGIN
		SELECT s.song_key AS SongCode, 
			s.song_name AS SongName,  
			s.song_notes AS SongNotes, 
			s.song_lyrics AS SongLyrics, 
			s.song_image AS SongImage,
			MIN(sh.show_date) AS FirstPlayed, 
			MIN(sh.show_key) AS FirstPlayedShowCode,
			MAX(sh.show_date) AS LastPlayed, 
			MAX(sh.show_key) AS LastPlayedShowCode,
			ISNULL(COUNT(ss.song_key),0) AS TimesPlayed, 
			a.album_key AS AlbumCode, 
			a.album_name AS AlbumName, 
			s.song_order AS SongOrder
		FROM ff_song s
		JOIN ff_album a ON s.album_key = a.album_key
		LEFT JOIN ff_show_song ss ON ss.song_key = s.song_key
		LEFT JOIN ff_show sh ON sh.show_key = ss.show_key
		WHERE (s.song_key = @SongCode OR @SongCode IS NULL)
		AND (a.album_key = @AlbumCode OR @AlbumCode IS NULL)
		GROUP BY s.song_key, s.song_name, 
			s.song_notes, s.song_lyrics, s.song_image,
			a.album_key, a.album_name, s.song_order
		ORDER BY CASE WHEN @SongCode IS NULL AND @AlbumCode IS NULL THEN ISNULL(COUNT(ss.song_key),0) ELSE 1 END DESC,
			CASE WHEN not @SongCode IS NULL OR NOT @AlbumCode IS NULL THEN s.song_order END, s.song_name
	END
	ELSE
	BEGIN
		SELECT s.song_key AS SongCode, 
			s.song_name AS SongName,  
			s.song_notes AS SongNotes, 
			s.song_lyrics AS SongLyrics, 
			s.song_image AS SongImage,
			MIN(sh.show_date) AS FirstPlayed, 
			MIN(sh.show_key) AS FirstPlayedShowCode,
			MAX(sh.show_date) AS LastPlayed, 
			MAX(sh.show_key) AS LastPlayedShowCode,
			ISNULL(COUNT(ss.song_key),0) AS TimesPlayed, 
			a.album_key AS AlbumCode, 
			a.album_name AS AlbumName, 
			ss.show_song_order AS SongOrder
		FROM ff_song s
		JOIN ff_album a ON s.album_key = a.album_key
		LEFT JOIN ff_show_song ss ON ss.song_key = s.song_key
		LEFT JOIN ff_show sh ON sh.show_key = ss.show_key
		WHERE sh.show_key = @ShowCode
		GROUP BY s.song_key, s.song_name, 
			s.song_notes, s.song_lyrics, s.song_image, 
			a.album_key, a.album_name, ss.show_song_order
		ORDER BY ss.show_song_order
	END

END