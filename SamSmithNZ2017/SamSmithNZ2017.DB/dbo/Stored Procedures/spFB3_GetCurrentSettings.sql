CREATE PROCEDURE [dbo].[spFB3_GetCurrentSettings]
AS

SELECT current_week_code, current_year_code
FROM FBSettings