

CREATE PROCEDURE [dbo].[spFB_GetCurrentPlayerCode]
	@year_code smallint,
	@ecode char(5)
AS

CREATE TABLE #tmpCode (player_code smallint)

INSERT INTO #tmpCode
SELECT isnull(player_code,0)
FROM fbplayer
WHERE employee_code = @ecode and year_code = @year_code

DECLARE @count smallint

SELECT @count = count(*)
FROM #tmpCode

IF (@count = 0) 
BEGIN
	INSERT INTO #tmpCode VALUES (0)
END

SELECT * FROM #tmpCode

DROP TABLE #tmpCode