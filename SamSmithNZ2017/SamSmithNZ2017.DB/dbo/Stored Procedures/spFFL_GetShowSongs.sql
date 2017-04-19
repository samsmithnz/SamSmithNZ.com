CREATE PROCEDURE [dbo].[spFFL_GetShowSongs]
	@show_key smallint,
	@ffl_show_key smallint
AS
SET NOCOUNT ON
/*
SELECT CONVERT(smallint,null) as show_song_order, 
	CONVERT(smallint,null) as song_key, 
	CONVERT(varchar(100),null) as song_name,
	CONVERT(smallint,null) as ffl_song_key, 
	CONVERT(varchar(100),null) as ffl_song_name,
	CONVERT(varchar(50),null) as ffl_song_notes,
	CONVERT(bit,null) as is_known_song
*/


--Get the total counts
DECLARE @show_count smallint
DECLARE @ffl_show_count smallint
SELECT @show_count = count(*)
FROM ff_show_song ss
WHERE ss.show_key = @show_key
SELECT @ffl_show_count = count(*)
FROM ffl_show_song ss
WHERE ss.show_key = @ffl_show_key  
DECLARE @total smallint
IF (@show_count >= @ffl_show_count)
BEGIN
	SELECT @total = @show_count
END
ELSE
BEGIN
	SELECT @total = @ffl_show_count
END

--Insert all the records into the temp table
CREATE TABLE #tmp_show_songs (show_song_order smallint, 
	song_key smallint, song_name varchar(100),
	ffl_song_key smallint, ffl_song_name varchar(100), ffl_song_notes varchar(50), is_known_song bit)

DECLARE @count smallint
SET @count = 0
WHILE (SELECT @count) < @total
BEGIN
	SELECT @count = @count + 1
	INSERT INTO #tmp_show_songs
	SELECT @count, null, null, null, null, null, null
END

--Add Show Songs
UPDATE ts
SET ts.song_key = s.song_key, 
	ts.song_name = s.song_name
FROM #tmp_show_songs ts
JOIN ff_show_song ss ON ss.show_song_order = ts.show_song_order
LEFT OUTER JOIN ff_song s ON ss.song_key = s.song_key
WHERE ss.show_key = @show_key

--Add FFL Show Songs
UPDATE ts
SET ts.ffl_song_key = isnull(ss.song_key,ss.unknown_song_key), 
	ts.ffl_song_name = isnull(s.song_name, us.song_name),
	ts.ffl_song_notes = s.song_notes,
	ts.is_known_song = CASE WHEN ss.unknown_song_key is null THEN 1 ELSE 0 END
FROM #tmp_show_songs ts
JOIN ffl_show_song ss ON ss.show_song_order = ts.show_song_order
LEFT OUTER JOIN ff_song s ON ss.song_key = s.song_key
LEFT OUTER JOIN ffl_unknown_song us ON ss.unknown_song_key = us.song_key
WHERE ss.show_key = @ffl_show_key

/*
SELECT ss.show_song_order, s.song_key, s.song_name
FROM ff_show_song ss
LEFT OUTER JOIN ff_song s ON ss.song_key = s.song_key
WHERE ss.show_key = @show_key
ORDER BY ss.show_song_order, s.song_name

SELECT ssl.show_song_order, isnull(ssl.song_key,ssl.unknown_song_key), isnull(s.song_name, usl.song_name)
FROM ffl_show_song ssl
LEFT OUTER JOIN ff_song s ON ssl.song_key = s.song_key
LEFT OUTER JOIN ffl_unknown_song usl ON ssl.unknown_song_key = usl.song_key
WHERE ssl.show_key = @ffl_show_key
ORDER BY ssl.show_song_order, s.song_name
*/

SELECT show_song_order, 
	isnull(song_key,-1) as song_key, 
	isnull(song_name,'') as song_name,
	isnull(ffl_song_key,-1) as ffl_song_key, 
	isnull(ffl_song_name,'') as ffl_song_name,
	isnull(ffl_song_notes,'') as ffl_song_notes,
	isnull(is_known_song,0) as is_known_song
FROM #tmp_show_songs
ORDER BY show_song_order

--SELECT ssl.show_song_order, isnull(ssl.song_key,ssl.unknown_song_key), isnull(s.song_name, usl.song_name)
--FROM ffl_show_song ssl
--LEFT OUTER JOIN ff_song s ON ssl.song_key = s.song_key
--LEFT OUTER JOIN ffl_unknown_song usl ON ssl.unknown_song_key = usl.song_key
--WHERE ssl.show_key = @ffl_show_key
--ORDER BY ssl.show_song_order, s.song_name
--
--SELECT ss.show_song_order, s.song_key, s.song_name
--FROM ff_show_song ss
--LEFT OUTER JOIN ff_song s ON ss.song_key = s.song_key
--WHERE ss.show_key = @show_key
--ORDER BY ss.show_song_order, s.song_name

DROP TABLE #tmp_show_songs