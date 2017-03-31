CREATE PROCEDURE [dbo].[spPM_GetIdeaProject]
	@project_id uniqueidentifier
AS

SELECT * 
FROM pm_project p
WHERE p.project_id = @project_id