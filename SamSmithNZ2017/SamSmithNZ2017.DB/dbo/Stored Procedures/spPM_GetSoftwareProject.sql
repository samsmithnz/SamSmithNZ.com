CREATE PROCEDURE [dbo].[spPM_GetSoftwareProject]
	@project_id uniqueidentifier
AS

SELECT * 
FROM pm_project p
JOIN pm_software s ON p.project_id = s.project_id
WHERE p.project_id = @project_id