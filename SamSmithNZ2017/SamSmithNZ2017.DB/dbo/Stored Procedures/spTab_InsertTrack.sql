CREATE PROCEDURE [dbo].[spTab_InsertTrack]
	@TrackName varchar(75), 
	@TrackPath varchar(100), 
	@TrackOrder int, 
	@AlbumKey int,
	@Rating smallint
AS
SELECT 0
--DECLARE @TrackKey int
--SELECT @TrackKey = max(TrackKey) + 1 FROM TabTrack
--INSERT INTO TabTrack (TrackKey, TrackName, TrackPath, TrackOrder, AlbumKey, Rating) 
--VALUES (@TrackKey, @TrackName, @TrackPath, @TrackOrder, @AlbumKey, @Rating)