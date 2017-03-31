CREATE PROCEDURE [dbo].[spPM_CreateProject]
	@project_type_code smallint,
	@project_name varchar(400),
	@project_spec varchar(8000),
	@project_status_code smallint
AS

DECLARE @project_id uniqueidentifier
SELECT @project_id = newid()

INSERT INTO pm_project
SELECT @project_id, @project_type_code, @project_name, @project_spec, @project_status_code, 
	(isnull((select max(project_order) + 1 FROM pm_project),1)), getdate()

--IF (@project_type_code = 1) --It's an idea
	--do nothing special
--ELSE 
IF (@project_type_code = 2) --It's software - business
	INSERT INTO pm_software
	SELECT newid(), @project_id, '0.00'
ELSE IF (@project_type_code = 3) --It's software - game
	INSERT INTO pm_software
	SELECT newid(), @project_id, '0.00'
ELSE IF (@project_type_code = 4) --It's song - original
	INSERT INTO pm_song
	SELECT newid(), @project_id, '', ''
ELSE IF (@project_type_code = 5) --It's song - cover
	INSERT INTO pm_song
	SELECT newid(), @project_id, '', ''

SELECT @project_id as project_id