CREATE PROCEDURE [dbo].[spMusic_GetTuningList]
AS

SELECT *
FROM tab_tuning
ORDER BY tuning_code