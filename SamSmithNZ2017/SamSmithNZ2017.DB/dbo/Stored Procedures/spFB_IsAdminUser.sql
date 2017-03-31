
CREATE PROCEDURE [dbo].[spFB_IsAdminUser]
	@employee_code char(5)
AS

SELECT count(*)
FROM fbAdmin
WHERE employee_code = @employee_code