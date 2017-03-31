CREATE PROCEDURE [dbo].[spProblem_GetSearchResults]
	@record_id uniqueidentifier
AS

DECLARE @search_text varchar(100)

SELECT @search_text = search_text
FROM problem_search_parameters tsp
WHERE record_id = @record_id

CREATE TABLE #search_results (search_text varchar(100), problem_number int, 
	[description] varchar(8000), description_index int,
	notes varchar(8000), notes_index int)

INSERT INTO #search_results
SELECT @search_text as search_text, p.problem_number, 
	p.[description], charindex(@search_text, p.[description]),
	p.notes, charindex(@search_text, p.notes)
FROM problem p
WHERE (p.[description] like '%' + @search_text + '%') 
or (p.notes like '%' + @search_text + '%')

SELECT search_text, problem_number, 	
	CASE WHEN description_index <= 0 THEN '' ELSE SUBSTRING(p.[description], CASE WHEN description_index < 20 THEN 0 ELSE description_index - 20 END, 100) END as [description], 
	CASE WHEN notes_index <= 0 THEN '' ELSE SUBSTRING(p.notes, CASE WHEN notes_index < 20 THEN 0 ELSE notes_index - 20 END, 100) END as notes
FROM #search_results p
ORDER BY problem_number

DROP TABLE #search_results