CREATE TABLE [dbo].[LegoInventoryParts]
(
	[inventory_id] INT NOT NULL,
	part_num VARCHAR(100),
	color_id INT NULL,
	quantity INT NULL,
	is_spare BIT NULL
)
