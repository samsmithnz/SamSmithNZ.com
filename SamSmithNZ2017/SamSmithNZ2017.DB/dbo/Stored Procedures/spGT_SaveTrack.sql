CREATE PROCEDURE [dbo].[spGT_SaveTrack]
	@track_code smallint,
	@album_code smallint,
	@track_name varchar(100),
	@track_text text,
	@track_order smallint,
	@rating smallint,
	@tuning_code smallint
AS
IF (@track_code > 0)
BEGIN
	UPDATE tab_track
	SET track_name = @track_name, track_order = @track_order, track_text = @track_text, rating = @rating, tuning_code = @tuning_code, last_updated = getdate()
	WHERE track_code = @track_code
END
ELSE
BEGIN
	SELECT @track_code = max(track_code) + 1
	FROM tab_track
	
	INSERT INTO tab_track
	SELECT @track_code, @album_code, @track_name, @track_text, @track_order, @rating, @tuning_code, getdate()
END