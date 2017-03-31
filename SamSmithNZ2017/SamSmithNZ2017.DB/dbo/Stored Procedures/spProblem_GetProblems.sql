CREATE PROCEDURE [dbo].[spProblem_GetProblems]
	@problem_number int = null
AS

SELECT * 
FROM problem
WHERE @problem_number is null or problem_number = @problem_number
ORDER BY problem_number