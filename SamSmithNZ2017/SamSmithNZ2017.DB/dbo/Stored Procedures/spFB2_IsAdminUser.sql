CREATE PROCEDURE [dbo].[spFB2_IsAdminUser]
	@employee_code char(5)
AS

SELECT count(*) as admin_count
FROM fbAdmin
WHERE employee_code = @employee_code