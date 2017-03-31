CREATE PROCEDURE [dbo].[spError_GetTop5Errors]
AS

SELECT top 10 *
FROM ErrorLog
ORDER BY last_updated DESC