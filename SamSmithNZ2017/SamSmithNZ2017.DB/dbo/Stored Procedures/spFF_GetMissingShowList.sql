
CREATE PROCEDURE [dbo].[spFF_GetMissingShowList]
AS

SELECT sh.show_key, sh.show_date, sh.show_location, sh.show_city, isnull(count(ss.song_key),0) as song_count
FROM ff_show sh
LEFT OUTER JOIN ff_show_song ss ON sh.show_key = ss.show_key
GROUP BY sh.show_key, sh.show_date, sh.show_location, sh.show_city
HAVING isnull(count(ss.song_key),0) = 0
ORDER BY sh.show_date desc, sh.show_key desc