CREATE PROCEDURE [dbo].[spFFL2_GetSongShowList]
	@song_key smallint	
AS

SELECT sh.show_key, sh.show_date, sh.show_location, sh.show_city, sh.show_country
FROM ffl_show sh
LEFT OUTER JOIN ffl_show_song ss ON sh.show_key = ss.show_key
WHERE ss.song_key = @song_key
GROUP BY sh.show_key, sh.show_date, sh.show_location, sh.show_city, sh.show_country
ORDER BY sh.show_date desc, sh.show_key desc