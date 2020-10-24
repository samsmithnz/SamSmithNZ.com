CREATE PROCEDURE [dbo].[LegoGetOwnedSets]
	@OwnerCode INT = 1,
	@SetNum VARCHAR(100) = NULL
AS
BEGIN
	SELECT s.set_num AS SetNum,
		s.[name] AS SetName,
		s.[year] AS SetYear,
		t.id AS ThemeId,
		t.[name] AS ThemeName,
		s.num_parts AS NumberOfParts, 
		i.id as InventoryId 
	FROM LegoOwner o
	JOIN LegoMySets ms ON o.owner_code = ms.owner_code
	JOIN LegoSets s ON ms.set_num = s.set_num
	JOIN LegoInventories i ON s.set_num = i.set_num
	JOIN LegoThemes t ON t.id = s.theme_id
	WHERE o.owner_code = @OwnerCode
	AND (@SetNum IS NULL OR s.set_num = @SetNum) --'75218-1'
	ORDER BY s.[name]
END