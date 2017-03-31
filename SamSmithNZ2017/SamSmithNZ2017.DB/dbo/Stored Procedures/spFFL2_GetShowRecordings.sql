CREATE PROCEDURE [dbo].[spFFL2_GetShowRecordings]
	@show_key smallint,
	@iscirculatedrecording bit
AS

SELECT [Description], IsComplete, IsCirculatedRecording, Equipment, 
	LowestGeneration, LowestAudioGeneration, LowestVideoGeneration, LengthSoundQuality, Notes
FROM ffl_show_recording
WHERE show_key = @show_key
and iscirculatedrecording = @iscirculatedrecording
ORDER BY iscirculatedrecording desc, number