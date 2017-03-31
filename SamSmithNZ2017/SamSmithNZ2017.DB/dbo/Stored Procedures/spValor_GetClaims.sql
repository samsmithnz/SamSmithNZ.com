CREATE PROCEDURE [dbo].[spValor_GetClaims]
	@claim_code int = null,
	@x int = null,
	@y int = null,
	@owner varchar(100) = null,
	@added_by varchar(100) = null
AS
SELECT vc.*, vg.guild_name, [dbo].[fnValor_GetRegion](x, y) as region
FROM valor_claim vc
INNER JOIN valor_guild vg ON vc.guild_code = vg.guild_code
WHERE (claim_code = @claim_code or @claim_code is null)
and (x = @x or @x is null)
and (y = @y or @y is null)
and ([owner] like '%' + @owner + '%' or @owner is null)
and ([added_by] like '%' + @added_by + '%' or @added_by is null)
and last_updated > getdate() - 2 --48 hours
ORDER BY region, x, y, last_updated desc