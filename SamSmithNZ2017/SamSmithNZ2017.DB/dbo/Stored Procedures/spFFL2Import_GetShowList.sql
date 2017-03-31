CREATE PROCEDURE [dbo].spFFL2Import_GetShowList
AS
SELECT * 
FROM ffl_show
ORDER BY show_key