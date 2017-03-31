CREATE PROCEDURE [dbo].[spTab2_GetStatistics]
AS
SELECT 
	(SELECT count(*) 
	FROM tab_album
	WHERE is_bass_tab = 0) AS Guitar_Album_Count,
	(SELECT count(*) 
	FROM tab_album
	WHERE is_bass_tab = 1) AS Bass_Album_Count,
	(SELECT count(*) 
	FROM tab_album a 
	INNER JOIN tab_track t ON a.album_code = t.album_code WHERE is_bass_tab = 0) AS Guitar_Track_Count,
	(SELECT count(*) 
	FROM tab_album a 
	INNER JOIN tab_track t ON a.album_code = t.album_code WHERE is_bass_tab = 1) AS Bass_Track_Count