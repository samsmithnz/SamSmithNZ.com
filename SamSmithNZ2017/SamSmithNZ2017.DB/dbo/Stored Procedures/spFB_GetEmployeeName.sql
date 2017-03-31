
CREATE PROCEDURE [dbo].[spFB_GetEmployeeName] 
	@ecode char(5)
AS

SELECT 0
--SELECT isnull(e.familiar_name, e.first_name) + ' ' + e.last_name as full_name
--FROM employee e
--WHERE employee_code = @ecode