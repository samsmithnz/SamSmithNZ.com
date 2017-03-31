CREATE PROCEDURE [dbo].[spTab2_GetRatingStats]
AS

SELECT -1 as number, count(*) as rating_count
FROM tab_track
WHERE rating > 0

UNION

SELECT 0 as number, count(*) as rating_count
FROM tab_track
WHERE rating = 0

UNION

SELECT 1 as number, count(*) as rating_count
FROM tab_track
WHERE rating = 1

UNION

SELECT 2 as number, count(*) as rating_count
FROM tab_track
WHERE rating = 2

UNION

SELECT 3 as number, count(*) as rating_count
FROM tab_track
WHERE rating = 3

UNION

SELECT 4 as number, count(*) as rating_count
FROM tab_track
WHERE rating = 4

UNION

SELECT 5 as number, count(*) as rating_count
FROM tab_track
WHERE rating = 5