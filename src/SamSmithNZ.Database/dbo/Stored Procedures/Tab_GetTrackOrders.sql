CREATE PROCEDURE [dbo].[Tab_GetTrackOrders]
AS
SELECT 0 AS SortOrderCode, 
	'Track Order' AS SortOrderName
	UNION
SELECT 1 AS SortOrderCode,
	'Tuning' AS SortOrderName
ORDER BY SortOrderCode