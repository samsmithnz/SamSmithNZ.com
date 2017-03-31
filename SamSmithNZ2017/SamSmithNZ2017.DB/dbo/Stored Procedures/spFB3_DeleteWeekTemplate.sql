CREATE PROCEDURE [dbo].[spFB3_DeleteWeekTemplate]
	@id uniqueidentifier
AS

DELETE FROM FBWeek
WHERE record_id = @id

DELETE FROM FBWeekTemplate
WHERE record_id = @id