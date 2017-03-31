
CREATE PROCEDURE [dbo].[spProblem_SaveProblem]
	@problem_number int,
	@description varchar(max),
	@notes varchar(max),
	@is_completed bit
AS

IF (exists (SELECT 1 FROM problem WHERE problem_number = @problem_number))
BEGIN
	UPDATE problem
	SET description = @description, notes = @notes, is_completed = @is_completed, last_updated = getdate()
	WHERE problem_number = @problem_number
END 
ELSE
BEGIN
	INSERT INTO problem
	SELECT @problem_number, @description, @notes, @is_completed, getdate()
END