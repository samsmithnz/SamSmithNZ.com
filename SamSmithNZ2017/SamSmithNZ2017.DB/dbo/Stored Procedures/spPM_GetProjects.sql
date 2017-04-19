CREATE PROCEDURE [dbo].[spPM_GetProjects]
AS

SELECT p.project_id, p.project_type_code, pt.project_type_name, p.project_name, p.project_status_code, ps.project_status_name, p.project_spec
FROM pm_project p
JOIN pm_project_type pt ON p.project_type_code = pt.project_type_code
JOIN pm_project_status ps ON p.project_status_code = ps.project_status_code
ORDER BY p.project_type_code, p.project_name