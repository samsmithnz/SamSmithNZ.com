CREATE PROCEDURE [dbo].[spPM_SaveProject]
	@project_id uniqueidentifier,
	@project_type_code smallint,
	@project_name varchar(100),
	@project_spec varchar(8000),
	@project_status_code smallint
AS

DECLARE @count smallint

SELECT @count = count(*)
FROM pm_project
WHERE project_id = @project_id

IF (@count > 0)
BEGIN
	UPDATE pm_project
	SET project_type_code = @project_type_code, project_name = @project_name,
		project_spec = @project_spec, project_status_code = @project_status_code, last_updated = getdate()
	WHERE project_id = @project_id
END
ELSE
BEGIN
	INSERT INTO pm_project
	SELECT @project_id, @project_type_code, @project_name, @project_spec, @project_status_code, isnull((SELECT max(project_order) + 1 FROM pm_project),1), getdate()
END