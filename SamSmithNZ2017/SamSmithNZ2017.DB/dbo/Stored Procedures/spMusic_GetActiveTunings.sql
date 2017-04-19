CREATE PROCEDURE [dbo].[spMusic_GetActiveTunings]
AS
SELECT DISTINCT tu.tuning_code, tu.tuning_name 
FROM tab_track tt
JOIN tab_tuning tu ON tt.tuning_code = tu.tuning_code
WHERE tt.tuning_code <> 0 
ORDER BY tu.tuning_code