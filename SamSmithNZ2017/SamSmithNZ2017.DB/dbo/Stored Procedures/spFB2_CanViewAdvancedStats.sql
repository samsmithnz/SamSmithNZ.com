CREATE PROCEDURE [dbo].[spFB2_CanViewAdvancedStats]
	@employee_code char(5)
AS

SELECT count(*) as can_view_adv_stats
FROM fbstats
WHERE employee_code = @employee_code