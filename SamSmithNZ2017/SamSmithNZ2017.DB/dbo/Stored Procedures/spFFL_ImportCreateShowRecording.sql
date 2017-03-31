CREATE PROCEDURE [dbo].[spFFL_ImportCreateShowRecording]
	@show_key smallint,
	@number smallint,
	@Description varchar(500),
    @IsComplete bit,
    @IsCirculatedRecording bit,
    @Equipment varchar(500),
    @LowestGeneration varchar(500),
    @LowestAudioGeneration varchar(500),
    @LowestVideoGeneration varchar(500),
    @LengthSoundQuality varchar(500),
    @Notes varchar(500)
AS

	INSERT INTO ffl_show_recording
	SELECT isnull((SELECT max(recording_key)+1 FROM ffl_show_recording),1), @show_key, @number,
		@Description, @IsComplete, @IsCirculatedRecording, @Equipment, 
		@LowestGeneration, @LowestAudioGeneration, @LowestVideoGeneration,
		@LengthSoundQuality, @Notes, GETDATE()