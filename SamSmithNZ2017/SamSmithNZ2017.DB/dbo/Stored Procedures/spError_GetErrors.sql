CREATE PROCEDURE [dbo].[spError_GetErrors]
AS

SELECT *
FROM ErrorLog
ORDER BY last_updated DESC