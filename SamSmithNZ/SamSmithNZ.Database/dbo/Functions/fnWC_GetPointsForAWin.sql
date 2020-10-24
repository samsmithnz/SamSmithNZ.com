CREATE FUNCTION [dbo].[fnWC_GetPointsForAWin](
	@tournament_code INT
)
RETURNS INT
AS
BEGIN
	DECLARE @total INT

	SELECT @total = 0

	IF (@tournament_code <= 14)
		SELECT @total = 2 --All Tournaments (including 1990) gave 2 points for a win, not 2. 
	ELSE
		SELECT @total = 3

	RETURN @total
END