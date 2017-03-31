

CREATE PROCEDURE [dbo].[spValor_ValidateAddClaim]
	@world_num int,
	@x int,
	@y int,
	@owner varchar(100),
	@last_updated datetime

AS

DECLARE @result varchar(100)

SELECT @result = ''
SELECT @result = 'The claim ' + CONVERT(varchar(10),x) + ', ' + CONVERT(varchar(10),y) + ' is already active and is currently assigned to: ' + [owner]
FROM valor_claim
WHERE (x = @x)
and (y = @y)
and last_updated > getdate() - 2 --within 48 hours

SELECT @result as result