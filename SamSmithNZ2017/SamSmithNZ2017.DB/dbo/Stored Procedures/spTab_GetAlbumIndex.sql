CREATE PROCEDURE [dbo].[spTab_GetAlbumIndex]
AS
SELECT 0
--SELECT ta.albumkey, ArtistName, AlbumName, (ArtistName + ' - ' + AlbumName) AS FullName, 
--AlbumYear, IsABassTabAlbum, IsNewAlbum, IsUpdatedAlbum, 
--isnull(convert(int,round(convert(decimal(16,4),sum(tt.rating))/convert(decimal(16,4),count(tt.rating)),0)),0) as averagerating
--FROM TabAlbum ta
--LEFT OUTER JOIN TabTrack tt ON ta.albumkey = tt.albumkey and tt.rating > 0
--WHERE IsIncludedInTheIndex = 1 
--GROUP BY IsAVariousSelection, ArtistName, AlbumName, AlbumYear, IsABassTabAlbum, IsNewAlbum, IsUpdatedAlbum, ta.albumkey
--ORDER BY IsAVariousSelection DESC, ArtistName, AlbumYear, AlbumName, IsABassTabAlbum