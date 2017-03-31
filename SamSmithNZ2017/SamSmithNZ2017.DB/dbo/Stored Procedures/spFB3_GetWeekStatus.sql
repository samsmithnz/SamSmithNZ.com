CREATE PROCEDURE [dbo].[spFB3_GetWeekStatus]
	@year_code smallint,
	@week_code smallint
AS

SELECT * 
FROM fbstatus
WHERE year_code = @year_code 
and week_code = @week_code