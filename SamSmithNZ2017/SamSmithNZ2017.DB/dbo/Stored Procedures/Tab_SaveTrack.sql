CREATE PROCEDURE [dbo].[Tab_SaveTrack]
	@TrackCode INT,
	@AlbumCode INT,
	@TrackName VARCHAR(100),
	@TrackText TEXT,
	@TrackOrder INT,
	@Rating INT,
	@TuningCode INT
AS
IF (@TrackCode > 0)
BEGIN
	UPDATE tab_track
	SET track_name = @TrackName, track_order = @TrackOrder, track_text = @TrackText, rating = @Rating, tuning_code = @TuningCode, last_updated = GETDATE()
	WHERE track_code = @TrackCode
END
ELSE
BEGIN
	SELECT @TrackCode = MAX(track_code) + 1
	FROM tab_track
	
	INSERT INTO tab_track
	SELECT @TrackCode, @AlbumCode, @TrackName, @TrackText, @TrackOrder, @Rating, @TuningCode, GETDATE()
END