CREATE PROCEDURE [dbo].[spFFL_GetComparedShowList]
	@year_code smallint
AS

/*
SELECT CONVERT(datetime,null) as combined_date,
	CONVERT(smallint,null) as show_key, 
	CONVERT(datetime,null) as show_date, 
	CONVERT(int,null) as song_total, 
	CONVERT(smallint,null) as ffl_show_key, 
	CONVERT(datetime,null) as ffl_show_date,
	CONVERT(int,null) as ffl_song_total,
	CONVERT(bit,null) as show_info_different
*/

CREATE TABLE #tmp_shows (combined_date datetime, 
	show_key smallint, show_date datetime, song_total int, 
	ffl_show_key smallint, ffl_show_date datetime, ffl_song_total int,
	show_location_different bit, show_city_different bit, number_of_recordings int)

--SELECT * FROM ff_show
--SELECT * FROM ffl_show
-- select sum(song_key) from ff_show_song where show_key = 3

INSERT INTO #tmp_shows
SELECT isnull(s.show_date,cast(floor(cast(sl.show_date as float))as datetime)) as combined_date, 
	s.show_key, s.show_date, isnull(sum(isnull(ss.song_key,0)),0),
	sl.show_key, cast(floor(cast(sl.show_date as float))as datetime), isnull(sum(isnull(ssl.song_key,0)),0),
	(CASE WHEN isnull(s.show_location,'') <> isnull(sl.show_location,'') THEN 1 ELSE 0 END), 
    (CASE WHEN (isnull(s.show_city,'') <> (isnull(sl.show_city + 
									CASE WHEN not sl.show_state is null and sl.show_state <> '' THEN ', ' + sl.show_state ELSE '' END + 
									CASE WHEN not sl.show_country is null THEN ', ' + sl.show_country ELSE '' END,''))) 
				and
				(isnull(s.show_city,'') + ', United States' <> (isnull(sl.show_city + 
									CASE WHEN not sl.show_state is null and sl.show_state <> '' THEN ', ' + sl.show_state ELSE '' END + 
									CASE WHEN not sl.show_country is null THEN ', ' + sl.show_country ELSE '' END,'')))
				 THEN 1 ELSE 0 END)
	, null as number_of_recordings
FROM ff_show s
LEFT OUTER JOIN ff_show_song ss ON s.show_key = ss.show_key
FULL OUTER JOIN ffl_show sl ON s.show_date = cast(floor(cast(sl.show_date as float))as datetime)
LEFT OUTER JOIN ffl_show_song ssl ON sl.show_key = ssl.show_key
--WHERE year(s.show_date) = 1995
--and year(sl.show_date) = 1995
GROUP BY isnull(s.show_date,sl.show_date), s.show_key, s.show_date, sl.show_key, cast(floor(cast(sl.show_date as float))as datetime),
	isnull(s.show_location,''), isnull(sl.show_location,''),
	(CASE WHEN (isnull(s.show_city,'') <> (isnull(sl.show_city + 
									CASE WHEN not sl.show_state is null and sl.show_state <> '' THEN ', ' + sl.show_state ELSE '' END + 
									CASE WHEN not sl.show_country is null THEN ', ' + sl.show_country ELSE '' END,''))) 
				and
				(isnull(s.show_city,'') + ', United States' <> (isnull(sl.show_city + 
									CASE WHEN not sl.show_state is null and sl.show_state <> '' THEN ', ' + sl.show_state ELSE '' END + 
									CASE WHEN not sl.show_country is null THEN ', ' + sl.show_country ELSE '' END,'')))
				 THEN 1 ELSE 0 END)
ORDER BY combined_date

UPDATE s
SET number_of_recordings = (SELECT count(sr.show_key) FROM ffl_show_recording sr WHERE s.ffl_show_key = sr.show_key)
FROM #tmp_shows s

SELECT combined_date, 
	isnull(show_key,-1) as show_key, isnull(show_date, '1900-01-01') as show_date, song_total,
	isnull(ffl_show_key,-1) as ffl_show_key, isnull(ffl_show_date, '1900-01-01') as ffl_show_date, ffl_song_total,
	CASE WHEN show_location_different = 1 or show_city_different = 1 THEN 1 ELSE 0 END AS show_info_different,
	number_of_recordings
FROM #tmp_shows
WHERE year(combined_date) = @year_code
ORDER BY combined_date

DROP TABLE #tmp_shows