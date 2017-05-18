CREATE PROCEDURE [dbo].[FFL_GetSongs] 
	@song_key INT = NULL,
	@album_key INT = NULL,
	@show_key INT = NULL
AS
BEGIN
	SELECT s.song_key AS SongKey, 
		s.song_name AS SongName,  
		s.song_notes AS SongNotes, 
		s.song_lyrics AS SongLyrics, 
		s.song_image AS SongImage,
		MIN(sh.show_date) AS FirstPlayed, 
		MIN(sh.show_key) AS FirstPlayedShowKey,
		MAX(sh.show_date) AS LastPlayed, 
		MAX(sh.show_key) AS LastPlayedShowKey,
		ISNULL(COUNT(ss.song_key),0) AS TimesPlayed, 
		a.album_key AS AlbumKey, 
		a.album_name AS AlbumName, 
		CASE WHEN NOT @show_key IS NULL THEN ss.show_song_order ELSE 1 END AS SongOrder
	FROM ff_song s
	JOIN ff_album a ON s.album_key = a.album_key
	LEFT OUTER JOIN ffl_show_song ss ON ss.song_key = s.song_key
	LEFT OUTER JOIN ffl_show sh ON sh.show_key = ss.show_key
	WHERE (s.song_key = @song_key OR @song_key IS NULL)
	AND (a.album_key = @album_key OR @album_key IS NULL)
	AND (sh.show_key = @show_key OR @show_key IS NULL)
	GROUP BY s.song_key, s.song_name, 
		s.song_notes, s.song_lyrics, s.song_image,
		a.album_key, a.album_name, s.song_order, 
		CASE WHEN NOT @show_key IS NULL THEN ss.show_song_order ELSE 1 END
	ORDER BY CASE WHEN @song_key IS NULL AND @album_key IS NULL THEN ISNULL(COUNT(ss.song_key),0) ELSE 1 END DESC,
		CASE WHEN NOT @album_key IS NULL THEN s.song_order ELSE 1 END,
		CASE WHEN NOT @show_key IS NULL THEN CASE WHEN NOT @show_key IS NULL THEN ss.show_song_order ELSE 1 END END, 
		s.song_name
END