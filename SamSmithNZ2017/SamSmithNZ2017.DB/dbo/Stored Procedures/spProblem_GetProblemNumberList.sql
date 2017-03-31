CREATE PROCEDURE [dbo].[spProblem_GetProblemNumberList]
	@limit int
AS
WITH
 E1(Number) AS (SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL
           SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL
           SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL
           SELECT 1),                 --10E1  or 10 rows
 E2(Number) AS (SELECT 1 FROM E1 a, E1 b), --10E2  or 100 rows
 E4(Number) AS (SELECT 1 FROM E2 a, E2 b), --10E3  or 10000 rows
 E8(Number) AS (SELECT 1 FROM E4 a, E4 b), --10E4  or 100000000 rows
E16(Number) AS (SELECT 1 FROM E8 a, E8 b)  --10E16 or more rows than you can shake a stick at
SELECT TOP (@limit) Number = ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) FROM E16