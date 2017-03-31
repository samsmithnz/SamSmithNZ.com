CREATE PROCEDURE [dbo].[spit_GetTabForTrack]
	@trackname varchar(500),
	@artistname varchar(500)
AS
SELECT 0
--IF (@artistname = '')
--BEGIN
--	SELECT *, (ArtistName + ' - ' + AlbumName) AS FullName
--	FROM tabtrack t
--	INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--	WHERE t.trackname like '%' + @trackname
--END
--ELSE
--BEGIN
--	IF exists(SELECT *, (ArtistName + ' - ' + AlbumName) AS FullName
--	FROM tabtrack t
--	INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--	WHERE t.trackname like '%' + @trackname 
--		and a.artistname like '%' + @artistname)
--	BEGIN
--		SELECT *, (ArtistName + ' - ' + AlbumName) AS FullName
--		FROM tabtrack t
--		INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--		WHERE t.trackname like '%' + @trackname 
--			and a.artistname like '%' + @artistname 
--	END
--	ELSE
--	BEGIN
--		SELECT *, (ArtistName + ' - ' + AlbumName) AS FullName
--		FROM tabtrack t
--		INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--		WHERE t.trackname like '%' + @trackname + '%' and t.trackname like '%' + @artistname + '%'
--	END
--END