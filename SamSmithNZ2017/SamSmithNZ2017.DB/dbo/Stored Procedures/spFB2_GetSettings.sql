CREATE PROCEDURE [dbo].[spFB2_GetSettings]
AS

SELECT current_week_code, current_year_code
FROM FBSettings