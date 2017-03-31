CREATE FUNCTION [dbo].[fnit_GetTabForTrack] (@trackname varchar(500), @artistname varchar(500))
RETURNS varchar(1000)
BEGIN

--	DECLARE @fullname varchar(500)
--	DECLARE @track_name varchar(500)
	DECLARE @result varchar(1000)

--	IF (@artistname = '')
--	BEGIN
--		SELECT @fullname = (ArtistName + ' - ' + AlbumName), @track_name = TrackName
--		FROM tabtrack t
--		INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--		WHERE t.trackname like '%' + @trackname
--	END
--	ELSE
--	BEGIN
--		IF exists(SELECT *, (ArtistName + ' - ' + AlbumName) AS FullName
--		FROM tabtrack t
--		INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--		WHERE t.trackname like '%' + @trackname 
--			and a.artistname like '%' + @artistname)
--		BEGIN
--			SELECT @fullname = (ArtistName + ' - ' + AlbumName), @track_name = TrackName
--			FROM tabtrack t
--			INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--			WHERE t.trackname like '%' + @trackname 
--				and a.artistname like '%' + @artistname 
--		END
--		ELSE
--		BEGIN
--			SELECT @fullname = (ArtistName + ' - ' + AlbumName), @track_name = TrackName
--			FROM tabtrack t
--			INNER JOIN tabalbum a ON t.albumkey = a.albumkey
--			WHERE t.trackname like '%' + @trackname + '%' and t.trackname like '%' + @artistname + '%'
--		END
--	END

	RETURN @result
END