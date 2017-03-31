CREATE PROCEDURE [dbo].[spTab_ReIndexAlbums]
AS
SELECT 0
--	DECLARE @albumKey int
--	DECLARE @count int
--	--Get each Album
--	DECLARE Cursor1 CURSOR LOCAL FOR
--		SELECT albumkey
--		FROM Tabalbum
--		ORDER BY albumkey
--	OPEN Cursor1
	
--	SET @count = 1
--	FETCH NEXT FROM Cursor1 INTO @albumKey
--	WHILE (@@FETCH_STATUS <> -1)
--	BEGIN
--		UPDATE Tabalbum SET Albumkey = @Count	WHERE albumkey = @albumkey
--		UPDATE Tabtrack SET Albumkey = @Count	WHERE albumkey = @albumkey
--		SET @count = @count + 1
--		FETCH NEXT FROM Cursor1 INTO @albumKey
--	END
	
--	CLOSE Cursor1
--	DEALLOCATE Cursor1