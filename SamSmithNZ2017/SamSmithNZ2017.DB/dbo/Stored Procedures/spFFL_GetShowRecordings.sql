CREATE PROCEDURE [dbo].[spFFL_GetShowRecordings]
	@show_key smallint
AS

SELECT * 
FROM ffl_show_recording
WHERE show_key = @show_key
ORDER BY number