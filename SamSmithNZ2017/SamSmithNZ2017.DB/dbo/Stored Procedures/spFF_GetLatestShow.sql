CREATE PROCEDURE [dbo].[spFF_GetLatestShow]
AS
SELECT show_key, show_date, max(show_date)
FROM ff_show
GROUP BY show_key, show_date
ORDER BY show_date DESC