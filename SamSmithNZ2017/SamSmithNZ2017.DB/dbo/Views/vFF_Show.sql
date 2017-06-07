CREATE VIEW dbo.vFF_Show
AS
SELECT sh.show_key AS ShowCode, 
	sh.show_date AS ShowDate, 
	sh.show_location AS ShowLocation, 
	sh.show_city AS ShowCity, 
	ISNULL(COUNT(ss.song_key),0) AS SongCount
FROM ff_show sh
LEFT JOIN ff_show_song ss ON sh.show_key = ss.show_key
GROUP BY sh.show_key, sh.show_date, sh.show_location, sh.show_city