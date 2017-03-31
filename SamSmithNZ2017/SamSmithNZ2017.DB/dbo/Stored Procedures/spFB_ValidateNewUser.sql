CREATE PROCEDURE [dbo].[spFB_ValidateNewUser]
	@employee_code char(5)
AS
SELECT 0
--SELECT count(*) 
--FROM FBUser
--WHERE employee_code = @employee_code