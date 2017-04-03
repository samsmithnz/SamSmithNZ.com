CREATE PROCEDURE [dbo].[Tab_GetAlbums]
	@AlbumCode INT = NULL,
	@IsAdmin BIT = NULL
AS
BEGIN
	CREATE TABLE #tmpResults (AlbumCode INT, 
		ArtistName VARCHAR(100), 
		ArtistNameTrimed VARCHAR(100), 
		AlbumName VARCHAR(100), 
		AlbumYear INT, 
		BassAlbumCode INT,
		IsNewAlbum BIT, 
		IsMiscCollectionAlbum BIT, 
		IncludeInIndex BIT, 
		IncludeOnWebsite BIT, 
		AverageRating INT)

	INSERT INTO #tmpResults
	SELECT ta.album_code AS AlbumCode, 
		artist_name AS ArtistName, 
		replace(artist_name,' ','') AS ArtistNameTrimed,
		album_name AS AlbumName, 
		album_year AS AlbumYear,
		NULL AS BassAlbumCode, 
		--is_bass_tab AS IsBassTab, 
		is_new_album AS IsNewAlbum, 
		is_misc_collection_album AS IsMiscCollectionAlbum, 
		include_in_index AS IncludeInIndex, 
		include_on_website AS IncludeOnWebsite,
		ISNULL(CONVERT(INT,ROUND(CONVERT(DECIMAL(16,4),SUM(tt.rating))/CONVERT(DECIMAL(16,4),COUNT(tt.rating)),0)),0) AS AverageRating
	FROM tab_album ta
	LEFT OUTER JOIN tab_track tt ON ta.album_code = tt.album_code AND tt.rating > 0
	WHERE (@IsAdmin = 1 OR (@IsAdmin = 0 AND include_in_index = 1) OR (@IsAdmin IS NULL AND include_in_index = 1))
	AND (@AlbumCode IS NULL OR ta.album_code = @AlbumCode)
	AND is_bass_tab = 0
	GROUP BY artist_name, album_name, album_year, is_bass_tab, is_new_album, 
		is_misc_collection_album, include_in_index, include_on_website, ta.album_code
	--ORDER BY artist_name, album_year, album_name, is_bass_tab

	UPDATE r
	SET r.BassAlbumCode = a.album_code
	FROM #tmpResults r
	JOIN tab_album a ON r.ArtistName = a.artist_name AND r.AlbumName = a.album_name
	WHERE a.is_bass_tab = 1

	SELECT r.AlbumCode, 
		r.ArtistName, 
		r.ArtistNameTrimed, 
		r.AlbumName, 
		r.AlbumYear, 
		r.BassAlbumCode,
		r.IsNewAlbum, 
		r.IsMiscCollectionAlbum, 
		r.IncludeInIndex, 
		r.IncludeOnWebsite, 
		r.AverageRating 
	FROM #tmpResults r
	ORDER BY r.ArtistName, r.AlbumYear, r.AlbumName

	DROP TABLE #tmpResults
END
GO