
CREATE FUNCTION dbo.fnValor_GetRegion(@x int, @y int)
RETURNS varchar(10)
AS
BEGIN

   DECLARE @xStr varchar(10);
   DECLARE @yStr varchar(10);
   SELECT @xStr = '', @yStr = ''
   
	if (@x < 100)
		SELECT @xStr = '0'
	else
		SELECT @xStr = (@x / 100)
	if (@y < 100)
		SELECT @yStr = '' 
	else
		SELECT @yStr = (@y / 100)
	    
	RETURN 'R' + @yStr + @xStr;
END