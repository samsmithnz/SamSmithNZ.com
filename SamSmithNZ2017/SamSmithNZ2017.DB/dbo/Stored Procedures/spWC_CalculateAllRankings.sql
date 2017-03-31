CREATE PROCEDURE [dbo].[spWC_CalculateAllRankings]
AS

DECLARE @year_code smallint

DECLARE Cursor1 CURSOR LOCAL FOR
	SELECT DISTINCT [year]
	FROM wc_tournament
	ORDER BY [year]

OPEN Cursor1

--loop through all the items
FETCH NEXT FROM Cursor1 INTO @year_code
WHILE (@@FETCH_STATUS <> -1)
BEGIN

	--DO SOMETHING	
	DECLARE @year_date datetime
	SELECT @year_date = CONVERT(datetime,CONVERT(varchar(10),@year_code) + '-12-31')
	exec spWC_CalculateRanking @year_date

	FETCH NEXT FROM Cursor1 INTO @year_code
END

CLOSE Cursor1
DEALLOCATE Cursor1

--exec spWC_CalculateAllRankings