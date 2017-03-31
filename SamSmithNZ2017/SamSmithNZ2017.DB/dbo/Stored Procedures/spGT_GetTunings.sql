CREATE PROCEDURE [dbo].[spGT_GetTunings]
AS
SELECT tuning_code as TuningCode, tuning_name as TuningName
FROM tab_tuning
ORDER BY tuning_code