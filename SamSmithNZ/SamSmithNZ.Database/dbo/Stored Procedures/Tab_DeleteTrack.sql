CREATE PROCEDURE [dbo].[Tab_DeleteTab]
	@TabCode INT
AS
DELETE FROM tab_track
WHERE track_code = @TabCode