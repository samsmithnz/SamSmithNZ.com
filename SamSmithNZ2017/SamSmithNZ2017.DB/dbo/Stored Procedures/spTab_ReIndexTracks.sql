CREATE PROCEDURE [dbo].[spTab_ReIndexTracks]
AS
SELECT 0
--	DECLARE @TrackKey int
--	DECLARE @count int
--	--Get each Track
--	DECLARE Cursor1 CURSOR LOCAL FOR
--		SELECT TrackKey
--		FROM TabTrack
--		ORDER BY TrackKey
--	OPEN Cursor1
	
--	SET @count = 1
--	FETCH NEXT FROM Cursor1 INTO @TrackKey
--	WHILE (@@FETCH_STATUS <> -1)
--	BEGIN
--		UPDATE TabTrack SET TrackKey = @Count	WHERE TrackKey = @TrackKey
--		SET @count = @count + 1
--		FETCH NEXT FROM Cursor1 INTO @TrackKey
--	END
	
--	CLOSE Cursor1
--	DEALLOCATE Cursor1