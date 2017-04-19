CREATE PROCEDURE [dbo].[spPM_GetSongProject]
	@project_id uniqueidentifier
AS

SELECT * 
FROM pm_project p
JOIN pm_song s ON p.project_id = s.project_id
WHERE p.project_id = @project_id