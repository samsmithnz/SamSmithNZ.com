CREATE PROCEDURE [dbo].[Tab_GetTunings]
AS
SELECT t.tuning_code AS TuningCode, t.tuning_name AS TuningName
FROM tab_tuning t
ORDER BY tuning_code