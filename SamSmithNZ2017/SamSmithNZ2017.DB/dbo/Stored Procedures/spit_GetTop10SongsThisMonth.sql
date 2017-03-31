CREATE PROCEDURE [dbo].[spit_GetTop10SongsThisMonth]
	@playlist_code smallint
AS
SELECT top 10 ranking, track_name, previous_ranking - ranking as positive_change_this_month
FROM itTrack t
INNER JOIN itplaylist p ON t.playlist_code = p.playlist_code
WHERE p.playlist_code = @playlist_code 
	and ranking <= 100 and rating = 100
ORDER BY positive_change_this_month desc, track_name
SELECT top 10 ranking, track_name, ranking - previous_ranking as negative_change_this_month
FROM itTrack t
INNER JOIN itplaylist p ON t.playlist_code = p.playlist_code
WHERE p.playlist_code = @playlist_code 
	and ranking <= 100 and rating = 100
ORDER BY negative_change_this_month desc, track_name