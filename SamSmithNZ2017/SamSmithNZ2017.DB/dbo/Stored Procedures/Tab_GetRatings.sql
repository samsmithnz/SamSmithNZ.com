CREATE PROCEDURE [dbo].[Tab_GetRatings]
AS
SELECT r.rating as RatingCode
FROM vwtab_rating r
ORDER BY r.rating