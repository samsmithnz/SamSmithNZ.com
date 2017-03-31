CREATE PROCEDURE [dbo].[spFFL2_GetYearList]
	@sort bit
AS

IF (@sort = 1)
BEGIN
	SELECT DISTINCT year(s.show_date) as year_code, CONVERT(varchar(5),year(s.show_date)) as year_text
	FROM ffl_show s
	ORDER BY year_code DESC
END
ELSE
BEGIN
	SELECT DISTINCT year(s.show_date) as year_code, CONVERT(varchar(5),year(s.show_date)) as year_text
	FROM ffl_show s
	ORDER BY year_code ASC
END