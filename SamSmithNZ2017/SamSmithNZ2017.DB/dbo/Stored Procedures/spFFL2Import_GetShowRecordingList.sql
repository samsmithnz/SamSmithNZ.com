CREATE PROCEDURE [dbo].spFFL2Import_GetShowRecordingList
AS
SELECT * 
FROM ffl_show_recording
ORDER BY recording_key