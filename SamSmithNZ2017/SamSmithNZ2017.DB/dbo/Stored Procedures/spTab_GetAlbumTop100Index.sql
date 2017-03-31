CREATE PROCEDURE [dbo].[spTab_GetAlbumTop100Index]
AS
SELECT 0

--SET NOCOUNT ON

--CREATE TABLE #tmptop100 (albumkey smallint)
--INSERT INTO #tmptop100 SELECT 1
--INSERT INTO #tmptop100 SELECT 2
--INSERT INTO #tmptop100 SELECT 3
--INSERT INTO #tmptop100 SELECT 4
--INSERT INTO #tmptop100 SELECT 5
--INSERT INTO #tmptop100 SELECT 6
--INSERT INTO #tmptop100 SELECT 7
--INSERT INTO #tmptop100 SELECT 8
--INSERT INTO #tmptop100 SELECT 9
--INSERT INTO #tmptop100 SELECT 10

--SELECT t.albumkey, 'Top 100' as artistname, dbo.fnTab_GetAlbumTop100Name(t.albumkey) as albumname, ('Top 100' + ' - ' + dbo.fnTab_GetAlbumTop100Name(t.albumkey)) AS FullName,
--year(getdate()) as AlbumYear, 0 as IsABassTabAlbum, 0 as IsNewAlbum, 1 as IsUpdatedAlbum,
--isnull(convert(int,round(convert(decimal(16,4),sum(tt.rating))/convert(decimal(16,4),count(tt.rating)),0)),0) as averagerating
--FROM #tmptop100 t, TabAlbum ta
--LEFT OUTER JOIN TabTrack tt ON ta.albumkey = tt.albumkey and tt.rating > 0
--GROUP BY t.albumkey
--ORDER BY t.albumkey

--DROP TABLE #tmptop100

--/*
--SELECT ta.albumkey, ArtistName, AlbumName, (ArtistName + ' - ' + AlbumName) AS FullName, 
--AlbumYear, IsABassTabAlbum, IsNewAlbum, IsUpdatedAlbum, 
--isnull(convert(int,round(convert(decimal(16,4),sum(tt.rating))/convert(decimal(16,4),count(tt.rating)),0)),0) as averagerating
--FROM TabAlbum ta
--LEFT OUTER JOIN TabTrack tt ON ta.albumkey = tt.albumkey and tt.rating > 0
--WHERE IsIncludedInTheIndex = 1 
--GROUP BY IsAVariousSelection, ArtistName, AlbumName, AlbumYear, IsABassTabAlbum, IsNewAlbum, IsUpdatedAlbum, ta.albumkey
--ORDER BY IsAVariousSelection DESC, ArtistName, AlbumYear, AlbumName, IsABassTabAlbum
--*/