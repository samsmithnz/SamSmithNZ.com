CREATE PROCEDURE [dbo].[spTab_GetAlbumTop100Detail]
	@albumkey smallint
AS
SELECT 'Top 100' as artistname, 
dbo.fnTab_GetAlbumTop100Name(@albumkey) as albumname,
('Top 100  - ' + dbo.fnTab_GetAlbumTop100Name(@albumkey)) AS FullName,
'' as path, 0 as IsABassTabAlbum