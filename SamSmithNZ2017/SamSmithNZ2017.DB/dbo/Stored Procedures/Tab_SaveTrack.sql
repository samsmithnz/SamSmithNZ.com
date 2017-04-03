CREATE PROCEDURE [dbo].[Tab_SaveTab]
	@TabCode INT,
	@AlbumCode INT,
	@TabName VARCHAR(100),
	@TabText TEXT,
	@TabOrder INT,
	@Rating INT,
	@TuningCode INT
AS
IF (@TabCode > 0)
BEGIN
	UPDATE tab_track
	SET track_name = @TabName, track_order = @TabOrder, track_text = @TabText, rating = @Rating, tuning_code = @TuningCode, last_updated = GETDATE()
	WHERE track_code = @TabCode
END
ELSE
BEGIN
	SELECT @TabCode = MAX(track_code) + 1
	FROM tab_track
	
	INSERT INTO tab_track
	SELECT @TabCode, @AlbumCode, @TabName, @TabText, @TabOrder, @Rating, @TuningCode, GETDATE()
END