
CREATE PROCEDURE [dbo].[spImport_ImportTrack]
	@album_code smallint,
	@track_name varchar(100),
	@track_text text,
	@track_order smallint,
	@rating smallint,
	@tuning_code smallint
AS
INSERT INTO tab_track
SELECT isnull((SELECT max(track_code) + 1 FROM tab_track),1), 
	@album_code, @track_name, @track_text, @track_order, 
	@rating, @tuning_code, getdate()