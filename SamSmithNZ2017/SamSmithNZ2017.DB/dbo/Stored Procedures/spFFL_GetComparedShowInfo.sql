CREATE PROCEDURE [dbo].[spFFL_GetComparedShowInfo]
	@show_key smallint,
	@ffl_show_key smallint
AS

/*
SELECT CONVERT(datetime,null) as combined_show_date , 
	CONVERT(smallint,null) as show_key, 
	CONVERT(varchar(100),null) as show_location, 
	CONVERT(varchar(100),null) as show_city,
	CONVERT(smallint,null) as ffl_show_key, 
	CONVERT(varchar(100),null) as ffl_show_location, 
	CONVERT(varchar(100),null) as ffl_show_city,
	CONVERT(varchar(500),null) as other_performers,
	CONVERT(varchar(500),null) as notes,
	CONVERT(int,null) as number_of_recordings*/

CREATE TABLE #tmp_show (combined_show_date datetime, 
	show_key smallint, show_location varchar(100), show_city varchar(100),
	ffl_show_key smallint, ffl_show_location varchar(100), ffl_show_city varchar(100),
	other_performers varchar(500), notes varchar(500))

INSERT INTO #tmp_show
SELECT isnull(sh.show_date, sl.show_date),
	isnull(sh.show_key,-1), isnull(sh.show_location,''), isnull(sh.show_city,''),
	isnull(sl.show_key,-1), isnull(sl.show_location,''), isnull(sl.show_city + 
									CASE WHEN not sl.show_state is null and sl.show_state <> '' THEN ', ' + sl.show_state ELSE '' END + 
									CASE WHEN not sl.show_country is null and sl.show_country <> '' THEN ', ' + sl.show_country ELSE '' END,''), 
	sl.other_performers, sl.notes
FROM ff_show sh, ffl_show sl
WHERE sh.show_key = @show_key
and sl.show_key = @ffl_show_key

SELECT * 
FROM #tmp_show

DROP TABLE #tmp_show