CREATE PROCEDURE [dbo].[spGT_GetRatings]
AS
SELECT rating as RatingCode
FROM vwtab_rating
ORDER BY rating