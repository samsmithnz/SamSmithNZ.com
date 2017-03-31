CREATE FUNCTION [dbo].[fnTab_GetAlbumTop100Name](@albumkey smallint)
	RETURNS varchar(10)
AS
BEGIN
RETURN convert(varchar(10),((@albumkey - 1) * 10) + 1) + '-' + convert(varchar(10),((@albumkey - 1) * 10) + 10)
END