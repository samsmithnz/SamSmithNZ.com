CREATE PROCEDURE [dbo].[LegoGetParts]
	@SetNum VARCHAR(100)
AS
BEGIN
	SELECT SUM(ip.quantity) AS Quantity, 
		c.[id] AS ColorId,
		c.[name] AS ColorName, 
		p.part_num AS PartNum, 
		p.[name] AS PartName, 
		pc.[name] AS PartCategoryName
	FROM LegoInventories i 
	JOIN LegoInventoryParts ip ON i.id = ip.inventory_id 
	JOIN LegoParts p ON ip.part_num = p.part_num
	JOIN LegoPartCategories pc ON p.part_cat_id = pc.id
	JOIN LegoColors c ON ip.color_id = c.id
	WHERE i.set_num = @SetNum --'75218-1'
	AND ip.is_spare = 0
	GROUP BY c.[id], c.[name], p.part_num, p.[name], pc.[name]
	ORDER BY c.[id], p.[name]
END
GO
