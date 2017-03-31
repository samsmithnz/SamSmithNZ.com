
CREATE PROCEDURE [dbo].[spFB_CanViewAdvancedStats]
	@employee_code char(5)
AS

SELECT count(*) 
FROM fbstats
WHERE employee_code = @employee_code